import { Component, OnInit } from '@angular/core';
import { FriendSuggestion } from '../social.component';
import { fadeInUp400ms } from '@vex/animations/fade-in-up.animation';
import { fadeInRight400ms } from '@vex/animations/fade-in-right.animation';
import { scaleIn400ms } from '@vex/animations/scale-in.animation';
import { stagger40ms } from '@vex/animations/stagger.animation';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { SocialComponent } from '../social.component';

@Component({
  selector: 'vex-social-profile',
  templateUrl: './own-social-profile.component.html',
  animations: [fadeInUp400ms, fadeInRight400ms, scaleIn400ms, stagger40ms],
  standalone: true,
  imports: [MatIconModule, NgFor, NgIf, MatButtonModule]
})
export class SocialProfileComponent implements OnInit {
  constructor(public socialComponent: SocialComponent) {}

  ngOnInit(): void {}

  trackByName(index: number, friend: FriendSuggestion) {
    return friend.name;
  }
}
