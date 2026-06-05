import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TestApi {
  private baseUrl = 'https://localhost:7170/api/test'
}
