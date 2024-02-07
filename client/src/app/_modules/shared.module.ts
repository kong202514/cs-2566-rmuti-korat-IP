import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ButtonsModule } from 'ngx-bootstrap/buttons'
import { FileUploadModule } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination'
import { TimeagoModule } from "ngx-timeago";

@NgModule({
  declarations: [],
  imports: [
    TimeagoModule.forRoot(),
    CommonModule,
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({ positionClass: 'top-right', }),
    ButtonsModule.forRoot(),
    TabsModule.forRoot(),
    NgxSpinnerModule.forRoot({ type: 'square-spin' }),
    FileUploadModule,
    BsDatepickerModule.forRoot(), FileUploadModule

  ],
  exports: [BsDropdownModule, TabsModule, ToastrModule, NgxSpinnerModule, PaginationModule, BsDatepickerModule, FileUploadModule, ButtonsModule, TimeagoModule, FileUploadModule],
})
export class SharedModule { }
