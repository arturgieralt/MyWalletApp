import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import * as signalR from "@microsoft/signalr";
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public isAuthenticated: Observable<boolean>;
  private connection: signalR.HubConnection;
  constructor(private authorizeService: AuthorizeService) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.isAuthenticated.subscribe(auth => {
      if(auth) {
        this.authorizeService.getAccessToken().subscribe(t => {
          this.connection = new signalR.HubConnectionBuilder()
          .withUrl("hubs/eventFeed", { accessTokenFactory: () => t })
          .build();
      
          this.connection.on("EVENTEMITTED", (username: string, message: object) => {
            console.log('EMITTED', username, message);
          });
      
        });
      }
    })
  }
  connect() {
    this.connection.start().catch(err => console.log(err));
  }
}
