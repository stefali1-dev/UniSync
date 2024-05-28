import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Chat, ChatMessage} from './chat.component';
import { randFullName } from '@ngneat/falso';
import { StudentService } from '../../_services/student.service';
import { StorageService } from '../../_services/storage.service';
import { MessageService } from '../../_services/message.service';
import { UserService } from '../../_services/user.service';
import { ChannelService } from '../../_services/channel.service';



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
  public messages: ChatMessage[] = [];
  public previousMessages: ChatMessage[] = [];
  public users: string[] = [];
  public rooms: string[] = [];
  private isConnected: boolean = false;

  chats: Chat[] = [
  ];

  private chatsSubject = new BehaviorSubject<Chat[]>(this.chats);
  public chats$ = this.chatsSubject.asObservable();


  drawerOpen = new BehaviorSubject<boolean>(false);
  drawerOpen$ = this.drawerOpen.asObservable();

  constructor(
    private studentService: StudentService,
    private storageService: StorageService,
    private messageService: MessageService,
    private userService: UserService,
    private channelService: ChannelService

  ) {
    this.start();
    this.connection.on("ReceiveMessage", (userId: string, channelId:string, message: string, messageTime: string)=>{
      // TODO: remove hardcode

      console.log(`Received message: ${message}`)

      let receivedMmessage: ChatMessage = {
        id: channelId,
        senderId: userId,
        message: message,
        messageTime: messageTime
      }

      this.messages = [...this.messages, receivedMmessage ];
      this.messages$.next(this.messages);
    });

    this.connection.on("ConnectedUser", (users: any)=>{
      this.connectedUsers$.next(users);
    });

    //this.rooms.push('1A1');

    // TODO: implement getPreviousMessages() in convo component
    //this.getPreviousMessages();
    this.getChats()
   }

  //start connection
  public async start(){
    try {
      await this.connection.start();
      console.log("Connection is established!")
      let userId = this.storageService.getUser().userId
      
      if(userId === null){
        console.log("NULL USER ID")
      }

      this.channelService.getChannelsByUserId(userId).subscribe({
        next: data => {
          data.channels.forEach(channel => {
            this.joinRoom(userId, channel.channelId);
          })
        },
        error: err => {
          console.log(err)
        }
      });

      

    } catch (error) {
      console.log(error);
    }
  }

  //Join Room
  public async joinRoom(userId: string, room: string){
    return this.connection.invoke("JoinRoom", {userId, room})
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

  public async getPreviousMessages(channelId: string){
    this.clearMessageHistory()

    this.messageService.getMessagesByChannel(channelId).subscribe({
      next: data => {

        
        console.log(data.messages);

        for (const message of data.messages) {
          let chatMessage: ChatMessage = {
            id: message.messageId,
            senderId: message.chatUserId,
            message: message.content,
            messageTime: message.timestamp
          };


          this.previousMessages.push(chatMessage);
        }

        this.messages = this.previousMessages;
        this.messages$.next(this.messages);
      },
      error: err => {
        console.log(err);
      }
    });

  }

  getChats(){
    let userId = this.storageService.getUser().userId;

    this.channelService.getChannelsByUserId(userId).subscribe({
      next: data => {
        console.log(data.channels)

        data.channels.forEach(channel => {
          
          let chat: Chat = {
            id: channel.channelId,
            imageUrl: 'https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500',
            name: channel.channelName,
            lastMessage: '',
            unreadCount: 0,
            timestamp: '',
            nrOfParticipants: channel.chatUsersIds.length
          }

          // rename the chat
          if(chat.nrOfParticipants === 2 && chat.name === 'DM'){
            channel.chatUsersIds.forEach(chatUsersId => {
              let currentUserId = this.storageService.getUser().userId;

              if(chatUsersId != currentUserId){

                this.userService.getUserById(chatUsersId).subscribe({
                  next: data => {
                    chat.name = data.user.firstName + ' ' + data.user.lastName

                    this.chats.push(chat);
                    this.chatsSubject.next(this.chats);

                  },
                  error: err => {
                    console.log(err)
                  }
                });

              }
            });
          }

          else {
            this.chats.push(chat);
            this.chatsSubject.next(this.chats);
          }



        });
      },
      error: err => {
        if (err.status == 404) {
          // Handle other errors or log them to the console
          //console.error(err);
          this.chats = [];
          this.chatsSubject.next(this.chats);
        }
      }
    });
  }

  createChat(channelName: string, chatUserIds: string[]): string{

    this.channelService.createChannel(channelName, chatUserIds).subscribe({
      next: data => {
        console.log(data)
        return data.channel.channelId;
      },
      error: err => {
        return '';
      }
      
    });
    return '';
  }

  clearMessageHistory(){
    this.previousMessages = []
    this.messages = [];
    this.messages$.next(this.messages);
  }
}
