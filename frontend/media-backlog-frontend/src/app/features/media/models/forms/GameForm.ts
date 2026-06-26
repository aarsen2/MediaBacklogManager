import { BaseForm } from "./BaseForm";

export interface GameForm extends BaseForm {
    mediaType: 'game';
    platforms: string[];
    studio: string;
    contentRating: string;
}