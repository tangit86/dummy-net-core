import { Skill } from "./skill";
import { Location } from "./location";

export class Profile {
  constructor(
    public id: number,
    public name: string,
    public skills: Skill[],
    public location: Location
  ) {}
}
