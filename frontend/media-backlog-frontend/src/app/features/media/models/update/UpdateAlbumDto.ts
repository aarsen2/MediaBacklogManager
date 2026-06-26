import { UpdateMediaBase } from "./UpdateMediaBase";

export interface UpdateAlbumDto extends UpdateMediaBase {
    artist: string;
    trackCount: number;
    runTime: number;
    type: 'album';
}