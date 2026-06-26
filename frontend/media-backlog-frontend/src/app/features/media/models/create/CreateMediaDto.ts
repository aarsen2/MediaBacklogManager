import { CreateAlbumDto } from "./CreateAlbumDto";
import { CreateBookDto } from "./CreateBookDto";
import { CreateGameDto } from "./CreateGameDto";
import { CreateMovieDto } from "./CreateMovieDto";
import { CreateShowDto } from "./CreateShowDto";
import { CreateSongDto } from "./CreateSongDto";


export type CreateMediaDto = | CreateMovieDto | CreateShowDto | CreateAlbumDto | CreateBookDto | CreateGameDto | CreateSongDto;