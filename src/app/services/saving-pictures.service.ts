import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, catchError, map, throwError } from 'rxjs';
import { Picture } from '../models/picture';

@Injectable({
  providedIn: 'root'
})
export class SavingPicturesService {

  constructor(private http: HttpClient) { }
  private baseUrl = "https://localhost:44332/api/SavingPictures";

  getCollectionName(collectionId: string): Observable<string> {
    return this.http.get<string>(this.baseUrl + "?collectionId=" + collectionId);
  }

  addNewPictures(picturesToAdd: Picture[]): Observable<string> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<string>(this.baseUrl, JSON.stringify(picturesToAdd), {
      headers, observe: 'response', 
      responseType: 'text' as 'json'
    }).pipe(
      map((response: HttpResponse<string>) => response.body as string),
      catchError((error: any) => {
        console.error('An error occurred:', error);
        return throwError('Something went wrong. Please try again later.');
      })
    );
}
}
