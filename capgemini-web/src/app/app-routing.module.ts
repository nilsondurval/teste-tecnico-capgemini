import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ImportacaoSingleComponent } from './pages/importacao-single/importacao-single.component';
import { ImportacaoUploadComponent } from './pages/importacao-upload/importacao-upload.component';

const routes: Routes = [
  { path: '', component: ImportacaoUploadComponent },
  { path: 'importacao/:id', component: ImportacaoSingleComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
