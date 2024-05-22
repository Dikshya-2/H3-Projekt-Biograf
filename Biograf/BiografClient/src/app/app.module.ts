import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TestwithFlemmingComponent } from './testwith-flemming/testwith-flemming.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AuthorComponent } from './Admin/author/author.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GenerictestActorComponent } from './generictest-actor/generictest-actor.component';
import { MovieComponent } from './Admin/movie/movie.component';
import { LanguageComponent } from './Admin/language/language.component';
import { CategoryComponent } from './Admin/category/category.component';
import { UserComponent } from './Admin/user/user.component';
import { ActorComponent } from './Admin/actor/actor.component';
import { AdminControlComponent } from './Admin/admin-control/admin-control.component';
import { MoviedetailComponent } from './Components/moviedetail/moviedetail.component';
import { LoginComponent } from './Components/login/login.component';
import { NgxMatDatetimePickerModule } from '@angular-material-components/datetime-picker';
import { RegisterationComponent } from './Components/registeration/registeration.component';
import { CategorydetailComponent } from './Components/categorydetail/categorydetail.component';
import { RandomovieComponent } from './Components/randomovie/randomovie.component';
import { MovieDescriptionComponent } from './Components/movie-description/movie-description.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { JwtInterceptor } from './Helper/jwt.interceptor';
import { LoginService } from './Services/login.service';
import { CategoriesbtnComponent } from './Components/categoriesbtn/categoriesbtn.component';

@NgModule({
  declarations: [
    AppComponent,
    TestwithFlemmingComponent,
    ActorComponent,
    HomeComponent,
    NavbarComponent,
    AuthorComponent,
    GenerictestActorComponent,
    MovieComponent,
    LanguageComponent,
    CategoryComponent,
    UserComponent,
    AdminControlComponent,
    MoviedetailComponent,
    TestwithFlemmingComponent,
    LoginComponent,
    RegisterationComponent,
    CategorydetailComponent,
    RandomovieComponent,
    MovieDescriptionComponent,
    CategoriesbtnComponent,

  ],
  imports: [

    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    NgxMatDatetimePickerModule,
    BrowserAnimationsModule,
    MatSnackBarModule,
  ],
  providers: [LoginService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
