import { MediaForm } from "./MediaForm";

export interface ShowForm extends MediaForm {
    seasonCount: number;
    episodeCount: number;
    contentRating: string;
}