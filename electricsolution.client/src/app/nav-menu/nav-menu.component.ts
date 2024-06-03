import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/notification.service';
import {notification} from '../interfaces/notification.interface';
import { NotificationUpdateService } from '../services/notification-update.service';

interface DropdownStates {
  [key: string]: boolean;
}

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})


export class NavMenuComponent implements OnInit {
  isAuthenticated = false;
  userName: string | null = null;
  isExpanded = false;
  isDropdownOpen = false;
  dropdownStates: DropdownStates = {
    userDropdown: false,
    contractDropdown: false,
    bellDropdown: false
  };
  showNotificationBadge = true;
  unreadNotifications: notification[] = [];
  clientId: string = '';
  animateBell = false;
  private bellSound: HTMLAudioElement;

  constructor(
    private authService: AuthService,
    private router: Router,
    private notificationService: NotificationService,
    private notificationUpdateService: NotificationUpdateService
  ) {
    this.bellSound = new Audio('/assets/sounds/inbox-message.mp3');
    this.bellSound.volume = 0.2;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

 
  toggleDropdown(dropdownId: string) {
    this.dropdownStates[dropdownId] = !this.dropdownStates[dropdownId];

    if (dropdownId === 'bellDropdown' && this.dropdownStates[dropdownId]) {
      setTimeout(() => {
        this.showNotificationBadge = false;
       
      }, 4000);
    }


    // Cierra otros menús desplegables abiertos, si es necesario
    for (const key in this.dropdownStates) {
      if (key !== dropdownId) {
        this.dropdownStates[key] = false;
      }
    }
  }


  ngOnInit() {
    this.checkLoginStatus();

    this.authService.getClient().subscribe(userName => {
      this.userName = userName;
    });
    this.authService.getClientId().subscribe(clientId => {
      this.clientId = clientId;
      if (this.isAuthenticated) {
        this.loadUnreadNotifications(this.clientId);
      }
    });
    this.notificationUpdateService.contractSaved$.subscribe(() => {
      this.loadUnreadNotifications(this.clientId);
    });
  }


  handleNotificationClick(notification: notification, event: Event) {
    event.preventDefault();
    console.log(`Notification clicked: ${notification.message}`);
    this.notificationService.putMarkAsRead(notification.id).subscribe(() => {
      this.loadUnreadNotifications(this.clientId);
    });

  }


  loadUnreadNotifications(clientId: string) {
    this.notificationService.getUnreadNotifications(clientId).subscribe({
      next: (notifications) => {
        if (notifications.length > 0 && this.unreadNotifications.length !== notifications.length) {
          this.animateBell = true;
          setTimeout(() => this.animateBell = false,500); 
          this.playBellSound();
        }
        this.unreadNotifications = notifications;
        console.log(notifications);
      },
      error: (err) => console.error(err),
    });
  }


  checkLoginStatus() {
    this.authService.isAuthenticated().subscribe(isAuth => {
      this.isAuthenticated = isAuth ? true : false;
    });

  }

  logout(): void {
    this.authService.logout().subscribe({
      next: () => {
        console.log('Logout successful!');
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.error('Error logging out', error);
      }
    });
  }


  viewMyAccount(): void {
    this.router.navigate(['/my-account']);
  }


  closeDropdowns() {
    for (const key in this.dropdownStates) {
      this.dropdownStates[key] = false;
    }
  }


  playBellSound(): void {
    this.bellSound.play().catch(err => console.error('Error al reproducir el sonido:', err));
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    // Obtén el elemento clicado
    const targetElement = event.target as HTMLElement;

    // Verifica si el clic fue dentro de un menú desplegable usando una clase o id
    if (!targetElement.closest('.dropdown-toggle') && !targetElement.closest('.dropdown-menu')) {
      this.closeDropdowns();
    }
  }



}
