package com.mgshfxj.first_pring_app.service;
//CONTEM AS REGRAS DE NEGOCIOS
//CONTROLLER RECEBE E PASA PARA O SERVICE O PROCESSAMENTO

import org.springframework.stereotype.Service;

//Para isso declaramos o @Service na classe de serviço
@Service
public class helloService {

    //Aqui declaramos os métodos normalmente
    public String helloWord(String name){
        return "Ola Direto do serviço " + name;
    }
}
