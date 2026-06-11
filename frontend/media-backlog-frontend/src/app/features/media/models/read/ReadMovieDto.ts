import { MovieDto } from "../base/MovieDto";

export interface ReadMovieDto extends MovieDto {
    id: number;
    dateCreated: string;
}