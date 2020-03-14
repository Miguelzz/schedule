import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  objectKeys = Object.keys;

  editUser: any;

  user: any;

  constructor(private router: Router, private userService: UserService) { }

  relationEdit(user: any) {
    this.editUser = user;
    console.log('Save button is clicked!', user);
  }

  UserEditSave(name: string, lastName: string, event: Event) {
    event.preventDefault();
    // this.loginService.login(email, password).subscribe(
    //   (res: any) => {
    //     localStorage.setItem('token', res.token);
    //   },
    //   error => {
    //     if (error.status === 401) {
    //       console.error('401');
    //     }
    //     console.error(error);
    //   },
    //   () => this.navigate()
  }


  ngOnInit() {
    this.userService.getUser().subscribe(
      res => {
        this.user = res;
      },
      err => {

        if (err.status === 401) {
          localStorage.removeItem('token');
          this.router.navigateByUrl('/login');
        }
        console.log(err);
      }
    );
  }



}
