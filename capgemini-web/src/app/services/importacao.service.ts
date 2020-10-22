import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Importacao } from '../models/importacao.model';

@Injectable()
export class ImportacaoService {

  public importacoes: Importacao[];

  constructor(
    private httpClient: HttpClient,
  ) { }

  upload(excel: File) {
    const formData = new FormData();
    formData.append(excel.name, excel, excel.name);

    return this.httpClient.post(`${environment.api}/importacoes/upload`, formData);
  }

  getAll(): Observable<Importacao[]> {
    return this.httpClient.get<Importacao[]>(`${environment.api}/importacoes`);
  }

  getById(id: number): Observable<Importacao> {
    return this.httpClient.get<Importacao>(`${environment.api}/importacoes/${id}`);
  }
}
