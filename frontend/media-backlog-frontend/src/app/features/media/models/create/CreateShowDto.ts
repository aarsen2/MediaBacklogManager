import { CreateMediaBase } from "./CreateMediaBase";

export interface CreateShowDto extends CreateMediaBase {
    seasonCount: number;
    episodeCount: number;
    contentRating: string;
}