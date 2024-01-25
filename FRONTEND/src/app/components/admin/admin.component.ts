import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/interfaces/account/user';
import { EditRolesDialogComponent } from './dialogs/edit-roles-dialog/edit-roles-dialog.component';
import { ConfirmDialogComponent } from './dialogs/confirm-dialog/confirm-dialog.component';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/services/admin/admin.service';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Chart, registerables } from 'chart.js';
import { UsersCount } from 'src/app/interfaces/admin/usersCount';
Chart.register(...registerables);

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  chartUsers: any;
  users?: number[] = [];

  dataSource = new MatTableDataSource<User>([]);
  displayedColumns: string[] = ['name', 'roles', 'actions'];
  backgroundColorHeader = '#222';
  backgroundColorRow = '#333';

  randomColors1 = Array.from({ length: 6 }, () =>
    `rgba(${Math.floor(Math.random() * 256)}, ${Math.floor(
      Math.random() * 256
    )}, ${Math.floor(Math.random() * 256)}, 0.8)`
  );
  randomColors2 = Array.from({ length: 6 }, () =>
    `rgba(${Math.floor(Math.random() * 256)}, ${Math.floor(
      Math.random() * 256
    )}, ${Math.floor(Math.random() * 256)}, 0.8)`
  );
  randomColors3 = Array.from({ length: 6 }, () =>
    `rgba(${Math.floor(Math.random() * 256)}, ${Math.floor(
      Math.random() * 256
    )}, ${Math.floor(Math.random() * 256)}, 0.8)`
  );

  constructor(private adminService: AdminService, public dialog: MatDialog, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getUsersWithRoles();
    this.getUsersCount();
  };

  ngAfterViewInit() {
    this.createChartUsers();
  }

  createChartUsers() {
    this.chartUsers = new Chart('chartUsers', {
      type: 'bar',
      data: {
        labels: ["Last day", "Last 7 days", "Last month", "Last 3 months", "Last 6 months", "Last year"],
        datasets: [
          {
            label: 'Users Registered',
            data: this.users,
            backgroundColor: this.randomColors1,
          },
        ],
      },
      options: {
        aspectRatio: 2.2,
        maintainAspectRatio: false,
        scales: {
          x: {
            beginAtZero: true,
            ticks: {
              color: '#ddd'
            }
          },
          y: {
            beginAtZero: true,
            ticks: {
              color: '#ddd',
            }
          }
        },
        plugins: {
          legend: {
            labels: {
              color: '#ddd'
            }
          },
          title: {
            display: true,
            text: 'FitMetricsHub Users',
            color: 'white'
          }
        }
      }
    });
  }

  getUsersCount(): void {
    this.adminService.getUsersCount().subscribe({
      next: (response: UsersCount) => {
        const responseArray: number[] = Object.values(response);
        this.users = responseArray;
        this.chartUsers.data.datasets[0].data = this.users;
        this.chartUsers.update();
      }
    });
  };

  getUsersWithRoles(): void {
    this.adminService.getUsersWithRoles().subscribe({
      next: users => {
        this.dataSource.data = users
      }
    })
  };

  lockMember(userId: number, username: string): void {
    const userName = {
      username: username
    } as {};

    this.adminService.lockUser(userId, userName).subscribe({
      next: () => {
        this.toastr.success("User banned succesfully!")
        this.getUsersWithRoles();
      },
      error: (error) => {
        this.toastr.error(error.error);
      }
    })
  };

  unlockMember(userId: number, username: string): void {
    const userName = {
      username: username
    } as {};

    this.adminService.unlockUser(userId, userName).subscribe({
      next: () => {
        this.toastr.success("User unbanned succesfully!")
        this.getUsersWithRoles();
      },
      error: (error) => {
        this.toastr.error(error.error)
      }
    })
  };

  openDialogRoles(user: any) {
    const dialogRef = this.dialog.open(EditRolesDialogComponent, {
      data: {
        member: {
          userName: user.userName,
          roles: user.roles
        }
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'success') {
        this.getUsersWithRoles();
      }
    });
  };

  openConfirmDialog(userId: number, username: string) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        member: {
          userName: username,
          userId: userId
        }
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'success') {
        this.getUsersWithRoles();
      }
    });
  };
}
