package javavc;

public class Pessoa extends SerVivo {

    public Pessoa(int idade) {
        super(idade); // Chama o construtor da classe SerVivo com idade 0
        System.out.println("Pessoa criada!");
    }
    @Override
    public void respirar() {
        System.out.println(this.idade);
        System.out.println("Pessoa respirando");
    }

    @Override
    public void seReproduzir() {
        System.out.println(this.idade);
        System.out.println("Pessoa se reproduzindo");
    }
}
