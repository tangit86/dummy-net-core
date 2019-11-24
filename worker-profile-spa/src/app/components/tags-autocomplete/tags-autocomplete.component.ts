import {
  Component,
  OnInit,
  Inject,
  Input,
  Output,
  EventEmitter
} from "@angular/core";

import { Observable } from "rxjs";
import { Skill } from "../../model/skill";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";

@Component({
  selector: "app-tags-autocomplete",
  templateUrl: "./tags-autocomplete.component.html",
  styleUrls: ["./tags-autocomplete.component.css"]
})
export class TagsAutocompleteComponent implements OnInit {
  @Output() skillsChanged = new EventEmitter<Array<Skill>>();
  @Input() isOnlyAuto: boolean;
  @Input() skills: Array<Skill>;

  _skills = [];
  constructor(@Inject("apiService") private api) {
    if (this.skills !== undefined) {
      this._skills = this.skills;
    }
  }
  ngOnInit() {}

  clear() {
    this._skills = [];
    this.clear();
  }
  public request = (text: string): Observable<Response> => {
    return this.api.getSkills$(text);
  };

  public onAdd(item) {
    this._skills.push(item);
    this.skillsChanged.emit(this._skills);
  }

  public onRemove(item) {
    var result = this._skills.filter(function(obj) {
      console.log("filtering out skills", obj, item);
      return obj.id !== item.id;
    });

    console.log(result);
    this._skills = result;
    this.skillsChanged.emit(this._skills);
  }
}
