import { Movie } from "./movie";

export class Author{
  id:number=0;
  name: string='';
  age:number=0;
  movie?:Movie[];
}
