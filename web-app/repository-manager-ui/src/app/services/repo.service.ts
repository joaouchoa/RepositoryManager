import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Repo } from '../models/repo.model';

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
}
