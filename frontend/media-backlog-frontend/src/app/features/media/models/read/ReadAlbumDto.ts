import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadAlbumDto extends ReadMediaBase {
    artist: string;
    trackCount: number;
    runTime: number;
    type: 'album';
}