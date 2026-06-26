import { AlbumForm } from "./AlbumForm";
import { BookForm } from "./BookForm";
import { GameForm } from "./GameForm";
import { MovieForm } from "./MovieForm";
import { ShowForm } from "./ShowForm";
import { SongForm } from "./SongForm";

export type MediaForm =
  | MovieForm
  | ShowForm
  | AlbumForm
  | BookForm
  | GameForm
  | SongForm;