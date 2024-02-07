import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Message } from "../_modules/message ";




@Injectable({
    providedIn: 'root'
})
export class MessageService {
    http: HttpClient | undefined;

    constructor() { }

    setMainPhoto(photoId: number) {
        const endpoint = this.baseUrl + 'users/set-main-photo/' + photoId
        return this.http.put(endpoint, {})
    }

    getMessagesThread(username: string) {
        const url = this.baseUrl + 'messages/thread/' + username;
        return this.http.get<Message[]>(url);
    }
}
