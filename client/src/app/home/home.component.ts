import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {



  cancelRegister(event: boolean) {
    this.regisMode = !event
  }
  regisMode = false

  users: any;

  regisToggle() {
    this.regisMode = !this.regisMode
  }


  // private getuser() {
  //   this.http.get('https://localhost:7777/api/users').subscribe({
  //     next: (response: any) => this.users = response,
  //     error: (err: any) => console.log(err),
  //     complete: () => console.log('request completed')
  //   });
  // }
}
