export interface Repo {
    id: number;
    name: string;
    description: string | null;
    url: string;
    language: string;
    owner: string;
    stargazers: number;
    forks: number;
    watchers: number;
    favorited: boolean;
  }
  