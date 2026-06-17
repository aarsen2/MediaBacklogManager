import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadShowDto extends ReadMediaBase {
    seasonCount: number;
    episodeCount: number;
    contentRating: string;
    type: 'show';
}