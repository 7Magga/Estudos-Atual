package com.myorg;

import software.amazon.awscdk.*;
import software.amazon.awscdk.services.applicationautoscaling.EnableScalingProps;
import software.amazon.awscdk.services.ecs.*;
import software.amazon.awscdk.services.ecs.patterns.ApplicationLoadBalancedFargateService;
import software.amazon.awscdk.services.ecs.patterns.ApplicationLoadBalancedTaskImageOptions;
import software.amazon.awscdk.services.elasticloadbalancingv2.HealthCheck;
import software.amazon.awscdk.services.logs.LogGroup;
import software.constructs.Construct;

import java.util.HashMap;
import java.util.Map;
// import software.amazon.awscdk.Duration;
// import software.amazon.awscdk.services.sqs.Queue;

public class Service01Stack extends Stack {
    public Service01Stack(final Construct scope, final String id, Cluster cluster) {
        this(scope, id, null,cluster);
    }

    public Service01Stack(final Construct scope, final String id, final StackProps props,Cluster cluster) {
        super(scope, id, props);

        Map<String,String> envVariables = new HashMap<>();
        envVariables.put("SPRING_DATASOURCE_URL","jdbc:mariadb://"+ Fn.importValue("rds-endpoint")+":3306/aws_project01?createDatabaseIfNotExist=true");
        envVariables.put("SPRING_DATASOURCE_USERNAME","admin");
        envVariables.put("SPRING_DATASOURCE_PASSWORD",Fn.importValue("rds-password"));

        //Criação do loadbalance
        ApplicationLoadBalancedFargateService service01 = ApplicationLoadBalancedFargateService.Builder.create(this, "ALB01")
                .serviceName("Service-01")
                .cluster(cluster)
                .cpu(512)
                //Quantidade de instancias
                .desiredCount(2)
                //Porta a ser utilizada
                .listenerPort(8080)
                //Para quando colocarmos a .natGateways(0)
                //.assignPublicIp(true)
                //Quantidade de memoria
                .memoryLimitMiB(1024)
                .taskImageOptions(
                        ApplicationLoadBalancedTaskImageOptions.builder()
                                //Nome do countainer
                                .containerName("aws_project01")
                                //Link do repositorio do HUB
                                .image(ContainerImage.fromRegistry("matbrye/curso_aws_project01:spring01"))
                                .containerPort(8080)
                                //Configuração do logs
                                .logDriver(LogDriver.awsLogs(AwsLogDriverProps.builder()
                                                .logGroup(LogGroup.Builder.create(this,"Service01LogGroup")
                                                        //Nome do grupo que serao armazenados os logs
                                                        .logGroupName("Service01")
                                                        .removalPolicy(RemovalPolicy.DESTROY)
                                                        .build())
                                                .streamPrefix("Service01")
                                        .build()))
                                //Passo as variaveis de ambiente
                                .environment(envVariables)
                                .build())
                //Configuração de publico
                .publicLoadBalancer(true)
                .build();

        //Check de saude
        service01.getTargetGroup().configureHealthCheck(new HealthCheck.Builder()
                .path("/actuator/health")
                .port("8080")
                .healthyHttpCodes("200")
                .build());

        //Configuração do autoScaling
        ScalableTaskCount scalableTaskCount = service01.getService().autoScaleTaskCount(EnableScalingProps.builder()
                .minCapacity(1)
                .maxCapacity(3)
                .build());

        //Configuração dos parametros para o autoscaling
        scalableTaskCount.scaleOnCpuUtilization("Service01AutoScaling", CpuUtilizationScalingProps.builder()
                .targetUtilizationPercent(50)
                .scaleInCooldown(Duration.seconds(60))
                .scaleOutCooldown(Duration.seconds(60))
                .build());
    }
}
