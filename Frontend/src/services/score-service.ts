import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { API_URL } from "./api-config";
import { ScoreRequest } from "../models/score-request";
import { RankingItem } from "../models/ranking-item";

// O decorador @Injectable declara para o Angular que a classe é um service
@Injectable({ providedIn: 'root'})

export class ScoreService {

    // Recebemos na propriedade http, a instância de HttpClient que fizemos em app-config
    private http = inject(HttpClient);

    // Método responsável por fazer uma requisição POST à https://localhost:5108/api/scores, 
    // enviando um objeto data no formato de ScoreRequest
    registerScore(data: ScoreRequest){

        return this.http.post(`${API_URL}/api/scores`, data);

    }

    // Método responsável por fazer uma requisição GET à https://localhost:5108/api/scores/ranking,
    // espera receber um array de objetos do tipo Ranking Item
    getRanking(){

        return this.http.get<RankingItem[]>(`${API_URL}/api/scores/ranking`);
        
    }
}
