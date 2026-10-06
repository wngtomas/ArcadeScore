import { Component, OnInit, inject, input, output, signal } from '@angular/core';
import { PlayerService } from '../../services/player-service';
import { PlayerStatistics } from '../../models/player-statistics';
import { getErrorMessage } from '../../services/error-helper';
import { ErrorMessage } from '../error-message/error-message';
import { formatNumber, formatPlayingTime } from '../../services/format-helper';

@Component({
  selector: 'app-player-statistics',
  imports: [ErrorMessage],
  templateUrl: './player-statistics-popup.html',
  styleUrl: './player-statistics-popup.css'
})

export class PlayerStatisticsPopup {

  private playerService = inject(PlayerService);
  
  formatNumber = formatNumber;
  formatPlayingTime = formatPlayingTime;

  // input, o component pai (ranking) está passando um dado pro component filho(popup)
  // em ranking.html, quando clicamos em um jogador, passamos o nome do jogador para o popup:
  // [playerName]="name"
  playerName = input.required<string>();

  // output, serve para o component filho passar eventos/dados para o component pai
  // Nesse caso, o component filho (popup) passa um evento para o component pai (ranking)
  // Esse output é do tipo void(), ou seja não passa nenhum dado, pois o evento que é passado 
  // é o closed.emit(). 
  // Se estivessemos enviando, por ex: closed.emit("João"); Não poderia ser void.
  // Em ranking.html, temos: (closed)="closePopup()"
  // Oq faz com que: quando tivermos o evento closed.emit() no popup, vamos rodar closePopup() 
  // no ranking
  closed = output<void>(); 

  statistics = signal<PlayerStatistics | null>(null);
  errorMessage = signal('');

  // ngOnInit roda uma vez, quando o popup aparece
  ngOnInit() {
    this.playerService.getStatistics(this.playerName()).subscribe({
      next: (data) => this.statistics.set(data),
      error: (error) => this.errorMessage.set(getErrorMessage(error))
    });
  }
}