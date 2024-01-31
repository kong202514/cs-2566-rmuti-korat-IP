import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_modules/Member';
import { PaginationResult } from '../_modules/pagination ';
import { map, of, take } from 'rxjs';
import { UserParams } from '../_models/userParams ';
import { User } from '../_models/user';
import { AccountService } from './account.service';
import { getPaginationHeaders, getPaginationResult } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  // userParams: UserParams | undefined
  user: User | undefined
  memberCache = new Map()


  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) {//, private accountService: AccountService

  }


  // getUserParams() {
  //   return this.userParams
  // }
  // setUserParams(params: UserParams) {
  //   this.userParams = params
  // }


  private _key(userParams: UserParams) {
    return Object.values(userParams).join('_');
  }

  // paginationResult: PaginationResult<Member[]> = new PaginationResult<Member[]>

  getMembers(userParams: UserParams) {
    const key = this._key(userParams)
    const response = this.memberCache.get(key)
    if (response) return of(response)

    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize)
    params = params.append('minAge', userParams.minAge)
    params = params.append('maxAge', userParams.maxAge)
    params = params.append('gender', userParams.gender)
    params = params.append('orderBy', userParams.orderBy)
    const url = this.baseUrl + 'users'
    return getPaginationResult<Member[]>(url, params, this.http)
  }

  addLike(username: string) {
    return this.http.post(this.baseUrl + 'likes/' + username, {})
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number) {
    let params = getPaginationHeaders(pageNumber, pageSize)
    params = params.append('predicate', predicate)
    const url = this.baseUrl + 'likes'
    return getPaginationResult<Member[]>(url, params, this.http)
  }






  getMember(username: string) {

    const cache = [...this.memberCache.values()]

    const members = cache.reduce((arr, item) => arr.concat(item.result), [])

    const member = members.find((member: Member) => member.userName === username)

    if (member) return of(member)

    return this.http.get<Member>(this.baseUrl + 'users/username/' + username)
  }


}
