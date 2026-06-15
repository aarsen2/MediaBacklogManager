import { CreateMediaBase } from "./CreateMediaBase";

export interface CreateAlbumDto extends CreateMediaBase {
    artist: string;
    trackCount: number;
    runTime: number;
}