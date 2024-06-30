import { Injectable } from '@angular/core';
import { VexLayoutService } from '@vex/services/vex-layout.service';
import { NavigationItem } from './navigation-item.interface';
import { BehaviorSubject, Observable } from 'rxjs';
import { StorageService } from '../../_services/storage.service';

@Injectable({
  providedIn: 'root'
})
export class NavigationLoaderService {
  private readonly _items: BehaviorSubject<NavigationItem[]> =
    new BehaviorSubject<NavigationItem[]>([]);

  get items$(): Observable<NavigationItem[]> {
    return this._items.asObservable();
  }

  constructor(
    private readonly layoutService: VexLayoutService,
    private storageService: StorageService
  ) {
    this.loadNavigation();
  }

  loadNavigation(): void {
    this._items.next([
      {
        type: 'link',
        label: 'Home',
        route: '/home',
        routerLinkActiveOptions: { exact: true }
      },
      {
        type: 'link',
        label: 'Chat',
        route: '/apps/chat',
        icon: 'mat:chat'
      },
      {
        type: 'link',
        label: 'Courses',
        route: '/apps/courses'
      },
      {
        type: 'link',
        label: 'Evaluation',
        route: '/apps/evaluation'
        // TODO: add dynamic professor/student evaluation trough storage service
      },
      {
        type: 'link',
        label: 'Timetable',
        route: '/apps/timetable'
      },
      {
        type: 'subheading',
        label: 'Social',
        children: [
          {
            type: 'link',
            label: 'All Users',
            route: '/apps/contacts',
            icon: 'mat:contacts',
            badge: {
              value: '16',
              bgClass: 'bg-cyan-600',
              textClass: 'text-white'
            }
          },
          {
            type: 'link',
            label: 'My Profile',
            route: '/apps/profile',
            icon: 'mat:person_outline',
            badge: {
              value: '16',
              bgClass: 'bg-cyan-600',
              textClass: 'text-white'
            }
          }
        ]
      }
    ]);
  }
}
