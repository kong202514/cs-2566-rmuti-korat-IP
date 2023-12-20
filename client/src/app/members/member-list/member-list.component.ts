import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_modules/Member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members: Member[] = []

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadMember()
  }

  loadMember() {
    this.memberService.getMembers().subscribe({
      next: users => {
        console.log(users);

        this.members = users
      }

    })
  }
}