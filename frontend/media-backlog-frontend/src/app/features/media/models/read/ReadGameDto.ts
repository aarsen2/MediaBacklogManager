import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadGameDto extends ReadMediaBase {
    platforms: string[];
    studio: string;
    contentRating: string;
    type: 'game'
}