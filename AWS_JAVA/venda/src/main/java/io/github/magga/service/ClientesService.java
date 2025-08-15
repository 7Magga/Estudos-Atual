package io.github.magga.service;

import io.github.magga.model.Cliente;
import io.github.magga.repository.ClientesRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ClientesService {

    @Autowired
    private ClientesRepository repository;
    //@Autowired // vai entender que precisa executar antes de buildar
    /*public ClientesService(ClientesRepository repository){
        this.repository = repository;
    }*/

    public void salvarCliente(Cliente cliente){
        validarCliente(cliente);
        repository.persistirCliente(cliente);
    }
    public void validarCliente(Cliente cliente){
        //aplicar
    }
}
