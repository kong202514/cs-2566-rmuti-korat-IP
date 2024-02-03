import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs'
import { PaginationModule } from 'ngx-bootstrap/pagination'
import { TimeagoModule } from 'ngx-timeago';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot(),
    TabsModule.forRoot(),
    PaginationModule.forRoot(),
    // ButtonsModule.forRoot(),
    TimeagoModule.forRoot(),
    NgxSpinnerModule.forRoot({ type: 'square-spin' }),
    FileUploadModule,
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    PaginationModule,
    // ButtonsModule,
    TimeagoModule,
    NgxSpinnerModule,
    FileUploadModule,
  ]
})
export class SharedModule { }
