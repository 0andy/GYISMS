import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { AppComponentBase } from '@shared/app-component-base';
import { ChangePasswordComponent } from '../../change-password/change-password.component';

@Component({
  selector: 'abp-sidebar-user',
  templateUrl: './sidebar-user.component.html',
  styles: [],
})
export class SidebarUserComponent extends AppComponentBase implements OnInit {
  @ViewChild('changePasswordModal') changePasswordModal: ChangePasswordComponent;
  shownLoginName = '';
  emailAddress = '';
  avatar = '';

  constructor(injector: Injector, private authService: AppAuthService) {
    super(injector);
  }

  ngOnInit() {
    this.shownLoginName = this.appSession.user.name; //this.appSession.getShownLoginName();
    this.emailAddress = this.appSession.user.emailAddress;
    this.avatar = this.appSession.user.avatar;
  }

  logout() {
    this.authService.logout();
  }

  changePassword() {
    this.changePasswordModal.show();
  }

  callBack(): void {
  }
}
