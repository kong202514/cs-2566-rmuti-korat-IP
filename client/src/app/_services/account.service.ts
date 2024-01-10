import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment'
import { Member } from '../_modules/Member'

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl // 'https://localhost:7777/api/'


  private currentUserSource = new BehaviorSubject<User | null>(null)
  currentUser$ = this.currentUserSource.asObservable()

  constructor(private http: HttpClient) { }

  register(model: any) {
    return this.http.post<User>(`${this.baseUrl}account/register`, model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUserSource.next(user)
        }
        // return user
      })
    )
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/username/' + username, this.getHttpOptions())
  }



  getHttpOptions() {
    const userString = localStorage.getItem('user')
    if (!userString) return
    const user: User = JSON.parse(userString)
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + user.token
      })
    }
  }



  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users', this.getHttpOptions())
  }

  login(model: any) {
    return this.http.post<User>(`${this.baseUrl}account/login`, model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUserSource.next(user)
        }
      }
      )

    )
  }

  logout() {
    localStorage.removeItem('user')
    this.currentUserSource.next(null)
  }
  setCurrentUser(user: User) {
    this.currentUserSource.next(user)
  }


}
