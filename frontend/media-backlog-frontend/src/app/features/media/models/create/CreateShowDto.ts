import { CreateMediaBase } from "./CreateMediaBase";

export interface CreateShowDto extends CreateMediaBase {
    runTime: number;
    language: string;
    director: string;
    contentRating: string;
}