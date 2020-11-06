import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class FormDataService {

  createFormData<T>(jsonObject: T, jsonObjectName: string, files: File[]) {
    const formData = new FormData();
    formData.append(jsonObjectName, JSON.stringify(jsonObject));
    files.forEach(file => formData.append(file.name, file, file.name));
    return formData;
  }
}
