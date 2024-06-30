import { ChangeDetectorRef, Component } from '@angular/core';
import {
  ReactiveFormsModule,
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { fadeInUp400ms } from '@vex/animations/fade-in-up.animation';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { NgIf } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import {AuthService} from "../../../_services/auth.service";
import { Inject } from '@angular/core';

@Component({
  selector: 'vex-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  animations: [fadeInUp400ms],
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTooltipModule,
    NgIf,
    MatIconModule,
    MatCheckboxModule,
    MatSnackBarModule,
    RouterLink
  ]
})
export class RegisterComponent {
  form: UntypedFormGroup = this.fb.group({
    registrationId: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required],
    passwordConfirm: ['', Validators.required]
  });


  inputType = 'password';
  visible = false;
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';


  constructor(
    private router: Router,
    private fb: UntypedFormBuilder,
    private cd: ChangeDetectorRef,
    @Inject(AuthService) private authService: AuthService,
    private snackbar: MatSnackBar


  ) {}

  send() {
    
    var registrationId  = this.form.get('registrationId')?.value;
    var email = this.form.get('email')?.value;
    var password = this.form.get('password')?.value;

    this.authService.register(registrationId, email, password).subscribe({
      next: data => {
        console.log(data);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
      },
      error: err => {
        this.errorMessage = err.error.message;
        this.isSignUpFailed = true;
      }
    });

    this.snackbar.open(
      "Registration succesful!",
      'Ok',
      {
        duration: 1000
      }
    );
    this.router.navigate(['/login']);

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
