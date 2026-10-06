import { Component, signal } from '@angular/core';
import { Header } from '../components/header/header';
import { Menu } from '../components/menu/menu';
import { RegisterScore } from '../pages/register-score/register-score';
import { Ranking } from '../pages/ranking/ranking';
import { Page } from '../models/page';

@Component({
  selector: 'app-root',
  imports: [Header, Menu, RegisterScore, Ranking],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {

  // selectedPage é um signal do tipo Page ou do tipo null, começa com null
  selectedPage = signal<Page | null>(null);   

  // função responsável por mudar o valor do signal selectedPage
  selectPage(page: Page) {
    this.selectedPage.set(page);
  }
}