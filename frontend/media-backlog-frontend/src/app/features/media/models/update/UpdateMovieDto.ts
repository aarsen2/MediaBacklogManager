import { UpdateMediaBase } from "./UpdateMediaBase";

export interface UpdateMovieDto extends UpdateMediaBase {
    runTime: number;
    language: string;
    director: string;
    contentRating: string;
    type: 'movie';
}