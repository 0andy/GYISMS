import { AppComponentBase } from '@shared/app-component-base';
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { SettingsService } from '@delon/theme';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { AppAuthService } from '@shared/auth/app-auth.service';
import { ChangePasswordComponent } from '../../change-password/change-password.component';

@Component({
  selector: 'header-user',
  // template: `
  // <nz-dropdown nzPlacement="bottomRight">
  //   <div class="item d-flex align-items-center px-sm" nz-dropdown>
  //     <nz-avatar [nzSrc]="" nzSize="small" class="mr-sm"></nz-avatar>

  //   </div>
  //   <div nz-menu class="width-sm">
  //     <div nz-menu-item [nzDisabled]="true"><i class="anticon anticon-user mr-sm"></i>个人中心</div>
  //     <div nz-menu-item [nzDisabled]="true"><i class="anticon anticon-setting mr-sm"></i>设置</div>
  //     <li nz-menu-divider></li>
  //     <div nz-menu-item (click)="logout()"><i class="anticon anticon-setting mr-sm"></i> {{l('Logout')}}</div>
  //   </div>
  // </nz-dropdown>
  // `,
  template: `
  <nz-dropdown nzPlacement="bottomRight">
    <div class="item d-flex align-items-center px-sm" nz-dropdown>
      <nz-avatar [nzSrc]="avatar" nzSize="small" class="mr-sm"></nz-avatar>
    </div>
     <div nz-menu class="width-sm">
       <div nz-menu-item (click)="changePassword()"><i class="anticon anticon-user mr-sm"></i>修改密码</div>
       <li nz-menu-divider></li>
       <div nz-menu-item (click)="logout()"><i class="anticon anticon-setting mr-sm"></i>退出登录</div>
     </div>
  </nz-dropdown>
  <change-password-modal #changePasswordModal (modalSave)="callBack()"></change-password-modal>
  `,
})
export class HeaderUserComponent extends AppComponentBase implements OnInit {
  @ViewChild('changePasswordModal') changePasswordModal: ChangePasswordComponent;
  avatar = '';

  constructor(injector: Injector, private _authService: AppAuthService) {
    super(injector);
  }
  ngOnInit(): void {
    this.avatar = this.appSession.user.avatar;
  }
  logout(): void {
    this._authService.logout();
  }
  changePassword() {
    this.changePasswordModal.show();
  }

  callBack(): void {
  }
}
