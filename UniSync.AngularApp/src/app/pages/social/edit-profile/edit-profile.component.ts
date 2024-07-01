import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './edit-profile.component.html'
})
export class EditProfileComponent implements OnInit {
  profileForm: FormGroup;
  profileImageUrl: string =
    'https://cdn.prod.website-files.com/6365d860c7b7a7191055eb8a/65a751a180c6edec28086e13_Loki%20Bright-p-500.png';
  coverImageUrl: string = 'assets/img/demo/landscape.jpg';

  constructor(private fb: FormBuilder) {
    this.profileForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      phone: [''],
      bio: ['']
    });
  }

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    // Implement logic to fetch user profile data from your backend
    // For now, we'll use dummy data
    const userData = {
      email: 'student1@example.com',
      bio: 'A passionate student at the Faculty of Computer Science in Iasi.'
    };

    this.profileForm.patchValue(userData);
  }

  onProfileImageChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.profileImageUrl = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  onCoverImageChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.coverImageUrl = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit(): void {
    if (this.profileForm.valid) {
      // Implement logic to save the updated profile data
      console.log('Saving profile:', this.profileForm.value);
      // You would typically send this data to your backend API
    }
  }
}
