import { Component, Input } from '@angular/core';
import { faClock, faPaperPlane } from '@fortawesome/free-regular-svg-icons';
import { Message } from 'src/app/_modules/message ';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css'],

})
export class MemberMessagesComponent {
  @Input() username?: string
  @Input() messages: Message[] = []
  faClock = faClock
  faPaperPlane = faPaperPlane


  messageContent = ''
  constructor(private messageService: MessageService) { }

  loadMessages() {
    if (!this.username) return

    this.messageService.getMessagesThread(this.username).subscribe({
      next: (response: Message[]) => this.messages = response
    })
  }
  sendMessage() {
    if (!this.username) return
    this.messageService.sendMessage(this.username, this.messageContent).subscribe({
      next: response => {
        this.messages.push(response)
        this.messageForm?.reset()
      }
    })
  }
  ngOnInit(): void {
    this.loadMessages()
  }
}
