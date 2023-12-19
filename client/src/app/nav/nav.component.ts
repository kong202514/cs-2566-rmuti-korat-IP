import { User } from './../_models/user';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, map, of } from 'rxjs';
// import { User } from '../_models/user';
import { Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  currentUser$: Observable<User | null> = of(null) // isLogin = false



  user: User | null = null;

  constructor(private toastr: ToastrService, private router: Router, public accountService: AccountService) { }


  model: { username: string | undefined, password: string | undefined } = {
    username: undefined,
    password: undefined
  }





  logout() {
    this.accountService.logout()
    // this.isLogin = false
  }


  ngOnInit(): void {
    // this.getCurrentUser()
    this.currentUser$ = this.accountService.currentUser$
    this.currentUser$.subscribe({
      next: user => this.user = user
    })



    // this.currentUser$.pipe(map(a => {
    //   this.user = { username: "", token: "" };

    // }))



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
