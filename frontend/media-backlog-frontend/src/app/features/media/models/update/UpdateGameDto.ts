import { Platform } from "../base/Platform";
import { UpdateMediaBase } from "./UpdateMediaBase";

export interface UpdateGameDto extends UpdateMediaBase {
    platforms: string[];
    studio: string;
    contentRating: string;
    type: 'game'
}