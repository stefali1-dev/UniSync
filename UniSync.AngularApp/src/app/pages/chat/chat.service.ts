import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Chat, ChatMessage } from './chat.component';
import { randFullName } from '@ngneat/falso';
import { StudentService } from '../../_services/student.service';
import { StorageService } from '../../_services/storage.service';
import { MessageService } from '../../_services/message.service';
import { UserService } from '../../_services/user.service';
import { ChannelService } from '../../_services/channel.service';
import { ChannelCreationDto } from 'src/app/_interfaces/channelCreationDto';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  public connection: signalR.HubConnection = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:7165/chat')
    .configureLogging(signalR.LogLevel.Information)
    .build();

  public messages$ = new BehaviorSubject<any>([]);
  public connectedUsers$ = new BehaviorSubject<string[]>([]);
  public messages: ChatMessage[] = [];
  public previousMessages: ChatMessage[] = [];
  public users: string[] = [];
  public rooms: string[] = [];
  private isConnected: boolean = false;

  chats: Chat[] = [];

  public chatsSubject = new BehaviorSubject<Chat[]>(this.chats);
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
    this.connection.on(
      'ReceiveMessage',
      (
        userId: string,
        channelId: string,
        message: string,
        messageTime: string,
        senderPhotoUrl: string
      ) => {
        // TODO: remove hardcode

        console.log(`Received message: ${message}`);

        let receivedMmessage: ChatMessage = {
          id: channelId,
          senderId: userId,
          message: message,
          messageTime: messageTime,
          senderPhotoUrl: senderPhotoUrl
        };

        this.messages = [...this.messages, receivedMmessage];
        this.messages$.next(this.messages);
      }
    );

    this.connection.on('ConnectedUser', (users: any) => {
      this.connectedUsers$.next(users);
    });

    //this.rooms.push('1A1');

    // TODO: implement getPreviousMessages() in convo component
    //this.getPreviousMessages();
    //this.getChats();

    this.chats = [
      {
        id: '1',
        imageUrl: 'https://gcdnb.pbrd.co/images/YbWP8CcDbavg.jpg?o=1',
        name: '1A1 Group',
        lastMessage: 'Thanks! Got any homework?',
        unreadCount: 0,
        timestamp: '3 minutes ago',
        nrOfParticipants: 5
      },
      {
        id: '2',
        imageUrl: 'https://gcdnb.pbrd.co/images/Mm99t6Di9bZO.jpg?o=1',
        name: 'Semian A',
        lastMessage: 'Next day course..',
        unreadCount: 2,
        timestamp: '5 gours ago',
        nrOfParticipants: 3
      },
      {
        id: '3',
        imageUrl: 'https://gcdnb.pbrd.co/images/SIjzUOoFLI6J.jpg?o=1',
        name: 'Year 1',
        lastMessage: 'The optionals are intresting',
        unreadCount: 1,
        timestamp: '6 hours ago',
        nrOfParticipants: 10
      },
      {
        id: '4',
        imageUrl:
          'https://images.squarespace-cdn.com/content/v1/58cfd41c17bffcb09bd654f0/1618331670353-X33MH2UJL8BOGXAZN541/unsplash-image-8IKf54pc3qk.jpg?format=750w',
        name: 'HangoutGroup',
        lastMessage: 'Lets go next week..',
        unreadCount: 0,
        timestamp: '2 days ago',
        nrOfParticipants: 8
      }
    ];

    this.chatsSubject.next(this.chats);

    this.messages = [
      {
        id: '1',
        senderId: 'user1',
        message: 'Hi there!',
        messageTime: '2024-06-23T11:50:00',
        senderPhotoUrl: 'https://i.pravatar.cc/150?img=1'
      },
      {
        id: '2',
        senderId: 'user2',
        message: 'Hello! How are you?',
        messageTime: '2024-06-23T11:51:00',
        senderPhotoUrl: 'https://i.pravatar.cc/150?img=60'
      },
      {
        id: '3',
        senderId: 'user1',
        message: 'Doing well, thanks! How about you?',
        messageTime: '2024-06-23T11:52:00',
        senderPhotoUrl: 'https://i.pravatar.cc/150?img=1'
      },
      {
        id: '4',
        senderId: 'user2',
        message: "I'm good too. Any plans for the weekend?",
        messageTime: '2024-06-23T11:53:00',
        senderPhotoUrl: 'https://i.pravatar.cc/150?img=60'
      },
      {
        id: '5',
        senderId: 'user1',
        message: 'Not sure yet. Maybe catching up on some reading.',
        messageTime: '2024-06-23T11:54:00',
        senderPhotoUrl: 'https://i.pravatar.cc/150?img=1'
      },
      {
        id: '6',
        senderId: 'user2',
        message: 'Sounds relaxing! I have a family gathering.',
        messageTime: '2024-06-23T11:55:00',
        senderPhotoUrl: 'https://i.pravatar.cc/150?img=60'
      },
      {
        id: '7',
        senderId: 'user1',
        message: 'Family time is important. Enjoy!',
        messageTime: '2024-06-23T11:56:00',
        senderPhotoUrl: 'https://i.pravatar.cc/150?img=1'
      },
      {
        id: '8',
        senderId: 'user2',
        message: 'Thanks! Got any homework?',
        messageTime: '2024-06-23T11:57:00',
        senderPhotoUrl: 'https://i.pravatar.cc/150?img=60'
      }
    ];

    this.messages$.next(this.messages);
  }

  //start connection
  public async start() {
    try {
      await this.connection.start();
      console.log('Connection is established!');
      let userId = this.storageService.getUser().userId;

      if (userId === null) {
        console.log('NULL USER ID');
      }

      this.channelService.getChannelsByUserId(userId).subscribe({
        next: (data) => {
          data.channels.forEach((channel) => {
            this.joinRoom(userId, channel.channelId);
          });
        },
        error: (err) => {
          console.log(err);
        }
      });
    } catch (error) {
      console.log(error);
    }
  }

  //Join Room
  public async joinRoom(userId: string, room: string) {
    return this.connection.invoke('JoinRoom', { userId, room });
  }

  // Send Messages
  public async sendMessage(message: string) {
    return this.connection.invoke('SendMessage', message);
  }

  //leave
  public async leaveChat() {
    return this.connection.stop();
  }

  getChat(chatId: string): Chat | undefined {
    return this.chats.find((chat) => chat.id === chatId);
  }

  public async getPreviousMessages(channelId: string) {
    this.clearMessageHistory();

    this.messageService.getMessagesByChannel(channelId).subscribe({
      next: (data) => {
        console.log(data.messages);

        for (const message of data.messages) {
          let chatMessage: ChatMessage = {
            id: message.messageId,
            senderId: message.chatUserId,
            message: message.content,
            messageTime: message.timestamp,
            senderPhotoUrl: message.senderPhotoUrl
          };

          this.previousMessages.push(chatMessage);
        }

        this.messages = this.previousMessages;
        this.messages$.next(this.messages);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getChats() {
    let userId = this.storageService.getUser().userId;

    this.channelService.getChannelsByUserId(userId).subscribe({
      next: (data) => {
        console.log(data.channels);

        data.channels.forEach((channel) => {
          let chat: Chat = {
            id: channel.channelId,
            imageUrl:
              'https://images.pexels.com/photos/3225517/pexels-photo-3225517.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500',
            name: channel.channelName,
            lastMessage: '',
            unreadCount: 0,
            timestamp: '',
            nrOfParticipants: channel.chatUsersIds.length
          };

          // rename the chat
          if (chat.nrOfParticipants === 2 && chat.name === 'DM') {
            channel.chatUsersIds.forEach((chatUsersId) => {
              let currentUserId = this.storageService.getUser().userId;

              if (chatUsersId != currentUserId) {
                this.userService.getUserById(chatUsersId).subscribe({
                  next: (data) => {
                    chat.name = data.user.firstName + ' ' + data.user.lastName;

                    this.chats.push(chat);
                    this.chatsSubject.next(this.chats);
                  },
                  error: (err) => {
                    console.log(err);
                  }
                });
              }
            });
          } else {
            this.chats.push(chat);
            this.chatsSubject.next(this.chats);
          }
        });
      },
      error: (err) => {
        if (err.status == 404) {
          // Handle other errors or log them to the console
          //console.error(err);
          this.chats = [];
          this.chatsSubject.next(this.chats);
        }
      }
    });
  }

  createChat(channelName: string, chatUserIds: string[]): string {
    let channelCreationDto: ChannelCreationDto = {
      channelName: channelName,
      chatUsersIds: chatUserIds
    };

    this.channelService.addChannel(channelCreationDto).subscribe({
      next: (data) => {
        console.log(data);
        return data.channel.channelId;
      },
      error: (err) => {
        return '';
      }
    });
    return '';
  }

  clearMessageHistory() {
    this.previousMessages = [];
    this.messages = [];
    this.messages$.next(this.messages);
  }

  clearAllInfo() {
    this.clearMessageHistory();

    this.chats = [];
    this.chatsSubject.next(this.chats);

    this.messages = [];
    this.messages$.next(this.messages);
  }
}
