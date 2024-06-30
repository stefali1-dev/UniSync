import { Component, OnInit } from '@angular/core';
import { Link } from '@vex/interfaces/link.interface';
import { scaleIn400ms } from '@vex/animations/scale-in.animation';
import { fadeInRight400ms } from '@vex/animations/fade-in-right.animation';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { NgFor } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { UserService } from '../../_services/user.service';
import { StorageService } from '../../_services/storage.service';
import { Profile } from './interfaces/profile.interface';

export interface FriendSuggestion {
  name: string;
  imageSrc: string;
  friends: number;
  added: boolean;
}

@Component({
  selector: 'vex-social',
  templateUrl: './social.component.html',
  styleUrls: ['./social.component.scss'],
  animations: [scaleIn400ms, fadeInRight400ms],
  standalone: true,
  imports: [MatTabsModule, NgFor, RouterLinkActive, RouterLink, RouterOutlet]
})
export class SocialComponent implements OnInit {
  links: Link[] = [
    {
      label: 'ABOUT',
      route: './',
      routerLinkActiveOptions: { exact: true }
    }
  ];

  public profile: Profile = {
    id: 0,
    imageSrc: '',
    name: '',
    email: '',
    role: ''
  };

  constructor(
    private userService: UserService,
    private storageService: StorageService
  ) {
    let userId = this.storageService.getUser().userId;

    this.userService.getUserById(userId).subscribe({
      next: (data) => {
        console.log(data.user);

        let user = data.user;

        this.profile = {
          id: user.userId,
          // imageSrc: user.userPhoto,
          imageSrc: 'https://i.pravatar.cc/150?img=68',
          name: user.firstName + ' ' + user.lastName,
          email: user.email,
          role: user.roles[0],
          bio: user.bio
        };
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  ngOnInit() {}
}
