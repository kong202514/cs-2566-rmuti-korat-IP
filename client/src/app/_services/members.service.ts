import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_modules/Member';
import { PaginationResult } from '../_modules/pagination ';
import { map, of, take } from 'rxjs';
import { UserParams } from '../_models/userParams ';
import { User } from '../_models/user';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  userParams: UserParams | undefined
  user: User | undefined
  memberCache = new Map()


  baseUrl = environment.apiUrl

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.userParams = new UserParams(user)
          this.user = user
        }
      }
    })
  }


  getUserParams() {
    return this.userParams
  }
  setUserParams(params: UserParams) {
    this.userParams = params
  }
  resetUserParams() {
    if (!this.user) return
    this.userParams = new UserParams(this.user)
    return this.userParams
  }

  private _key(userParams: UserParams) {
    return Object.values(userParams).join('_');
  }

  // paginationResult: PaginationResult<Member[]> = new PaginationResult<Member[]>

  getMembers(userParams: UserParams) {
    const key = this._key(userParams)
    const response = this.memberCache.get(key)
    if (response) return of(response)

    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize)
    params = params.append('minAge', userParams.minAge)
    params = params.append('maxAge', userParams.maxAge)
    params = params.append('gender', userParams.gender)
    params = params.append('orderBy', userParams.orderBy)
    const url = this.baseUrl + 'users'
    return this.getPaginationResult<Member[]>(url, params, key)
  }


  private getPaginationResult<T>(url: string, params: HttpParams, key: string | null) {
    const paginationResult: PaginationResult<T> = new PaginationResult<T>
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body)
          paginationResult.result = response.body

        const pagination = response.headers.get('Pagination')
        if (pagination)
          paginationResult.pagination = JSON.parse(pagination)
        if (key)
          this.memberCache.set(key, paginationResult)
        return paginationResult
      })
    )
  }
  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams()
    params = params.append('pageNumber', pageNumber)
    params = params.append('pageSize', pageSize)
    return params
  }



  getMember(username: string) {

    const cache = [...this.memberCache.values()]

    const members = cache.reduce((arr, item) => arr.concat(item.result), [])

    const member = members.find((member: Member) => member.userName === username)

    if (member) return of(member)

    return this.http.get<Member>(this.baseUrl + 'users/username/' + username)
  }


}
