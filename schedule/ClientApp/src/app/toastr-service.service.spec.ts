import { TestBed, inject } from '@angular/core/testing';

import { ToastrServiceService } from './toastr-service.service';

describe('ToastrServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ToastrServiceService]
    });
  });

  it('should be created', inject([ToastrServiceService], (service: ToastrServiceService) => {
    expect(service).toBeTruthy();
  }));
});
