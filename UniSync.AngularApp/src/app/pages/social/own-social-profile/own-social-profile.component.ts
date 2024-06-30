import { Component, OnInit } from '@angular/core';
import { fadeInUp400ms } from '@vex/animations/fade-in-up.animation';
import { fadeInRight400ms } from '@vex/animations/fade-in-right.animation';
import { scaleIn400ms } from '@vex/animations/scale-in.animation';
import { stagger40ms } from '@vex/animations/stagger.animation';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { Link } from '@vex/interfaces/link.interface';
import { Profile } from '../interfaces/profile.interface';
import { UserService } from 'src/app/_services/user.service';
import { StorageService } from 'src/app/_services/storage.service';
import { MatTabsModule } from '@angular/material/tabs';
import { RouterLinkActive } from '@angular/router';
import { RouterLink } from '@angular/router';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'vex-social-profile',
  templateUrl: './own-social-profile.component.html',
  animations: [fadeInUp400ms, fadeInRight400ms, scaleIn400ms, stagger40ms],
  standalone: true,
  imports: [
    MatIconModule,
    NgFor,
    NgIf,
    MatButtonModule,
    MatTabsModule,
    RouterLinkActive,
    RouterLink,
    RouterOutlet
  ]
})
export class SocialProfileComponent implements OnInit {
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

  ngOnInit(): void {}

  onProfileImageChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.profile.imageSrc = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }
}
