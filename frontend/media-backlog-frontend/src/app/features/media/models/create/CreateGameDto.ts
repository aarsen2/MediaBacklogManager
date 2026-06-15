import { CreateMediaBase } from "./CreateMediaBase";

export interface CreateGameDto extends CreateMediaBase {
    platforms: string[];
    studio: string;
    contentRating: string;
}