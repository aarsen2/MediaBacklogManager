import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadShowDto extends ReadMediaBase {
    runTime: number;
    language: string;
    director: string;
    contentRating: string;
    type: 'Show';
}