import { Component, Input } from '@angular/core';
import { faStar, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { Member } from 'src/app/_modules/Member';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent {
  aTrashCan = faTrashCan
  faStar = faStar
  @Input() member: Member | undefined
}