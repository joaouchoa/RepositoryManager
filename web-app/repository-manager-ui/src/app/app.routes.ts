import { Routes } from '@angular/router';
import { RepoListComponent } from './components/repo-list/repo-list.component';
import { RepoFavoritesComponent } from './components/repo-favorites/repo-favorites.component';

export const routes: Routes = [
  { path: '', component: RepoListComponent },
  { path: 'favorites', component: RepoFavoritesComponent }
];
