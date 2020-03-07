import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import * as signalR from "@microsoft/signalr";
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';

@Component({
  selector: 'event-notifier',
  templateUrl: './event-notifier.component.html',
  styleUrls: ['./event-notifier.component.css']
})
export class EventNotifierComponent {
  public isAuthenticated: Observable<boolean>;
  private connection: signalR.HubConnection;
  constructor(private authorizeService: AuthorizeService,
    private notificationService: ToastrService) { }

  ngOnInit() {
    this.authorizeService
    .isAuthenticated()
    .subscribe(auth => {
      if(auth) {
        this.authorizeService.getAccessToken().pipe(first()).subscribe(t => {
          this.connection = this.buildConnection(t);
          this.subscribeToEventFeed();
          this.startConnection();
        });
      }
    })
  }
  buildConnection = (t: string) => {
    return new signalR.HubConnectionBuilder()
          .withUrl("hubs/eventFeed", { accessTokenFactory: () => t })
          .build();
  }

  subscribeToEventFeed = () => {
    this.connection.on("EVENTEMITTED", (message: any) => {
      this.notificationService.info(message.message)
    });
  }

  startConnection = () => {
    this.connection.start();
  }
}
