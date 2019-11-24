import {
  Component,
  OnInit,
  Inject,
  EventEmitter,
  Output,
  Input
} from "@angular/core";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";
import { Location } from "../../model/location";
import { ApiService } from "../../services/api.service";

@Component({
  selector: "app-locations-autocomplete",
  templateUrl: "./locations-autocomplete.component.html",
  styleUrls: ["./locations-autocomplete.component.css"]
})
export class LocationsAutocompleteComponent implements OnInit {
  keyword = "name";
  data = [];

  private _initLocation: Location;
  @Input() set initLocation(value: Location) {
    this._initLocation = value;
    console.log("init location arrived", this.initLocation);
    this.initialValue = this._initLocation.address;
  }
  get initLocation(): Location {
    return this._initLocation;
  }
  @Output() locationChanged = new EventEmitter<Location>();

  curLocation: Location;
  initialValue: string;
  constructor(private api: ApiService) {}
  ngOnInit(): void {}

  selectEvent(item) {
    console.log(item, "new loc selected");
    this.curLocation = item.payload[0];
    this.locationChanged.emit(item.payload[0]);
  }

  onChangeSearch(val: string) {
    if (val === "") {
      this.selectEvent({ payload: [new Location("", 0.0, 0.0)] });
      return;
    }
    this.api.getLocations$(val).subscribe(res => {
      this.data = [];
      res.forEach(element => {
        this.data.push({
          id: element.address,
          name: element.address,
          payload: res
        });
      });
    });
  }

  onFocused(e) {
    // do something when input is focused
  }
}
