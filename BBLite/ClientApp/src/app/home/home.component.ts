import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { articleReference } from '../models/articleReference';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  articles: articleReference[];
  currentPage: number;
  constructor(private data: DataService) { }

  ngOnInit() {
    this.currentPage = 1;
    this.data.get(this.currentPage)
      .subscribe(data => this.articles = data);
  }

  nextPage() {
    this.currentPage++;
    this.data.get(this.currentPage)
      .subscribe(data => this.articles = data);
  }

  prevPage() {
    this.currentPage--;
    this.data.get(this.currentPage)
      .subscribe(data => this.articles = data);
  }
}
