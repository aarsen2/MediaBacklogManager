import { MovieDto } from "./MovieDto";

export interface ReadMovieDto extends MovieDto {
    id: number;
    dateCreated: string;
}