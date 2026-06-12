import { CreateMovieDto } from "./CreateMovieDto";
import { CreateShowDto } from "./CreateShowDto";


export type CreateMediaDto = | CreateMovieDto | CreateShowDto;