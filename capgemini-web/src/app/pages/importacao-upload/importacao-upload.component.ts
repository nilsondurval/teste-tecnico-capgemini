import { Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { FileUpload } from 'primeng/fileupload';
import { ImportacaoService } from 'src/app/services/importacao.service';
import { handleErrorResponse } from 'src/app/shared/handle-error-response.function';
import { ImportacaoListComponent } from '../importacao-list/importacao-list.component';

@Component({
  selector: 'app-importacao-upload',
  templateUrl: './importacao-upload.component.html',
  styleUrls: ['./importacao-upload.component.css']
})
export class ImportacaoUploadComponent implements OnInit {

  files: File[] = [];

  processing: boolean;

  @ViewChild(ImportacaoListComponent, { static: true }) importacaoListComponent: ImportacaoListComponent;

  constructor(
    private importacaoService: ImportacaoService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
  }

  onSelect(file: File[], fileUpload: FileUpload): void {
    this.processing = true;
    this.importacaoService.upload(file[0]).subscribe(_ => {
      fileUpload.clear();
      this.importacaoListComponent.getImportacoes();
      this.messageService.add({ severity: 'sucess', summary: 'Sucesso!', detail: "Arquivos persistidos com sucesso", sticky: true });
      this.processing = false;
    },
    errorResponse => {
      fileUpload.clear();
      this.messageService.addAll(handleErrorResponse(errorResponse));
      this.processing = false;
    }
  );
  }
}
