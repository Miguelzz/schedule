import { Component } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-search-component',
  styleUrls: ['./search.component.css'],
  templateUrl: './search.component.html'
})
export class SearchComponent {

  users: any;
  constructor(private userService: UserService, private router: Router) { }

  addRelation(user: any) {
    console.log(user, {
      user2Id: user.id,
      relation: user.relation
    });
    this.userService.addRelation({
      user2Id: user.id,
      relation: user.relation
    }).subscribe(
      res => {
        this.router.navigateByUrl('/');
      },
      err => {

        console.log(err);
      }
    );
  }

  getUsers(relation) {
    this.userService.getUsers(relation).subscribe(
      res => {
        this.users = res;
      },
      err => {

        console.log(err);
      }
    );
  }

}
