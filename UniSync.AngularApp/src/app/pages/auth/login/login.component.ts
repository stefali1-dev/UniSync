import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component
} from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { fadeInUp400ms } from '@vex/animations/fade-in-up.animation';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { NgIf } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AuthService } from '../../../_services/auth.service';
import { StorageService } from '../../../_services/storage.service';
import { Inject } from '@angular/core';
import { Member } from '../../../_modules/member';

@Component({
  selector: 'vex-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [fadeInUp400ms],
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    NgIf,
    MatButtonModule,
    MatTooltipModule,
    MatIconModule,
    MatCheckboxModule,
    RouterLink,
    MatSnackBarModule
  ]
})
export class LoginComponent {
  form = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  inputType = 'password';
  visible = false;
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];
  token = '';

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private snackbar: MatSnackBar,
    @Inject(AuthService) private authService: AuthService,
    private storageService: StorageService
  ) {}

  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      this.roles = this.storageService.getUser().roles;
    }
  }

  send() {
    var email = this.form.get('email')?.value ?? '';
    var password = this.form.get('password')?.value ?? '';

    this.authService.login(email, password).subscribe({
      next: (data) => {
        console.log(data);

        let newMember: Member = {
          token: data,
          userId: null,
          email: null,
          username: null,
          role: null,
          appUserId: null,
          userPhoto: null
        };

        this.storageService.saveUser(newMember);

        this.isLoginFailed = false;
        this.isLoggedIn = true;

        let role = this.storageService.getUser().role;

        this.router.navigate(['/home/student']);
        // if (role === 'Student') {
        //   this.router.navigate(['/home/student']);
        // } else if (role === 'Professor') {
        //   this.router.navigate(['/home/professor']);
        // } else if (role === 'Admin') {
        //   this.router.navigate(['/home/admin']);
        // }
      },
      error: (err) => {
        console.log(err);
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    });

    // this.snackbar.open(
    //   "Lucky you! Looks like you didn't need a password or email address! For a real application we provide validators to prevent this. ;)",
    //   'THANKS',
    //   {
    //     duration: 10000
    //   }
    // );
  }

  reloadPage(): void {
    window.location.reload();
  }

  toggleVisibility() {
    if (this.visible) {
      this.inputType = 'password';
      this.visible = false;
      this.cd.markForCheck();
    } else {
      this.inputType = 'text';
      this.visible = true;
      this.cd.markForCheck();
    }
  }
}
