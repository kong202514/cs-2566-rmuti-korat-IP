import { Router } from '@angular/router';
import { AccountService } from './../_services/account.service';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  model: any = {}
  // @Input() usersFromHomeCpmponent: any
  @Output() isCancel = new EventEmitter()

  constructor(private toastr: ToastrService, private accountService: AccountService, private router: Router) { }


  register() {

    console.log(this.model)
    this.accountService.register(this.model).subscribe(
      {
        error: err => this.toastr.error(err.error),
        next: _ => this.router.navigateByUrl('/members')//this.cancel()




      }

    )

  }

  cancel() {
    console.log('cancel')

    this.isCancel.emit(true)
  }
}
