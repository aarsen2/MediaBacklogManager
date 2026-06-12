import { ReadMovieDto } from "./ReadMovieDto";
import { ReadShowDto } from "./ReadShowDto";

export type ReadMediaDto = | ReadMovieDto | ReadShowDto;