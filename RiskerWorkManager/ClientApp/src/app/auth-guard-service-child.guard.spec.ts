import { TestBed } from '@angular/core/testing';

import { AuthGuardServiceChildGuard } from './auth-guard-service-child.guard';

describe('AuthGuardServiceChildGuard', () => {
  let guard: AuthGuardServiceChildGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthGuardServiceChildGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
