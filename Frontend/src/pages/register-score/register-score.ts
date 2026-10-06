import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ScoreService } from '../../services/score-service';
import { getErrorMessage } from '../../services/error-helper';
import { ErrorMessage } from '../../components/error-message/error-message';

@Component({
  selector: 'app-register-score',
  imports: [FormsModule, ErrorMessage],
  templateUrl: './register-score.html',
  styleUrl: './register-score.css',
})

export class RegisterScore {
  private scoreService = inject(ScoreService);

  // Campos do formulário
  playedAt = '';
  points: number | null = null;
  playerName = '';

  // Valores que a tela precisa "acompanhar" (signals)
  errorMessage = signal('');
  successMessage = signal('');
  saving = signal(false);

  save() {
    this.errorMessage.set('');
    this.successMessage.set('');

    if (!this.playedAt || this.points === null || !this.playerName.trim()) {
      this.errorMessage.set('Preencha todos os campos.');
      return;
    }

    if (!Number.isInteger(this.points) || this.points < 0) {
      this.errorMessage.set('A pontuação deve ser um número inteiro, sem negativos.');
      return;
    }

    this.saving.set(true);

    // this.scoreService.registerScore: chama o método registerScore() do scoreService 
    // .subscribe(): quando a requisição acabar, quero saber oq aconteceu
    // next(), roda quando a API retorna sucesso, 200/201/202/204
    // error(), roda quando a API retorna erro, 400/401/403/404/409/422
    this.scoreService
      .registerScore({
        playerName: this.playerName.trim(),
        points: this.points,
        playedAt: this.playedAt,
      })
      .subscribe({
        next: () => {
          this.successMessage.set('Pontuação registrada com sucesso!');
          this.playedAt = '';
          this.points = null;
          this.playerName = '';
          this.saving.set(false);
        },
        error: (error) => {
          this.errorMessage.set(getErrorMessage(error));
          this.saving.set(false);
        },
      });
  }
}
