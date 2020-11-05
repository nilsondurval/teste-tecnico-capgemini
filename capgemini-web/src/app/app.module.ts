import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 

import { PanelModule } from 'primeng/panel';
import { FileUploadModule } from 'primeng/fileupload';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';

import { ImportacaoService } from './services/importacao.service';
import { HttpClientModule } from '@angular/common/http';
import { ImportacaoUploadComponent } from './pages/importacao-upload/importacao-upload.component';
import { MessageService } from 'primeng/api';
import { ImportacaoSingleComponent } from './pages/importacao-single/importacao-single.component';
import { ImportacaoListComponent } from './pages/importacao-list/importacao-list.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    ImportacaoUploadComponent,
    ImportacaoListComponent,
    ImportacaoSingleComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    PanelModule,
    FileUploadModule,
    ProgressSpinnerModule,
    TableModule,
    ToastModule
  ],
  providers: [
    ImportacaoService,
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
