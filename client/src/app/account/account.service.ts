import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  /* BehaviorSubject needs an initial value (null) while application starts.
   * It results in the automatic log out and routed back to login page when user click refresh in our checkout page,
   * because the currentUserSource would be set to null when the page refresh, and the auth gaurd get the currentUserSource as null.
   * To resolve the issue, we'll use ReplaySubject instead of BehaviorSubject
   */
  // private currentUserSource = new BehaviorSubject<User | null>(null);

  private currentUserSource = new ReplaySubject<User | null>(1); // need one value to be catched, no value needed to be initialized
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  loadCurrentUser(token: string | null) {

    // If user logged out, reset the currentUserSource to null state.
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    // Add authorization to header in request
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<User>(this.baseUrl + 'account', { headers }).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        }
        return null;
      })
    )
  }

  login(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    )
  }

  register(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    )
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/'); // redirect to home
  }

  // For validation
  checkEmailExists(email: string) {
    return this.http.get<boolean>(this.baseUrl + 'account/emailExists?email=' + email);
  }
}
