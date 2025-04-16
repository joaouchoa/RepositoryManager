import { Component } from '@angular/core';
import { RepoListComponent } from './components/repo-list/repo-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RepoListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'repository-manager-ui';
}
