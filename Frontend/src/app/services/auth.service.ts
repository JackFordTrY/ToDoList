import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, concatMap, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  user: User | null = null;

  loadCurrent(): Observable<User> {
    const url: string = environment.apiUrl + '/api/users/current';

    const request = this.http.get<User>(url, { withCredentials: true });

    return request.pipe(
      concatMap((value) =>
        of(value).pipe(
          tap((value) => {
            this.user = value;
          })
        )
      )
    );
  }

  login(username: string, password: string): Observable<void> {
    const url: string = environment.apiUrl + '/api/users/login';

    return this.http.post<void>(
      url,
      {
        username: username,
        password: password,
      },
      { withCredentials: true }
    );
  }
  register(username: string, password: string): Observable<void> {
    const url: string = environment.apiUrl + '/api/users/register';

    return this.http.post<void>(url, {
      username: username,
      password: password,
    });
  }
  logout(): Observable<void> {
    const url: string = environment.apiUrl + '/api/users/logout';

    return this.http.get<void>(url, { withCredentials: true });
  }
}
