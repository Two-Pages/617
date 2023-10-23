package org.example;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;
import org.example.InitMethod;
public class Myclass {
    private String name;
    private int id;
    public Myclass(){}
    public Myclass(String name,int id){
        this.name=name;
        this.id=id;
    }
   // @Retention(value= RetentionPolicy.SOURCE)
    @InitMethod()
    private void init(){
        System.out.println("Hello world!");
    }

}
