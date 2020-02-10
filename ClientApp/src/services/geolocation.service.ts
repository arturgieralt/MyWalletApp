import { Observable } from "rxjs";
import { Injectable } from "@angular/core";


@Injectable({
    providedIn: 'root'
})
export class GeoLocationService {

    public getCurrentPosition(): Observable <Position> {
        return  Observable.create(observer => {
            navigator.geolocation.getCurrentPosition(
                position => {
                    observer.next(position);
                    observer.complete();
                },
                observer.error.bind(observer)
            );
        });
        
    }
}