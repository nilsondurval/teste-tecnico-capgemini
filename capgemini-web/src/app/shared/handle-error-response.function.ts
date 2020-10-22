import { ErroEnum } from '../models/enum/erro-enum';

export function handleErrorResponse(errorResponse): any {
  const msgs = [];
  if (errorResponse.error && errorResponse.error.errors) {
    for (const error of errorResponse.error.errors) {
      switch (error.codigo) {
        case ErroEnum.ERROR:
          msgs.push({ severity: 'error', summary: 'Erro!', detail: error.message, sticky: true });
          break;
      }
    }

    return msgs;
  } else {
    throw errorResponse;
  }
}
