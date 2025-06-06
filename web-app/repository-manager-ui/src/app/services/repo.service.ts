import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Repo } from '../models/repo.model';
import { map } from 'rxjs/operators';

interface SearchResponse {
  finalPage: number;
  repositories: Repo[];
}

@Injectable({ providedIn: 'root' })
export class RepoService {
  private readonly baseUrl = 'https://localhost:7162/api/Repos';

  constructor(private http: HttpClient) {}

  searchRepos(repoName: string, page = 1, perPage = 10, sortBy: number | null = null): Observable<SearchResponse> {
    let params = new HttpParams()
      .set('RepoName', repoName)
      .set('Page', page.toString())
      .set('PerPage', perPage.toString());

      if (sortBy !== null && sortBy !== undefined) {
        params = params.set('SortBy', sortBy);
      }

    return this.http.get<SearchResponse>(`${this.baseUrl}/search`, { params });
  }
  getFavoriteRepos(page: number, perPage: number, sortBy: number | null) {
    const params: any = { page, perPage };
    if (sortBy !== null) {
      params.sortBy = sortBy;
    }
  
    return this.http.get<SearchResponse>(`${this.baseUrl}/favorite-repositories`, {
      params
    });
  }
  favoriteRepo(repo: Repo): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/favorite`, repo);
  }
  removeFavoriteRepo(id: number): Observable<void> {
    return this.http.request('DELETE', `${this.baseUrl}/remove-favorite`, {
      body: { id },
      responseType: 'text' as const
    }).pipe(map(() => undefined));  
  }
}
