import { Component } from "@angular/core"
import { UserService } from "./service/user.service";

@Component({
    selector: "user-app",
    providers: [UserService],
    template:
        ` <div>
               <nav class='navbar navbar-inverse'>
               <div class='container-fluid'>
                 <ul class='nav navbar-nav'>
                 <li><a [routerLink]="['home']">Home</a></li>
                 <li><a [routerLink]="['leagues']">Leagues</a></li>
                 <li><a [routerLink]="['teams']">Teams</a></li>
                 <li><a [routerLink]="['players']">Players</a></li>
                 <li><a [routerLink]="['soccermatches']">SoccerMatches</a></li>
                 </ul>
              </div>
              </nav>
              <div class='container'>
                 <router-outlet></router-outlet>
              </div>
         </div> `
})
export class AppComponent {

}