import { TestBed, inject } from '@angular/core/testing';

import { FloorServiceService } from './floor-service.service';

describe('FloorServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FloorServiceService]
    });
  });

  it('should be created', inject([FloorServiceService], (service: FloorServiceService) => {
    expect(service).toBeTruthy();
  }));
});
