import { MediaForm } from "./MediaForm";

export interface GameForm extends MediaForm {
    platforms: string[];
    studio: string;
    contentRating: string;
}