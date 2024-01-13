import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { map } from 'rxjs';
import { AccountService } from '../account/account.service';

export const nonAuthGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);

  return accountService.currentUser$.pipe(
    map(user => {
      return user ? false : true;
    })
  )
};
