import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  DestroyRef,
  inject,
  OnInit,
  ViewChild
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Chat, ChatMessage } from '../chat.component';
import { trackById } from '@vex/utils/track-by';
import { map } from 'rxjs/operators';
import { fadeInUp400ms } from '@vex/animations/fade-in-up.animation';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { stagger20ms } from '@vex/animations/stagger.animation';
import { VexScrollbarComponent } from '@vex/components/vex-scrollbar/vex-scrollbar.component';
import { ChatService } from '../chat.service';
import { MatMenuModule } from '@angular/material/menu';
import { NgFor, NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { StorageService } from '../../../_services/storage.service';
import { CommonModule } from '@angular/common';
import { MessageService } from '../../../_services/message.service';
import { ChatComponent } from '../chat.component';
import { ChannelService } from '../../../_services/channel.service';
import { Router } from '@angular/router';
import { convo1, convo2, convo3, recentChats } from 'src/static-data/mock';

@Component({
  selector: 'vex-chat-conversation',
  templateUrl: './chat-conversation.component.html',
  styleUrls: ['./chat-conversation.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [fadeInUp400ms, stagger20ms],
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    NgIf,
    MatMenuModule,
    VexScrollbarComponent,
    NgFor,
    ReactiveFormsModule,
    MatDividerModule,
    CommonModule
  ]
})
export class ChatConversationComponent implements OnInit {
  chat?: Chat;
  filteredMessages!: ChatMessage[];

  form = new FormGroup({
    message: new FormControl<string>('', {
      nonNullable: true
    })
  });

  previousMessages: ChatMessage[] = [];

  trackById = trackById;

  @ViewChild(VexScrollbarComponent)
  scrollbar?: VexScrollbarComponent;

  private readonly destroyRef: DestroyRef = inject(DestroyRef);

  showChatInfo: boolean = false;

  constructor(
    private route: ActivatedRoute,
    public chatService: ChatService,
    private cd: ChangeDetectorRef,
    private storageService: StorageService,
    private messageService: MessageService,
    private chatComponent: ChatComponent,
    private channelService: ChannelService,
    private router: Router
  ) {}

  ngOnInit() {
    this.route.paramMap
      .pipe(
        map((paramMap) => paramMap.get('chatId')),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((chatId) => {
        this.chatService.clearMessageHistory();

        if (!chatId) {
          throw new Error('Chat id not found!');
        }

        if (chatId === 'df0eb722-2722-4312-8300-58c36cea1b24') {
          this.chat = recentChats[0];
          this.chatService.messages = convo1;
          this.chatService.messages$.next(convo1);
        } else if (chatId === 'd43f0d77-3164-49fb-9e64-82d1ec3ffb68') {
          this.chat = recentChats[1];
          this.chatService.messages = convo2;
          this.chatService.messages$.next(convo2);
        } else if (chatId === 'fe373755-4c6f-467a-a110-b5be0332ca9a') {
          this.chat = recentChats[2];
          this.chatService.messages = convo3;
          this.chatService.messages$.next(convo3);
        } else {
          this.chatService.getPreviousMessages(chatId);

          this.chatService.chats$.subscribe((chats) => {
            if (chats.length > 0) {
              this.chatService.messages$.subscribe((res) => {
                //console.log(res)
              });

              this.chatService.connectedUsers$.subscribe((res) => {
                //console.log(res);
              });

              this.cd.detectChanges();
              const chat = this.chatService.getChat(chatId);

              if (!chat) {
                throw new Error(`Chat with id ${chatId} not found!`);
              }

              this.chat = chat;
              this.chat.unreadCount = 0;
              //this.filterMessages(chatId);
              this.cd.detectChanges();

              //console.log(this.messages);

              this.scrollToBottom();
            } else {
              console.log('No chats available yet');

              this.router.navigate(['apps/chat']);
            }
          });
        }
      });
  }

  // filterMessages(id: ChatMessage['id']) {
  //   this.chatService.messages$.subscribe((messages) => {
  //   this.filteredMessages = messages.filter((message: ChatMessage) => message.id === id);
  //   // Do something with the filtered messages
  //   console.log(this.filteredMessages);
  // });
  // }

  send() {
    this.scrollToBottom();
    let messageText = this.form.controls.message.value;

    // let newMessage: ChatMessage = {
    //   id: this.chat!.id,
    //   senderId: this.storageService.getUser().userId,
    //   message: messageText,
    //   messageTime: new Date().toISOString()
    // }

    //this.messages.push(newMessage);

    this.chatService
      .sendMessage(messageText)
      .then(() => {
        console.log(`Sent message: ${messageText}`);
      })
      .catch((err) => {
        console.log(err);
      });

    this.form.controls.message.setValue('');

    this.cd.detectChanges();
    this.scrollToBottom();
  }

  isIncomingMessage(message: ChatMessage) {
    return this.storageService.getUser().userId !== message.senderId;
  }

  scrollToBottom() {
    if (!this.scrollbar) {
      console.log('not scrolable');
      return;
    }

    this.scrollbar.scrollbarRef?.getScrollElement()?.scrollTo({
      behavior: 'smooth',
      top: this.scrollbar.scrollbarRef.getContentElement()?.clientHeight
    });
  }

  openDrawer() {
    this.chatService.drawerOpen.next(true);
    this.cd.markForCheck();
  }

  closeDrawer() {
    this.chatService.drawerOpen.next(false);
    this.cd.markForCheck();
  }

  toggleInfo() {
    this.showChatInfo = !this.showChatInfo;
  }
}
