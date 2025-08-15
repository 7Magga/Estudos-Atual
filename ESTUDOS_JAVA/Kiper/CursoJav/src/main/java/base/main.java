package base;
import javavc.Carro;
import javavc.Mobi;
import javavc.Sandeiro;
import javavc.Pessoa;

public class main {
    public static void main(String[] args) {
        Sandeiro car = new Sandeiro();
        Carro _car = new Sandeiro();

        //Testando metodos da interface Carro que esta no javavc que foram implementados na classe Sandeiro
        car.ligar();
        car.acelerar(80);
        car.frear(50);
        car.desligar();

        _car.ligar();
        _car.acelerar(70);
        _car.frear(30);
        _car.desligar();

        Mobi mobi = new Mobi();
        mobi.ligar();
        mobi.acelerar(90);
        mobi.frear(20);
        mobi.desligar();

        newCarro newCarro = new newCarro("Fusca", "Azul", 1970);
        System.out.println("Modelo: " + newCarro.recuperarModelo());
        System.out.println("Cor: " + newCarro.recuperarCor());
        System.out.println("Ano: " + newCarro.recuperarAno());


        Pessoa pessoa = new Pessoa(10);
        pessoa.respirar();
        pessoa.seReproduzir();

    }
//    public static void main(String[] args) {
//        //System.out.println("Hello, World!");
//        //Vetor de inteiros
//        int[] vetor = {1, 2, 3, 4, 5};
//        int[] novov = new int[10];
//        System.out.println(novov.length);
//
//        ArrayList<Integer> lista = new ArrayList<>();
//        lista.add(0);
//        lista.add(1);
//
//        ArrayList<String> listaString = new ArrayList<>();
//        listaString.add("Hello");
//        listaString.add("World");
//
//        for (int i = 0; i < listaString.size(); i++) {
//            System.out.println(listaString.get(i));
//        }
//
//        for (int i = 0; i < vetor.length; i++) {
//            System.out.println(vetor[i]);
//        }
//
//        int i = 0;
//        while (i < vetor.length) {
//            try {
//                System.out.println("TESTE"+ vetor[i]);
//            } catch (NumberFormatException e) {
//                System.out.println("Erro" + e.getMessage());
//            }
//            i++;
//        }
//
//        double idade = 24.0;
//        Integer iIdade = (int)idade;
//
//        String sIdade = "24";
//        Integer _idade = Integer.parseInt(sIdade);
//
//        String _sIdade = iIdade.toString();
//        String _sIdade2 = Integer.toString(iIdade);
//        String _sIdade3 = String.valueOf(iIdade);
//    }
}
class newCarro {
    String modelo;
    String cor;
    int ano;

    //Construtor com parâmetros
    public newCarro(String modelo, String cor, int ano) {
        this.modelo = modelo;
        this.cor = cor;
        this.ano = ano;
    }

    public String recuperarModelo(){
        if(!validaCarro()) {
            return this.modelo;
        }
        return "Carro inválido!";
    }

    public String recuperarCor(){
        return this.cor;
    }

    public Integer recuperarAno(){
        return this.ano;
    }

    //Chamadas dentro da mesma classe ele consegue enxergar o método
    private Boolean validaCarro() {
        return this.modelo.isBlank();
    }

    //Chamadas de fora da classe ele não consegue enxergar o método
    protected Boolean validaCarro2() {
        return this.modelo.isBlank();
    }
}