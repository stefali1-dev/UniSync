import { Component, OnInit } from '@angular/core';
import { Notification } from '../interfaces/notification.interface';
import { DateTime } from 'luxon';
import { trackById } from '@vex/utils/track-by';
import { VexDateFormatRelativePipe } from '@vex/pipes/vex-date-format-relative/vex-date-format-relative.pipe';
import { RouterLink } from '@angular/router';
import { MatRippleModule } from '@angular/material/core';
import { NgClass, NgFor } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'vex-toolbar-notifications-dropdown',
  templateUrl: './toolbar-notifications-dropdown.component.html',
  styleUrls: ['./toolbar-notifications-dropdown.component.scss'],
  standalone: true,
  imports: [
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    NgFor,
    MatRippleModule,
    RouterLink,
    NgClass,
    VexDateFormatRelativePipe
  ]
})
export class ToolbarNotificationsDropdownComponent implements OnInit {
  notifications: Notification[] = [
    // {
    //   id: '1',
    //   label: 'New Order Received',
    //   icon: 'mat:shopping_basket',
    //   colorClass: 'text-primary-600',
    //   datetime: DateTime.local().minus({ hour: 1 })
    // }
  ];

  trackById = trackById;

  constructor() {}

  ngOnInit() {}
}
