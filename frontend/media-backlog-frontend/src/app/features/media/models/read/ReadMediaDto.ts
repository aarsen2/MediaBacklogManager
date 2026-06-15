import { ReadAlbumDto } from "./ReadAlbumDto";
import { ReadBookDto } from "./ReadBookDto";
import { ReadGameDto } from "./ReadGameDto";
import { ReadMovieDto } from "./ReadMovieDto";
import { ReadShowDto } from "./ReadShowDto";
import { ReadSongDto } from "./ReadSongDto";

export type ReadMediaDto = | ReadMovieDto | ReadShowDto | ReadAlbumDto | ReadBookDto | ReadGameDto | ReadSongDto;