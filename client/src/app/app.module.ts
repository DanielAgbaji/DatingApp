import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { CrisisListComponent } from './crisis-list/crisis-list.component';
import { HeroesListComponent } from './heroes-list/heroes-list.component';
import { RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthService } from './auth.service';
import { CustomerGuardGuard } from './customer-guard.guard';
@NgModule({
  declarations: [
    AppComponent,
    CrisisListComponent,
    HeroesListComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      {path:'grocery-list', component: CrisisListComponent},
      {path: 'heroe-list', component: HeroesListComponent, canActivate: [CustomerGuardGuard]},
      {path: '', redirectTo:'/heroe-lis', pathMatch:'full'},
      {path: '**', component: PageNotFoundComponent}

    ])

  ],
  providers: [AuthService, CustomerGuardGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
