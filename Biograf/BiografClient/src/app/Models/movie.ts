import { Actor } from "./actor";
import { Category } from "./category";
import { Language } from "./language";
import { Photo } from "./photo";

export class Movie{
  id:number=0;
  title: string='';
  description:string='';
  duration:number=0;
  releasedDate?: Date = new Date();
  // releasedDate=Date;
  authorId?:number=0;
  userId?:number=0;
  language?:Language[];
  actor?:Actor[];
  categories?:Category[];
  photo?: Photo[];
}

export class MovieCreateModel {
  id: number = 0;
  title: string='';
  description:string='';
  duration:number=0;
  releasedDate?: Date = new Date();
  categories?:Category[];
}
