import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthorComponent } from './Admin/author/author.component';
import { CategoryComponent } from './Admin/category/category.component';
import { LanguageComponent } from './Admin/language/language.component';
import { UserComponent } from './Admin/user/user.component';
import { ActorComponent } from './Admin/actor/actor.component';
import { MovieComponent } from './Admin/movie/movie.component';
import { MoviedetailComponent } from './Components/moviedetail/moviedetail.component';
import { LoginComponent } from './Components/login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RandomovieComponent } from './Components/randomovie/randomovie.component';
import { CategorydetailComponent } from './Components/categorydetail/categorydetail.component';
import { MovieDescriptionComponent } from './Components/movie-description/movie-description.component';
import { authGuard } from './Helper/auth.guard';
import { RegisterationComponent } from './Components/registeration/registeration.component';
import { AdminControlComponent } from './Admin/admin-control/admin-control.component';
import { TestComponent } from './Components/testmovie/test.component';
import { CategoriesbtnComponent } from './Components/categoriesbtn/categoriesbtn.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'admin/actor', component: ActorComponent, canActivate: [authGuard]},
  { path: 'admin/author', component: AuthorComponent, canActivate: [authGuard]},
  {path: 'admin/category', component : CategoryComponent, canActivate: [authGuard]},
  {path: 'admin/language', component : LanguageComponent, canActivate: [authGuard]},
  {path: 'admin/movie', component : MovieComponent, canActivate: [authGuard]},
  {path: 'admin/user', component : UserComponent, canActivate: [authGuard]},
  {path: 'admin', component : AdminControlComponent, canActivate: [authGuard]},
  {path: 'moviedetails', component : MoviedetailComponent},
  {path: 'login', component : LoginComponent},
  {path: 'registration', component : RegisterationComponent},
  {path: 'randomovie', component: RandomovieComponent},
  {path: 'categorydetail/:id', component: CategorydetailComponent},
  {path: 'movie/:id', component: MovieDescriptionComponent},
  {path: 'test', component: TestComponent},
  {path: 'moviedetails/:id', component: MoviedetailComponent},
  {path: 'categoriesbtn', component : CategoriesbtnComponent},
  // {path: 'moviedescription/id', component: MovieDescriptionComponent},
  // { path: 'admin/category', component: CategoryComponent, canActivate: [AdminGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
