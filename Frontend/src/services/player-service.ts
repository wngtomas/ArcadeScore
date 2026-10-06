import { Injectable, inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { API_URL } from "./api-config";
import { PlayerStatistics } from "../models/player-statistics";

// O decorador @Injectable declara para o Angular que a classe é um service
@Injectable({providedIn: 'root'})

export class PlayerService {

    // Recebemos na propriedade http, a instância de HttpClient que fizemos em app-config
    private http = inject(HttpClient);

    // Método responsável por fazer uma requisição GET à https://localhost:5108/api/players/
    // {playerName}/statistics. Recebe como parâmetro playerName do tipo string e espera
    // receber um objeto do tipo PlayerStatistics
    getStatistics(playerName : string){

        // encodeURIComponent() => transforma caracteres especiais em um formtato seguro para
        // uso em URLs
        const name = encodeURIComponent(playerName);
        return this.http.get<PlayerStatistics>(`${API_URL}/api/players/${name}/statistics`);

    } 

}