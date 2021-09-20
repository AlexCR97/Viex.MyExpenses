import { Component, OnInit } from '@angular/core';

const template = /*html*/`
<app-header title="Descriptors" variant="menu"></app-header>

<div class="container py-5">

  <app-collapse-container>
    <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Maxime aspernatur, repudiandae doloremque, illo dolores doloribus praesentium hic adipisci aliquam quo enim nobis tenetur iste alias autem molestias sapiente nesciunt excepturi?</p>
  </app-collapse-container>

</div>
`

@Component({
  selector: 'app-descriptors-page',
  template,
})
export class DescriptorsPageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
