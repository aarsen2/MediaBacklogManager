export interface MovieDto {
  title: string;
  description: string;
  releaseDate: string; // 
  generalRating: number;
  runTime: number;
  language: string;
  director: string;
  contentRating: string;
  assets: any[]; // replace with propper asset storage later
  genres: any[];       // List of genre types. I'll figure this out later.
}