import { inject, Injectable } from '@angular/core';
import { CreateUserMediaDto } from '../../models/create/CreateUserMediaDto';
import { Observable } from 'rxjs';
import { UserMediaApi } from './user-media-api';

@Injectable({
  providedIn: 'root',
})
export class UserMediaService {
  private readonly userMediaApi: UserMediaApi = inject(UserMediaApi)

  
    addItem(itemDto: CreateUserMediaDto): Observable<object> {
    console.log("adding item to the users backlog" + itemDto)
    return this.userMediaApi.addItem(itemDto);

  }
}
