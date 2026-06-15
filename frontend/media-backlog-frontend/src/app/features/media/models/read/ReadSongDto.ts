import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadSongDto extends ReadMediaBase {
    artist: string;
    runTime: number;
    type: 'song';
}