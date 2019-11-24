import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TagsAutocompleteComponent } from './tags-autocomplete.component';

describe('TagsAutocompleteComponent', () => {
  let component: TagsAutocompleteComponent;
  let fixture: ComponentFixture<TagsAutocompleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TagsAutocompleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TagsAutocompleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
