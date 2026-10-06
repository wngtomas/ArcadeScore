import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

import { provideHttpClient } from '@angular/common/http'; // Habilita chamadas HTTP

// Tudo que estiver nos providers do app.config, ficará disponível para acesso em todo o projeto
export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient() // Disponibiliza o HttpClientProvider
  ]
};
