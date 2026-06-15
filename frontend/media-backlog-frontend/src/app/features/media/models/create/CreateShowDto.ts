import { CreateMediaBase } from "./CreateMediaBase";

export interface CreateShowDto extends CreateMediaBase {
    seasonCount: number;
    EpisodeCount: number;
    contentRating: string;
}