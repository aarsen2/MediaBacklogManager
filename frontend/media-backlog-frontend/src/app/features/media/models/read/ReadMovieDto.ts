import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadMovieDto extends ReadMediaBase {
    runTime: number;
    language: string;
    director: string;
    contentRating: string;
    type: 'movie';
}