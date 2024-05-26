import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from "../../environments/environment";

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export class ChannelService {
  constructor(private http: HttpClient) {}


  getChannelsByUserId(request: string): Observable<any> {
    return this.http.get(API_URL + `/Channel/ByUserId/${request}`);
  }

  createChannel(channelName: string, chatUserIds: string[]): Observable<any>{
    return this.http.post(
      API_URL + '/Channel',
      {
        channelName,
        chatUserIds
      },
      {responseType: 'text'}
    );
  }
}