import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RepoService } from '../../services/repo.service';
import { Repo } from '../../models/repo.model';

@Component({
  selector: 'app-repo-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './repo-list.component.html',
  styleUrls: ['./repo-list.component.css']
})
export class RepoListComponent {
  repos: Repo[] = [];
  repoName = '';
  page = 1;
  perPage = 5;
  finalPage = 1;
  sortBy: number | null = null;
  errorMessage = '';
  apiErrors: string[] = [];
  isLoading = false;

  constructor(private repoService: RepoService) {}

  search(): void {
    if (!this.repoName.trim()) {
      this.errorMessage = 'Please enter a repository name.';
      this.apiErrors = [];
      return;
    }

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
    if (this.isLoading) return;
    this.isLoading = true;

    const onError = (err: any) => {
      this.apiErrors = [];

      try {
        const parsed = JSON.parse(err.error);
        const parsedErrors = Array.isArray(parsed) ? parsed : [parsed.toString()];
        this.apiErrors = [...parsedErrors, 'Please refresh the page to update the list.'];        
      } catch {
        if (typeof err.error === 'string') {
          this.apiErrors.push(err.error);
        } else {
          this.apiErrors.push('Failed to remove repository from favorites.');
        }
        this.apiErrors.push('Please refresh the page to update the list.');
      }

      this.isLoading = false;
    };

    if (repo.favorited) {
      this.repoService.removeFavoriteRepo(repo.id).subscribe({
        next: () => {
          repo.favorited = false;
          this.apiErrors = [];
          this.isLoading = false;
        },
        error: onError
      });
    } else {
      this.repoService.favoriteRepo(repo).subscribe({
        next: () => {
          repo.favorited = true;
          this.apiErrors = [];
          this.isLoading = false;
        },
        error: onError
      });
    }
  }

  private searchRepos(resetPage: boolean = true, targetPage: number = 1): void {
    if (resetPage) targetPage = 1;

    this.isLoading = true;

    this.repoService.searchRepos(this.repoName, targetPage, this.perPage, this.sortBy)
      .subscribe({
        next: (res) => {
          this.repos = res.repositories;
          this.finalPage = res.finalPage;
          this.page = targetPage;
          this.apiErrors = [];
          this.isLoading = false;
        },
        error: (err) => {
          this.apiErrors = [];

          try {
            const parsed = JSON.parse(err.error);
            this.apiErrors = Array.isArray(parsed) ? parsed : [parsed.toString()];
          } catch {
            if (typeof err.error === 'string') {
              this.apiErrors.push(err.error);
            } else {
              this.apiErrors.push('An unexpected error occurred. Please try again later.');
            }
          }

          this.isLoading = false;
        }
      });
  }
}
