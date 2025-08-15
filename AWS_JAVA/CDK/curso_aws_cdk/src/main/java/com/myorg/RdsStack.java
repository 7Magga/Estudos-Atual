package com.myorg;

import software.amazon.awscdk.*;
import software.amazon.awscdk.services.ec2.*;
import software.amazon.awscdk.services.rds.*;
import software.constructs.Construct;

import javax.xml.crypto.Data;
import java.util.Collections;
// import software.amazon.awscdk.Duration;
// import software.amazon.awscdk.services.sqs.Queue;

public class RdsStack extends Stack {
    public RdsStack(final Construct scope, final String id, Vpc vpc) {
        this(scope, id, null,vpc);
    }

    public RdsStack(final Construct scope, final String id, final StackProps props,Vpc vpc) {
        super(scope, id, props);

        //Captura do parametro
        CfnParameter dataBasePassword = CfnParameter.Builder.create(this,"dataBasePassword")
                .type("String")
                .description("The RDS instance password")
                .build();

        //Pega o security group da vpc e adiciona coisas nele
        ISecurityGroup iSecurityGroup = SecurityGroup.fromSecurityGroupId(this,id,vpc.getVpcDefaultSecurityGroup());
        iSecurityGroup.addIngressRule(Peer.anyIpv4(), Port.tcp(3306));

        DatabaseInstance databaseInstance = DatabaseInstance.Builder
                //Id
                .create(this,"Rds01")
                //Nome
                .instanceIdentifier("aws-project01-db")
                //Tipo da instancia
                .engine(DatabaseInstanceEngine.mysql(MySqlInstanceEngineProps.builder()
                                .version(MysqlEngineVersion.VER_5_7)
                        .build()))
                //Colocando dentro da vpc
                .vpc(vpc)
                //Criação das credenciais
                .credentials(Credentials.fromUsername("admin",
                        //Criando a credencial
                        CredentialsFromUsernameOptions.builder()
                                .password(SecretValue.plainText(dataBasePassword.getValueAsString()))
                                .build()))
                //Definindo a maquina e seu tamanho
                .instanceType(InstanceType.of(InstanceClass.BURSTABLE2,InstanceSize.MICRO))
                .multiAz(false)
                //Tamanho do disco
                .allocatedStorage(10)
                //Definindo o security group
                .securityGroups(Collections.singletonList(iSecurityGroup))
                //Definindo a subrede
                .vpcSubnets(SubnetSelection.builder()
                        .subnets(vpc.getPrivateSubnets())
                        .build())
                .build();

        //Precisamos expor o endpoint e a senha
        CfnOutput.Builder.create(this,"rds-endpoint")
                .exportName("rds-endpoint")
                .value(databaseInstance.getDbInstanceEndpointAddress())
                .build();

        CfnOutput.Builder.create(this,"rds-password")
                .exportName("rds-password")
                .value(dataBasePassword.getValueAsString())
                .build();
    }
}
