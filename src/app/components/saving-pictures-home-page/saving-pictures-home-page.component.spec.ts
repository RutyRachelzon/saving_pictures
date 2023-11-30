import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SavingPicturesHomePageComponent } from './saving-pictures-home-page.component';

describe('SavingPicturesHomePageComponent', () => {
  let component: SavingPicturesHomePageComponent;
  let fixture: ComponentFixture<SavingPicturesHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SavingPicturesHomePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SavingPicturesHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
