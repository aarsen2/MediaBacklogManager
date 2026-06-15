import { CreateMediaBase } from "./CreateMediaBase";

export interface CreateSongDto extends CreateMediaBase {
    artist: string;
    runTime: number;
}