﻿import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  ExternalLoginProviderInfoModel,
  ExternalAuthenticateModel,
} from '@shared/service-proxies/service-proxies';

import {
  TokenAuthServiceProxy,
  AuthenticateModel,
  AuthenticateResultModel,
} from '@shared/service-proxies/service-proxies';
import { UrlHelper } from '@shared/helpers/UrlHelper';
import { AppConsts } from '@shared/AppConsts';

import * as _ from 'lodash';
import { UtilsService } from 'abp-ng2-module/dist/src/utils/utils.service';
import { MessageService } from 'abp-ng2-module/dist/src/message/message.service';
import { TokenService } from 'abp-ng2-module/dist/src/auth/token.service';
import { LogService } from 'abp-ng2-module/dist/src/log/log.service';
import { AppSessionService } from '@shared/session/app-session.service';

@Injectable()
export class LoginService {
  static readonly twoFactorRememberClientTokenName =
    'TwoFactorRememberClientToken';

  authenticateModel: AuthenticateModel;
  authenticateResult: AuthenticateResultModel;

  rememberMe: boolean;

  constructor(
    private _tokenAuthService: TokenAuthServiceProxy,
    private _router: Router,
    private _utilsService: UtilsService,
    //private _messageService: MessageService,
    private _tokenService: TokenService,
    private _logService: LogService,
    //private _appSessionService: AppSessionService,
  ) {
    this.clear();
  }

  authenticate(finallyCallback?: () => void): void {
    finallyCallback = finallyCallback || (() => { });

    this._tokenAuthService
      .authenticate(this.authenticateModel)
      .finally(finallyCallback)
      .subscribe((result: AuthenticateResultModel) => {
        this.processAuthenticateResult(result);
      });
  }

  private processAuthenticateResult(
    authenticateResult: AuthenticateResultModel,
  ) {
    this.authenticateResult = authenticateResult;

    if (authenticateResult.accessToken) {
      // Successfully logged in
      // tslint:disable-next-line:max-line-length
      this.login(
        authenticateResult.accessToken,
        authenticateResult.encryptedAccessToken,
        authenticateResult.expireInSeconds,
        this.rememberMe,
      );
    } else {
      // Unexpected result!

      this._logService.warn('Unexpected authenticateResult!');
      this._router.navigate(['account/login']);
    }
  }

  private login(
    accessToken: string,
    encryptedAccessToken: string,
    expireInSeconds: number,
    rememberMe?: boolean,
  ): void {
    const tokenExpireDate = rememberMe
      ? new Date(new Date().getTime() + 1000 * expireInSeconds)
      : undefined;

    this._tokenService.setToken(accessToken, tokenExpireDate);

    this._utilsService.setCookieValue(
      AppConsts.authorization.encrptedAuthTokenName,
      encryptedAccessToken,
      tokenExpireDate,
      abp.appPath,
    );

    let initialUrl = UrlHelper.initialUrl;
    //alert(initialUrl)
    // if (initialUrl.indexOf('/login') > 0) {
    if (initialUrl.indexOf('/login') > 0 || initialUrl.indexOf('/#/') > 0) {
      initialUrl = AppConsts.appBaseUrl;
    }
    //alert(initialUrl)
    //let roles = this._appSessionService.roles;
    //console.log(roles);

    //if (roles.length == 1 && roles[0] == 'EnterpriseAdmin') { //如果用户只是企管人员 将没有首页
    //  location.href = AppConsts.appBaseUrl + '/#/app/doc/document';
    //}
    //else {
    location.href = initialUrl;
    //}

  }

  private clear(): void {
    this.authenticateModel = new AuthenticateModel();
    this.authenticateModel.rememberClient = false;
    this.authenticateResult = null;
    this.rememberMe = false;
  }
}
