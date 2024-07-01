import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

interface GeneralStatistics {
  totalUsers: number;
  totalStudents: number;
  totalTeachers: number;
  activeCourses: number;
  totalDepartments: number;
}

@Component({
  selector: 'app-admin-homepage',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './admin-home.component.html'
})
export class AdminHomeComponent implements OnInit {
  adminName: string = 'Admin User';
  statistics: GeneralStatistics;

  constructor() {
    this.statistics = {
      totalUsers: 0,
      totalStudents: 0,
      totalTeachers: 0,
      activeCourses: 0,
      totalDepartments: 0
    };
  }

  ngOnInit(): void {
    this.loadMockData();
  }

  loadMockData(): void {
    // Mock data pentru statistici generale
    this.statistics = {
      totalUsers: 58,
      totalStudents: 40,
      totalTeachers: 15,
      activeCourses: 10,
      totalDepartments: 5
    };
  }
}
