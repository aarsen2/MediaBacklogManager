import { MediaForm } from "./MediaForm";

export interface MovieForm extends MediaForm {
    director: string;
    runTime: number
    contentRating: string;
    language: string;
}