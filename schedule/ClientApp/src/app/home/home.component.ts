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

  editRelation(user: any) {
    console.log(user, {
      user2Id: user.id,
      relation: user.relation
    });



    this.userService.editRelation({
      user2Id: user.id,
      relation: user.relation
    }).subscribe(
      res => {
        this.getUser();
      },
      err => {

        console.log(err);
      }
    );
  }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
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
