import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Importacao } from '../models/importacao.model';
import { FormDataService } from './form-data.service';

@Injectable()
export class ImportacaoService {

  public importacoes: Importacao[];

  constructor(
    private httpClient: HttpClient,
    private formDataService: FormDataService
  ) { }

  upload(excel: File) {
    const formData = this.formDataService.createFormData<any>({}, 'importacoes', [ excel ]);
    return this.httpClient.post(`${environment.api}/importacoes/upload`, formData);
  }

  getAll(): Observable<Importacao[]> {
    return this.httpClient.get<Importacao[]>(`${environment.api}/importacoes`);
  }

  getById(id: number): Observable<Importacao> {
    return this.httpClient.get<Importacao>(`${environment.api}/importacoes/${id}`);
  }
}
