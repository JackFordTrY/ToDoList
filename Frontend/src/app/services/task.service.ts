import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Task } from '../models/task.model';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class TaskService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  get(): Observable<Task[]> {
    const url: string =
      environment.apiUrl + `/api/users/${this.authService.user!.user_id}/tasks`;

    return this.http.get<Task[]>(url, { withCredentials: true });
  }

  add(title: string): Observable<number> {
    const url: string =
      environment.apiUrl + `/api/users/${this.authService.user!.user_id}/tasks`;

    return this.http.post<number>(
      url,
      {
        title: title,
      },
      { withCredentials: true }
    );
  }

  complete(id: number, isComplete: boolean | null = null): Observable<void> {
    const url: string =
      environment.apiUrl +
      `/api/users/${this.authService.user!.user_id}/tasks/` +
      id;

    return this.http.put<void>(
      url,
      {
        isComplete: isComplete,
      },
      { withCredentials: true }
    );
  }

  delete(id: number): Observable<void> {
    const url: string =
      environment.apiUrl +
      `/api/users/${this.authService.user!.user_id}/tasks/${id}`;

    return this.http.delete<void>(url, { withCredentials: true });
  }
}
