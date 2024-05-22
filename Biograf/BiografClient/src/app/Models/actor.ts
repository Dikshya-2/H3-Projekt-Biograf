import { identifierName } from "@angular/compiler";
import { Movie } from "./movie";
export class Actor{
  id:number=0;
  name: string='';
  age:number=0;
  movie?:Movie[];
}
