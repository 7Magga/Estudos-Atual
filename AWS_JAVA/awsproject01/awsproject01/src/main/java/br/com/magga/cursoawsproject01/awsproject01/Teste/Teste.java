package br.com.magga.cursoawsproject01.awsproject01.awsproject01.Teste;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/teste")
public class Teste {

    @GetMapping("/dog/{name}")
    public ResponseEntity<?>dogTeste(@PathVariable String name){
        return ResponseEntity.ok("Name " + name);
    }
}
