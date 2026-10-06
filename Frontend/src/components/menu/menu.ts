import { Component, input, output } from '@angular/core';
import { Page } from '../../models/page';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.html',
  styleUrl: './menu.css'
})

export class Menu {

  // input: recebe do component pai
  selectedPage = input<Page | null>(null);   

  // output: passa para o component pai
  pageSelected = output<Page>();             
}