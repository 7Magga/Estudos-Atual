package br.com.matbrye.awsproject01.awsproject01.Controller;

import br.com.matbrye.awsproject01.awsproject01.Model.Product;
import br.com.matbrye.awsproject01.awsproject01.repository.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.awt.image.ReplicateScaleFilter;
import java.util.Optional;

@RestController
@RequestMapping("/api/products")
public class ProductController {

    private ProductRepository productRepository;
    @Autowired
    public ProductController(ProductRepository productRepository){
        this.productRepository = productRepository;

    }

    @GetMapping
    public Iterable<Product> findAll(){return productRepository.findAll();}

    @GetMapping("/{id}")
    public ResponseEntity<Product> findById(@PathVariable long id){
        Optional<Product> optProduct = productRepository.findById(id);
        if(optProduct.isPresent())
            return new ResponseEntity<Product>(optProduct.get(), HttpStatus.OK);
        else
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    @PostMapping
    public ResponseEntity<Product> saveProduct(@RequestBody Product product){
        Product productCreated = productRepository.save(product);
        return new ResponseEntity<Product>(productCreated,HttpStatus.CREATED);
    }

    @PutMapping(path = "/{id}")
    public ResponseEntity<Product> updateProduct(
            @RequestBody Product product,@PathVariable("id") long id){

        if(productRepository.existsById(id)){
            product.setId(id);
            Product productUpdate = productRepository.save(product);

            return new ResponseEntity<Product>(productUpdate,HttpStatus.OK);
        }else{
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @DeleteMapping(path = "/{id}")
    public ResponseEntity<Product> deleteProduct(@PathVariable("id") long id){
        Optional<Product> optProduct = productRepository.findById(id);
        if(optProduct.isPresent()){
            Product product = optProduct.get();
            productRepository.delete(product);
            return new ResponseEntity<Product>(product,HttpStatus.OK);
        }else{
            return new ResponseEntity<Product>(HttpStatus.NOT_FOUND);
        }
    }

    @GetMapping(path = "/bycode")
    public ResponseEntity<Product> findByCode(@RequestParam String code){
        Optional<Product> optProduct = productRepository.findByCode(code);
        if(optProduct.isPresent()){
            return new ResponseEntity<Product>(optProduct.get(),HttpStatus.OK);
        }else{
            return new ResponseEntity<Product>(HttpStatus.NOT_FOUND);
        }
    }

}
