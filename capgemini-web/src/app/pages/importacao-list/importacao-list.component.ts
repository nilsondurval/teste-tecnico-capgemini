import { Component, OnInit } from '@angular/core';
import { Importacao } from 'src/app/models/importacao.model';
import { ImportacaoService } from 'src/app/services/importacao.service';
import { handleErrorResponse } from 'src/app/shared/handle-error-response.function';
import { moneyMask } from 'capgemini-web-lib';
import * as moment from 'moment'
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
      { field: 'dataImportacao', header: 'Data de Importação', valueDisplayFn: i => moment(i.dataImportacao).format('DD/MM/YYYY') },
      { field: 'dataEntrega', header: 'Data de Entrega', valueDisplayFn: i => moment(i.dataEntrega).format('DD/MM/YYYY') },
      { field: 'nomeProduto', header: 'Nome do Produto' },
      { field: 'quantidade', header: 'Quantidade' },
      { field: 'valorUnitario', header: 'Valor Unitário', valueDisplayFn: i => moneyMask(i.valorUnitario) },
      { field: 'valorTotal', header: 'Valor Total', valueDisplayFn: i => moneyMask(i.valorUnitario * i.quantidade) }
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
