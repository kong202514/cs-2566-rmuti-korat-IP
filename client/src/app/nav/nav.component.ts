import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: { username: string | undefined, password: string | undefined } = {
    username: undefined,
    password: undefined
  }

  currentUser$: Observable<User | null> = of(null) // isLogin = false



  logout() {
    this.accountService.logout()
    // this.isLogin = false
  }

  constructor(private toastr: ToastrService, private router: Router, public accountService: AccountService) { }

  ngOnInit(): void {
    // this.getCurrentUser()
    this.currentUser$ = this.accountService.currentUser$
  }
  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => console.log(user), // user?true:false
      error: err => console.log(err)
    })
  }

  login(): void {
    this.accountService.login(this.model).subscribe({ //Observable
      next: () => {
        // console.log(response)
        this.router.navigateByUrl('/members')
      },
      error: err => this.toastr.error(err.error)//console.log(err) //anything that's not in 200 range of HTTP status
    })
  }



}
