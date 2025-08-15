package javavc;

public class Mobi implements Carro{
    //Construtor, então toda vez que dar um new Carro(), ele vai chamar o construtor
    //Construtor é um método especial que tem o mesmo nome da classe e não tem retorno
    String modelo;
    public Mobi(){
        this.modelo = "Mobi";
    }

    //Métodos da classe Carro que vem da interface implementada de Carro
    //@Override para sobrescrever os métodos da interface
    //Se não colocar o @Override, o código compila, mas não é uma boa
    @Override
    public void ligar() {
        //Isto é polimorfismo, pois o método ligar() é o mesmo da classe Sandeiro, mas o comportamento é diferente
        System.out.println(this.modelo + " ligado!");
    }
    @Override
    public void desligar() {
        System.out.println("Carro desligado!");
    }
    @Override
    public void acelerar(int velocidade) {
        System.out.println("Carro acelerando a " + velocidade + " km/h");
    }
    @Override
    public void frear(int intensidade) {
        System.out.println("Carro freando com intensidade " + intensidade);
    }
}
