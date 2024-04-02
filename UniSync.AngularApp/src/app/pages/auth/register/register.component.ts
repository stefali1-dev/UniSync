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
import { MatIconModule } from '@angular/material/icon';
import { NgIf } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import {AccountService} from "../../../_services/account.service";

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

  model: any = {};


  inputType = 'password';
  visible = false;
  registered = false;


  constructor(
    private router: Router,
    private fb: UntypedFormBuilder,
    private cd: ChangeDetectorRef,
    private accountService: AccountService

  ) {}

  send() {
    if (this.form.valid && this.form.get('password')?.value == this.form.get('passwordConfirm')?.value) {
      this.model.registrationId = this.form.get('registrationId')?.value;
      this.model.email = this.form.get('email')?.value;
      this.model.password = this.form.get('password')?.value;
    }

    console.log(this.model)

    this.accountService.register(this.model).subscribe(response => {
      this.registered = true;
      // login now
      this.accountService.login(this.model).subscribe(response => {
        console.log("Registered and logged in");
      }, error => {
        console.log(error);
      });
    }
    , error => {
      console.log(error);
    });

    this.router.navigate(['/']);
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
