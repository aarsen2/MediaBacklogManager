import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadShowDto extends ReadMediaBase {
    seasonCount: number;
    EpisodeCount: number;
    contentRating: string;
    type: 'show';
}