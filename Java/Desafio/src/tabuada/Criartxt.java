package tabuada;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Scanner;

public class Criartxt {

  public void arquivo(String classe, ArrayList propriedades) throws IOException {
    Scanner ler = new Scanner(System.in);
    System.out.println(propriedades);

    File myObj = new File("classe.txt");
    
    if (myObj.createNewFile()) {
      System.out.println("Arquivo criado: " + myObj.getName());
    }
    
    FileWriter arq = new FileWriter("classe.txt");
    
    PrintWriter gravarArq = new PrintWriter(arq);
    
    gravarArq.printf("public class %s\n{\n", classe);

    for (int i=0; i < propriedades.size(); i++) {
        gravarArq.printf("private String %s;\n", propriedades.get(i).toString());
    }
    
    gravarArq.printf("\npublic %s() {}\n", classe);
    
    for (int i=0; i < propriedades.size(); i++) {
        gravarArq.printf(
            "public void set%1$s(String _%1$s) {%1$s = _%1$s;};\n",
            propriedades.get(i).toString()
        );
    }
    
    gravarArq.printf("\n");
    
    for (int i=0; i < propriedades.size(); i++) {
        gravarArq.printf(
            "public String get%1$s() {return %1$s;};\n",
            propriedades.get(i).toString()
        );
    }
    gravarArq.printf("}");
    //public String getTitulo() {return titulo;}
    
    arq.close();

  }
}