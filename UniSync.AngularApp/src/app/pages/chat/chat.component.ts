import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  DestroyRef,
  inject,
  OnInit
} from '@angular/core';
import { fadeInUp400ms } from '@vex/animations/fade-in-up.animation';
import { Observable, of } from 'rxjs';
import { delay, filter, startWith, switchMap } from 'rxjs/operators';
import { trackById } from '@vex/utils/track-by';
import { stagger80ms } from '@vex/animations/stagger.animation';
import {
  NavigationEnd,
  Router,
  RouterLink,
  RouterLinkActive,
  RouterOutlet
} from '@angular/router';
import { ChatService } from './chat.service';
import { MatRippleModule } from '@angular/material/core';
import { VexScrollbarComponent } from '@vex/components/vex-scrollbar/vex-scrollbar.component';
import { AsyncPipe, NgClass, NgFor, NgIf } from '@angular/common';
import { CommonModule } from '@angular/common';

import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatInputModule } from '@angular/material/input';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { VexLayoutService } from '@vex/services/vex-layout.service';
import { StudentService } from '../../_services/student.service';
import { StorageService } from '../../_services/storage.service';
import { UserService } from '../../_services/user.service';
import { ChannelService } from '../../_services/channel.service';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import {Contact, DialogUserList} from './dialog-user-list/dialog-user-list'
import { DialogGroupList } from './dialog-group-list/dialog-group-list';

export interface Chat {
  id: string;
  imageUrl: string;
  name: string;
  lastMessage: string;
  unreadCount: number;
  timestamp: string;
  nrOfParticipants: number
}

export interface ChatMessage {
  id: string;
  senderId: string;
  message: string;
  messageTime: string;
}

@Component({
  selector: 'vex-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [fadeInUp400ms, stagger80ms],
  standalone: true,
  imports: [
    MatSidenavModule,
    MatMenuModule,
    MatIconModule,
    NgClass,
    NgIf,
    VexScrollbarComponent,
    NgFor,
    MatRippleModule,
    RouterLinkActive,
    RouterLink,
    RouterOutlet,
    AsyncPipe,
    MatButtonModule,
    MatDividerModule,
    MatInputModule
  ]
})
export class ChatComponent implements OnInit {

  chats$: Observable<Chat[]> = of(this.chatService.chats).pipe(
    // Fix to allow stagger animations with static data
    delay(0)
  );

  mobileQuery$ = this.layoutService.ltMd$;
  drawerOpen$ = this.chatService.drawerOpen$;

  trackById = trackById;
  private readonly destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private cd: ChangeDetectorRef,
    private router: Router,
    private layoutService: VexLayoutService,
    public chatService: ChatService,
    private studentService: StudentService,
    private storageService: StorageService,
    private userService: UserService,
    private channelService: ChannelService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.router.events
      .pipe(
        filter((event) => event instanceof NavigationEnd),
        startWith(null),
        switchMap(() => this.mobileQuery$),
        filter((isMobile) => isMobile),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(() => this.closeDrawer());

    this.router.events
      .pipe(
        filter((event) => event instanceof NavigationEnd),
        startWith(null),
        switchMap(() => this.mobileQuery$),
        filter((isMobile) => !isMobile),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(() => this.openDrawer());

      // this.studentService.getStudentsByGroup('1A1').subscribe({
      //   next: data => {
      //     console.log(data.students);
      //   },
      //   error: err => {
      //     console.log(err);
      //   }
      // });
  
  }

  drawerChange(drawerOpen: boolean) {
    this.chatService.drawerOpen.next(drawerOpen);
  }

  openDrawer() {
    this.chatService.drawerOpen.next(true);
    this.cd.markForCheck();
  }

  closeDrawer() {
    this.chatService.drawerOpen.next(false);
    this.cd.markForCheck();
  }

  onEnterPressed(request: string) {
    this.userService.getSearchedUsers(request).subscribe({
      next: data => {
        console.log(data.users);
      },
      error: err => {
        console.log(err);
      }
    });
  }

  openDmDialog() {
    const dialogRef = this.dialog.open(DialogUserList);

    dialogRef.afterClosed().subscribe(result => {
      //console.log(`Dialog result: ${result}`);
    });
  }

  openGroupDialog() {
    const dialogRef = this.dialog.open(DialogGroupList);

    dialogRef.afterClosed().subscribe(result => {
      //console.log(`Dialog result: ${result}`);
    });
  }

}
