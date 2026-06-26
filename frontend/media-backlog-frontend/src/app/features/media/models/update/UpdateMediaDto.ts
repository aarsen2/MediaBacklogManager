import { UpdateAlbumDto } from "./UpdateAlbumDto";
import { UpdateBookDto } from "./UpdateBookDto";
import { UpdateGameDto } from "./UpdateGameDto";
import { UpdateMovieDto } from "./UpdateMovieDto";
import { UpdateShowDto } from "./UpdateShowDto";
import { UpdateSongDto } from "./UpdateSongDto";

export type UpdateMediaDto = | UpdateMovieDto | UpdateShowDto | UpdateAlbumDto | UpdateBookDto | UpdateGameDto | UpdateSongDto;