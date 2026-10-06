import { Component, OnInit, inject, signal } from '@angular/core';
import { ScoreService } from '../../services/score-service';
import { RankingItem } from '../../models/ranking-item';
import { getErrorMessage } from '../../services/error-helper';
import { ErrorMessage } from '../../components/error-message/error-message';
import { PlayerStatisticsPopup } from '../../components/player-statistics-popup/player-statistics-popup';
import { formatNumber } from '../../services/format-helper';

@Component({
  selector: 'app-ranking',
  imports: [ErrorMessage, PlayerStatisticsPopup],
  templateUrl: './ranking.html',
  styleUrl: './ranking.css'
})
export class Ranking implements OnInit {

  private scoreService = inject(ScoreService);

  formatNumber = formatNumber;

  ranking = signal<RankingItem[]>([]);
  errorMessage = signal('');
  selectedPlayer = signal<string | null>(null);   // null = popup fechado

  ngOnInit() {
    this.scoreService.getRanking().subscribe({
      next: (data) => this.ranking.set(data.slice(0, 10)),
      error: (error) => this.errorMessage.set(getErrorMessage(error))
    });
  }

  openPopup(playerName: string) {
    this.selectedPlayer.set(playerName);
  }

  closePopup() {
    this.selectedPlayer.set(null);
  }
}