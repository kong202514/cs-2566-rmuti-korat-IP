import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_modules/Member';
import { PaginationResult } from '../_modules/pagination ';
import { map } from 'rxjs';
import { UserParams } from '../_models/userParams ';

@Injectable({
  providedIn: 'root'
})
export class MembersService {


  baseUrl = environment.apiUrl
  constructor(private http: HttpClient) { }


  // paginationResult: PaginationResult<Member[]> = new PaginationResult<Member[]>

  getMembers(userParams: UserParams) {
    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize)
    params = params.append('minAge', userParams.minAge)
    params = params.append('maxAge', userParams.maxAge)
    params = params.append('gender', userParams.gender)
    const url = this.baseUrl + 'users'
    return this.getPaginationResult<Member[]>(url, params)
  }


  private getPaginationResult<T>(url: string, params: HttpParams) {
    const paginationResult: PaginationResult<T> = new PaginationResult<T>
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body)
          paginationResult.result = response.body

        const pagination = response.headers.get('Pagination')
        if (pagination)
          paginationResult.pagination = JSON.parse(pagination)

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
    return this.http.get<Member>(this.baseUrl + 'users/username/' + username)
  }


}
