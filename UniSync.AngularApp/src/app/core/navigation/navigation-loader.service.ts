import { Injectable } from '@angular/core';
import { VexLayoutService } from '@vex/services/vex-layout.service';
import { NavigationItem } from './navigation-item.interface';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavigationLoaderService {
  private readonly _items: BehaviorSubject<NavigationItem[]> =
    new BehaviorSubject<NavigationItem[]>([]);

  get items$(): Observable<NavigationItem[]> {
    return this._items.asObservable();
  }

  constructor(private readonly layoutService: VexLayoutService) {
    this.loadNavigation();
  }

  loadNavigation(): void {
    this._items.next([
      {
        type: 'subheading',
        label: 'Dashboards',
        children: [
          {
            type: 'link',
            label: 'Analytics',
            route: '/',
            icon: 'mat:insights',
            routerLinkActiveOptions: { exact: true }
          }
        ]
      },
      {
        type: 'subheading',
        label: 'Apps',
        children: [
          {
            type: 'link',
            label: 'Chat',
            route: '/apps/chat',
            icon: 'mat:chat',
            badge: {
              value: '16',
              bgClass: 'bg-cyan-600',
              textClass: 'text-white'
            }
          },
          {
            type: 'link',
            label: 'Contacts',
            route: '/apps/contacts',
            icon: 'mat:contacts',
            badge: {
              value: '16',
              bgClass: 'bg-cyan-600',
              textClass: 'text-white'
            }
          }
        ]
      },
    ]);
  }
}
