import { Component, OnInit } from '@angular/core';
import { Importacao } from 'src/app/models/importacao.model';
import { ImportacaoService } from 'src/app/services/importacao.service';
import { handleErrorResponse } from 'src/app/shared/handle-error-response.function';
import * as moment from 'moment'
import { moneyMask } from 'src/app/shared/money-mask.function';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-importacao-list',
  templateUrl: './importacao-list.component.html',
  styleUrls: ['./importacao-list.component.css']
})
export class ImportacaoListComponent implements OnInit {

  importacoes: Importacao[];
  cols: any;

  msgs: any[] = [];

  constructor(
    private importacaoService: ImportacaoService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.setupCols();
    this.getImportacoes();
  }

  setupCols(): void {
    this.cols = [
      { field: 'id', header: 'Codigo' },
      { field: 'dataEntrega', header: 'Data de Entrega', valueDisplayFn: i => moment(i.dataEntrega).format('DD/MM/YYYY') },
      { field: 'nomeProduto', header: 'Nome do Produto' },
      { field: 'quantidade', header: 'Quantidade' },
      { field: 'valorUnitario', header: 'Valor Unitário', valueDisplayFn: i => moneyMask(i.valorUnitario) }
    ];
  }

  getImportacoes(): void {
    this.importacaoService.getAll().subscribe(importacoes => {
        this.importacoes = importacoes;
      },
      errorResponse => {
        this.messageService.addAll(handleErrorResponse(errorResponse));
      }
    );
  }

  displayValue(col: any, obj: any) {
    if (col.valueDisplayFn) {
      return col.valueDisplayFn(obj);
    }
    return obj[col.field];
  }
}
