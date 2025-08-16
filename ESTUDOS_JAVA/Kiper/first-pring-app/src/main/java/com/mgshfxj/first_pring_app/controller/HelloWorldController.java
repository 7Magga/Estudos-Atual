package com.mgshfxj.first_pring_app.controller;

import com.mgshfxj.first_pring_app.domain.User;
import com.mgshfxj.first_pring_app.service.helloService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
//STATELESS -> A cada requisição é enviado o dados do usuario
//STATEFULL -> Estado de cada cliente é mantido no servidor
//@Controller
//@ResponseBody

//Para mapear a URL que essa classe vai atender:
@RequestMapping("/hello")
public class HelloWorldController {

    //Declaro aqui a dependencia privada
    private helloService service;

    //Na construção faço ele receber o service como construtor
    //E atribuo ele na dependencia declarada.
    //Isto é injeção de dependencia
    public HelloWorldController(helloService service){
        this.service = service;
    }

    //Outra forma de fazer:
    @Autowired
    private helloService service2;


    //Para definir o METODO que essa função ira responder
    @GetMapping
    public String helloWordGet(){
        return "Ola - GET";
    }

    //Para definir o METODO que essa função ira responder
    @PutMapping
    public String helloWordPut(){
        return "Ola - PUT";
    }

    //Para colocar mais parametro no path:
    @GetMapping("/get")
    public String helloGet(){
        return service2.helloWord("Matheus");
    }

    @PostMapping
    //Aqui vou declarar o parametro com uma notação para indicar que ele vem do body
    //Utilizei uma classe de User que esta dentro da domain para declarar o formato do body da REQ
    //Na Classe de domain utilizei o Loombok
    public String helloPost(@RequestBody User body){
        return "Ola Post " + body.getName();
    }

    @PostMapping("/{id}")
    //Aqui vou declarar o parametro com uma notação para indicar que ele vem do body
    //Utilizei uma classe de User que esta dentro da domain para declarar o formato do body da REQ
    //Na Classe de domain utilizei o Loombok
    //Utilizando um parametro no path
    public String helloPostId(@PathVariable("id") String id, @RequestBody User body){
        return "Ola Post " + body.getName() + ":" + id;
    }

    @PostMapping("/filter/{id}")
    //Aqui vou declarar o parametro com uma notação para indicar que ele vem do body
    //Utilizei uma classe de User que esta dentro da domain para declarar o formato do body da REQ
    //Na Classe de domain utilizei o Loombok
    //Utilizando um parametro no path
    public String helloPostFilter(@PathVariable("id") String id,@RequestParam(value = "name",defaultValue = "nenhum") String filter, @RequestBody User body){
        return "Ola Post " + body.getName() + ":" + id + " Filter " + filter;
    }
}
