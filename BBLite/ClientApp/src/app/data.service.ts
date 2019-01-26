import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {articleReference} from './models/articleReference'
import { article } from './models/article';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  get(page: number): Observable<articleReference[]> {
    return this.http.get<articleReference[]>('/api/news/articles?page=' + page);
  }

  getArticle(path: string): Observable<article> {
    return this.http.get<article>('/api/news/articles/' + path);
  }
}
