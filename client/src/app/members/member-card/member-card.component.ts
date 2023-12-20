import { Component, Input } from '@angular/core';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { Member } from 'src/app/_modules/Member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent {

  faUser = faUser
  @Input() member: Member | undefined
  faHeart!: IconProp;
  faEnvelope!: IconProp;
}
