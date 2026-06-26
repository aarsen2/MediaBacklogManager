import { UpdateMediaBase } from "./UpdateMediaBase";

export interface UpdateShowDto extends UpdateMediaBase {
    seasonCount: number;
    episodeCount: number;
    contentRating: string;
    type: 'show';
}