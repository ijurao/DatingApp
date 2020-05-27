import { Component, OnInit, Input } from '@angular/core';
import { Photo } from '../_models/Photo';

@Component({
  selector: 'app-galeery',
  templateUrl: './galeery.component.html',
  styleUrls: ['./galeery.component.css']
})
export class GaleeryComponent implements OnInit {
  
  @Input() images: Photo[];

  constructor() { }
   
  ngOnInit(): void {
  }

}
