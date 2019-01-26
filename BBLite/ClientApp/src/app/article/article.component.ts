import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { DataService } from '../data.service';
import { article } from '../models/article';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {

  article$: Observable<article>;

  constructor(private data: DataService,
              private router: Router,
              private route: ActivatedRoute) {

  }

  ngOnInit() {
    let path = this.route.snapshot.paramMap.get('path');

    this.article$ = this.data.getArticle(path);
  }

}
