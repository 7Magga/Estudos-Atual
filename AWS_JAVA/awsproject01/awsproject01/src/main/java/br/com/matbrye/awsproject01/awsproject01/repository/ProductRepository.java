package br.com.matbrye.awsproject01.awsproject01.repository;

import br.com.matbrye.awsproject01.awsproject01.Model.Product;
import org.springframework.data.repository.CrudRepository;

import java.util.Optional;

public interface ProductRepository extends CrudRepository<Product,Long> {

    Optional<Product> findByCode(String code);
}
