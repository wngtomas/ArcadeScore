import { HttpErrorResponse } from '@angular/common/http';

/*
    Formato erro DTO:
    {
        "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
        "title": "One or more validation errors occurred.",
        "status": 400,
        "errors": {
            "PlayerName": ["O nome do jogador é obrigatório."],
             "Points": ["A pontuação é obrigatória."]
        },
        "traceId": "00-..."
    }

    Erro de DTO - formato de dados (não é JSON):
    {
        "status": 400,
        "errors": {
        "$.points": ["The JSON value could not be converted to System.Nullable`1[System.Int32]. Path: $.points | LineNumber: 0 | BytePositionInLine: 20."],
        "dto": ["The dto field is required."]
    }
    }

    Erros do tipo Notfound("xxxxxxx"):
    "xxxx" => fica em error.error
*/

export function getErrorMessage(error: unknown): string {
  // Se error não for uma instância de HttpErrorResponse, retorna erro inesperado
  if (!(error instanceof HttpErrorResponse)) {
    return 'Ocorreu um erro inesperado.';
  }

  // Se o status da resposta da requisição for 0, a API está desligada
  if (error.status === 0) {
    return 'Não foi possível conectar à API. Verifique se o servidor está rodando.';
  }

  // Se o status for maior de 500, temos um erro de server
  // 500 Internal Server Error,  501 Not Implemented, 502 Bad Gateway, 503 Service Unavailable
  if (error.status >= 500) {
    return 'Erro interno no servidor. Tente novamente mais tarde';
  }

  // error.error corresponde ao corpo da resposta enviada pela API
  // ex: NotFound("Nenhuma parida encontrada para este jogador");
  const body = error.error;

  // Se tiver o campo errors no body
  if (body?.errors) {
    // Array de strings, começa vazio
    const messages: string[] = [];
    // Variável de controle para erros técnicos
    let hasTechnicalError = false;

    // Imagine que body.errors, seja:
    /*

         body.errors = {
            PlayerName: ["O nome é obrigatório."],
            Points: ["A pontuação é obrigatória."]
        }

        Object.keys(body.errors) seria: ["PlayerName", "Points"]
        Portanto, no for abaixo, percorremos cada uma dessas cahves
        */
    for (const field of Object.keys(body.errors)) {
      // Se o campo começar com $, é um erro de conversão de JSON
      // Definimos hasTechnicalError como true
      if (field.startsWith('$')) {
        hasTechnicalError = true;
        continue;
      }

      // Ainda dentro do mesmo campo, vamos percorrer as suas mensagens
      // .push() => adiciona a mensagem de erro no final do array messages
      for (const message of body.errors[field]) {
        messages.push(message);
      }
    }

    // Se hasTechnicalError for true, colocamos a msg de erro de formato inválido
    if (hasTechnicalError) {
      return 'Algum dado foi informado em formato inválido.';
    }

    // Se tiver mais de uma msg de erro, vamos transformar esse array de mensagens de erro
    // em uma string, se tiver mais de una msg, ele vai colocar um espaço no final de cada
    // mensagem: Erro 1 Erro 2 Erro 3
    if (messages.length > 0) {
      return messages.join(' ');
    }
  }

  // Se o tipo do body for uma string
  if (typeof body === 'string' && body) {
    return body;
  }

  // Se tiver o campo message no body, retorne message
  if (body?.message) {
    return body.message;
  }

  // Se tiver o campo detail no body, retorne message
  if (body?.detail) {
    return body.detail;
  }

  return 'Ocorreu um erro inesperado';
}
