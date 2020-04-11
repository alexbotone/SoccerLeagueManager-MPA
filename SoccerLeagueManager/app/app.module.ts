import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.components';
import { routing } from './app.routing';
import { HomeComponent } from './components/home.component';
import { LeagueComponent } from './components/league.component';
import { TeamComponent } from './components/team.component';
import { PlayerComponent } from './components/player.component';
import { SoccerMatchComponent } from './components/soccermatch.component';

import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';

import { UserService } from './service/user.service';

@NgModule({
    imports: [BrowserModule, ReactiveFormsModule, HttpModule, routing, Ng2Bs3ModalModule],
    declarations: [AppComponent, HomeComponent, LeagueComponent, TeamComponent, PlayerComponent, SoccerMatchComponent],
    providers: [{ provide: APP_BASE_HREF, useValue: '/' }, UserService],
    bootstrap: [AppComponent]

})
export class AppModule { }