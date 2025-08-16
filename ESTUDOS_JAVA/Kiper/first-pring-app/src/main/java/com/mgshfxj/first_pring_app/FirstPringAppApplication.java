package com.mgshfxj.first_pring_app;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Profile;

@SpringBootApplication
//Aqui posso definir o properties que vou carregar
//Lembrar de declarar o spring.profiles.active=prd no properties
//Ou usar a partir de variavel de ambiente: spring.profiles.active=${ACTIVE_PROFILE:dev}
//Se usar por variavel de ambiente não preciso declarar o profile aqui
//@Profile("spring.profiles.active")
public class FirstPringAppApplication {

	public static void main(String[] args) {
		SpringApplication.run(FirstPringAppApplication.class, args);
	}

}
