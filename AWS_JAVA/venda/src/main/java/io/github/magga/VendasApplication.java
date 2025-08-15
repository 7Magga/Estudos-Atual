package io.github.magga;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.AutoConfigureOrder;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@SpringBootApplication
@RestController
public class VendasApplication {

    //@Autowired
    //@Qualifier("applicationName") //nome do beam
    //private String applicationName;

    @Value("${application.name}")
    private String applicationName;

    @GetMapping("/hello")
    public String helloWord(){
        return applicationName;
    }
    public static void main(String[] args) {
        SpringApplication.run(VendasApplication.class,args);
    }
}
