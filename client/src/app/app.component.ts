import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { faBell } from '@fortawesome/free-solid-svg-icons'
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'gong';
  users: any
  faBell = faBell
  constructor(private http: HttpClient, private accountService: AccountService) { }

  setCurrentUser() {
    const userString = localStorage.getItem('user')
    if (!userString) return
    const user: User = JSON.parse(userString)
    this.accountService.setCurrentUser(user)
  }

  ngOnInit(): void {
    // this.getuser();

    this.setCurrentUser()
  }




}