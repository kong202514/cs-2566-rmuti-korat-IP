import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { faBell } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'gong';
  users: any
  faBell = faBell
  constructor(private http: HttpClient) { }



  ngOnInit(): void {
    this.http.get('https://localhost:7777/api/users').subscribe({
      next: (response: any) => this.users = response,
      error: (err: any) => console.log(err),
      complete: () => console.log('request completed')
    })
  }
}