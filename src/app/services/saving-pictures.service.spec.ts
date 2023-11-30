import { TestBed } from '@angular/core/testing';

import { SavingPicturesService } from './saving-pictures.service';

describe('SavingPicturesService', () => {
  let service: SavingPicturesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SavingPicturesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
