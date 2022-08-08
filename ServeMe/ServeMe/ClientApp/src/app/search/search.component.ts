import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SearchType } from '../home/home.component';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  searchType: SearchType = SearchType.StarRating;
  type = SearchType;
  loading$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  constructor() { }

  ngOnInit(): void {
  }

}
