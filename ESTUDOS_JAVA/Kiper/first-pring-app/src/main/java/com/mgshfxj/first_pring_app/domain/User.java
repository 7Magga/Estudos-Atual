package com.mgshfxj.first_pring_app.domain;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
//@AllArgsConstructor
public class User {

    public String name;
    public Integer age;

    public User(String name,Integer age){
        this.name = name;
        this.age = age;
    }
}
