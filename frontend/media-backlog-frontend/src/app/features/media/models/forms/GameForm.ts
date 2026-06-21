import { MediaForm } from "./MediaForm";

export interface GameForm extends MediaForm {
    platform: string[];
    studio: string;
    contentRating: string;
}