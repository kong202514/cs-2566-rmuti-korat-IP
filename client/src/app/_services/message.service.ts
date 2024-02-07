import { Injectable } from '@angular/core';
import { Message } from '../_modules/message ';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  http: HttpClient | undefined;
  baseUrl: string | undefined;

  constructor() { }

  sendMessage(recipientUsername: string, content: string) {
    const url = this.baseUrl + 'messages'
    const body = { recipientUsername, content } //ต้องสะกดตรงกับ CreateMessageDto.cs
    return this.http.post<Message>(url, body)
  }

  getMessagesThread(username: string) {
    const url = this.baseUrl + 'messages/thread/' + username
    return this.http!.get<Message[]>(url)
  }
}
