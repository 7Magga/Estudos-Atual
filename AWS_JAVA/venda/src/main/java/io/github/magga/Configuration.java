package io.github.magga;

import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Profile;

@org.springframework.context.annotation.Configuration
@Profile("development")
public class Configuration {
    @Bean(name = "applicationName")
    public String applicationName(){
        return "Sistema de vandas";
    }


    //Dessa maneira executo algo no momento de iniciar. Podendo ser um comando ou não
    @Bean
    public CommandLineRunner executar(){
       return args -> {
           System.out.println("Rodando Desenv");
       };
    }
}
