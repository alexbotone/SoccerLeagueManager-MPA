import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home.component';

import { LeagueComponent } from './components/league.component';
import { TeamComponent } from './components/team.component';
import { PlayerComponent } from './components/player.component';
import { SoccerMatchComponent } from './components/soccermatch.component';


const appRoutes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'leagues', component: LeagueComponent },
    { path: 'teams', component: TeamComponent },
    { path: 'players', component: PlayerComponent },
    { path: 'soccermatches', component: SoccerMatchComponent }

];
export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);