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
    if (repo.favorited) {
      return;
    }
  
    this.repoService.favoriteRepo(repo).subscribe({
      next: () => {
        repo.favorited = true;
      },
      error: () => {
        this.apiErrors = ['Failed to favorite the repository.'];
      }
    });
  }
  
  private searchRepos(resetPage: boolean = true, targetPage: number = 1): void {
    if (resetPage) targetPage = 1;
  
    this.repoService.searchRepos(this.repoName, targetPage, this.perPage, this.sortBy)
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

