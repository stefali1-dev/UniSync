import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Chat } from './chat.component';
import { randFullName } from '@ngneat/falso';
import { StudentService } from '../../_services/student.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  public connection : signalR.HubConnection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7165/chat")
  .configureLogging(signalR.LogLevel.Information)
  .build();

  public messages$ = new BehaviorSubject<any>([]);
  public connectedUsers$ = new BehaviorSubject<string[]>([]);
  public messages: any[] = [];
  public users: string[] = [];
  public rooms: string[] = [];

  chats: Chat[] = [
    {
      id: '1A1',
      imageUrl: '/assets/img/avatars/2.jpg',
      name: '1A1 Group',
      lastMessage: '',
      unreadCount: 0,
      timestamp: '3 minutes ago'
    }
  ];

  drawerOpen = new BehaviorSubject<boolean>(false);
  drawerOpen$ = this.drawerOpen.asObservable();

  constructor(private studentService: StudentService) {
    this.start();
    this.connection.on("ReceiveMessage", (user: string, message: string, messageTime: string)=>{
      this.messages = [...this.messages, {user, message, messageTime} ];
      this.messages$.next(this.messages);
    });

    this.connection.on("ConnectedUser", (users: any)=>{
      this.connectedUsers$.next(users);
    });

    // TODO: remove hardcode
    this.rooms.push('1A1')

    this.studentService.getStudentsByGroup('1A1').subscribe({
      next: data => {
        console.log(data.students);
      },
      error: err => {
        console.log(err);
      }
    });
   }

  //start connection
  public async start(){
    try {
      await this.connection.start();
      console.log("Connection is established!")
    } catch (error) {
      console.log(error);
    }
  }

  //Join Room
  public async joinRoom(user: string, room: string){
    return this.connection.invoke("JoinRoom", {user, room})
  }


  // Send Messages
  public async sendMessage(message: string){
    return this.connection.invoke("SendMessage", message)
  }

  //leave
  public async leaveChat(){
    return this.connection.stop();
  }

  getChat(chatId: string): Chat | undefined {
    return this.chats.find((chat) => chat.id === chatId);
  }

}
