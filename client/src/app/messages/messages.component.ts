import { Component } from '@angular/core';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages?: Message[]
  pagination?: Pagination
  label = 'Unread'  // 'Inbox'
  pageNumber = 1
  pageSize = 5
  loading = false
  constructor(private messageService: MessageService) { }
  ngOnInit(): void {
    this.loadMessage()
  }
  loadMessage() {
    this.loading = true
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.label).subscribe({
      next: response => {
        this.messages = response.result
        this.pagination = response.pagination
        this.loading = false
      }
    })
  }
  pageChanged(event: any) {
    if (this.pageNumber === event.page) return
    this.pageNumber = event.page
    this.loadMessage()
  }
}
