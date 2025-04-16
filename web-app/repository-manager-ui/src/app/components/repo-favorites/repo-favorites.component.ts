import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RepoService } from '../../services/repo.service';
import { Repo } from '../../models/repo.model';

@Component({
  selector: 'app-repo-favorites',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './repo-favorites.component.html',
  styleUrls: ['./repo-favorites.component.css']
})
export class RepoFavoritesComponent {
  repos: Repo[] = [];
  repoName = '';
  page = 1;
  perPage = 5;
  finalPage = 1;
  sortBy: number | null = null;
  errorMessage = '';
  apiErrors: string[] = [];

  constructor(private repoService: RepoService) {}

  search(): void {
    this.errorMessage = '';
    this.searchRepos(true);
  }

  nextPage(): void {
    if (this.page < this.finalPage) {
      this.searchRepos(false, this.page + 1);
    }
  }

  prevPage(): void {
    if (this.page > 1) {
      this.searchRepos(false, this.page - 1);
    }
  }

  toggleFavorite(repo: Repo): void {
    repo.favorited = !repo.favorited;
  }

  private searchRepos(resetPage: boolean = true, targetPage: number = 1): void {
    if (resetPage) targetPage = 1;

    this.repoService.getFavoriteRepos(targetPage, this.perPage, this.sortBy)
      .subscribe({
        next: (res) => {
          this.repos = res.repositories;
          this.finalPage = res.finalPage;
          this.page = targetPage;
          this.apiErrors = [];
        },
        error: (err) => {
          this.apiErrors = [];

          if (err.status === 400 && Array.isArray(err.error)) {
            this.apiErrors = err.error;
            return;
          }

          if (err.status === 404 && typeof err.error === 'string') {
            this.apiErrors.push(err.error);
            return;
          }

          this.apiErrors.push('An unexpected error occurred. Please try again later.');
        }
      });
  }
}
