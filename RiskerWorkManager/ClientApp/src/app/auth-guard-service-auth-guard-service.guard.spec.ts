import { TestBed } from '@angular/core/testing';

import { AuthGuardServiceAuthGuardServiceGuard } from './auth-guard-service-auth-guard-service.guard';

describe('AuthGuardServiceAuthGuardServiceGuard', () => {
  let guard: AuthGuardServiceAuthGuardServiceGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthGuardServiceAuthGuardServiceGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
