import { BaseForm } from "./BaseForm";

export interface MovieForm extends BaseForm {
    mediaType: 'movie';
    director: string;
    runTime: number
    contentRating: string;
    language: string;
}