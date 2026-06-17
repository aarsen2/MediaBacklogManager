import { Platform } from "../base/Platform";
import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadGameDto extends ReadMediaBase {
    platforms: Platform[];
    studio: string;
    contentRating: string;
    type: 'game'
}