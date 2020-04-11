"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var home_component_1 = require("./components/home.component");
var league_component_1 = require("./components/league.component");
var team_component_1 = require("./components/team.component");
var player_component_1 = require("./components/player.component");
var soccermatch_component_1 = require("./components/soccermatch.component");
var appRoutes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: home_component_1.HomeComponent },
    { path: 'leagues', component: league_component_1.LeagueComponent },
    { path: 'teams', component: team_component_1.TeamComponent },
    { path: 'players', component: player_component_1.PlayerComponent },
    { path: 'soccermatches', component: soccermatch_component_1.SoccerMatchComponent }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=app.routing.js.map