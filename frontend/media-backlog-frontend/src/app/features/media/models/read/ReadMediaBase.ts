import { Genre } from "../base/Genres";
import { MediaBase } from "../base/MediaBase";

export interface ReadMediaBase extends MediaBase {
    id: number;
    dateCreated: string;
    genres: Genre[];
}