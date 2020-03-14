import { Component } from '@angular/core';
import { UserService } from '../services/user.service';


@Component({
  selector: 'app-search-component',
  styleUrls: ['./search.component.css'],
  templateUrl: './search.component.html'
})
export class SearchComponent {

  users: any;

  constructor(private userService: UserService) { }


  getUsers(search) {
    this.userService.getUsers(search).subscribe(
      res => {
        this.users = res;
      },
      err => {

        console.log(err);
      }
    );
  }

}
