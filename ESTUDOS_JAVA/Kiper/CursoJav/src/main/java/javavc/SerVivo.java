package javavc;

public abstract class SerVivo {
    protected int idade;
    public SerVivo(int idade){
        this.idade = idade;
    }

    abstract void respirar();
    abstract void seReproduzir();
    void seAlimentar(){
        System.out.println("Ser vivo se alimentando");
    };
}
