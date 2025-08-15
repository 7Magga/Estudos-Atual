import { Component, OnChanges, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `<router-outlet>
    <app-title title="Ola mundo"></app-title>
  </router-outlet>`
})
export class AppComponent implements OnInit, OnChanges {
  constructor(){}
  //Ciclo de vida: inicio do build
  ngOnInit():void{
    // setTimeout(()=>{
    //   console.log(1);
    // },5000)
  }

  //Cliclo de vida: sempre que recebe um dado externo
  ngOnChanges():void{
    //Implementado no title.componente
  }

  //Ciclo de vida: sempre que as entradas do componente são verificadas
  ngDoCheck():void{
    //Implementado no title.componente
  }

}
