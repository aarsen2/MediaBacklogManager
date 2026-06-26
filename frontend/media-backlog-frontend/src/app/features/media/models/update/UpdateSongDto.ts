import { UpdateMediaBase } from "./UpdateMediaBase";

export interface UpdateSongDto extends UpdateMediaBase {
    artist: string;
    runTime: number;
    type: 'song';
}