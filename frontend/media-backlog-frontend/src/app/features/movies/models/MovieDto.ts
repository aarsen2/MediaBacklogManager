export interface MovieDto {
  title: string;
  description: string;
  assets: any[]; // replace with Asset later if you define it
  releaseDate: string; // ISO string from API
  genres: any[];       // replace with Genre type later
  generalRating: number;
  runTime: number;
  language: string;
  director: string;
  contentRating: string;
}