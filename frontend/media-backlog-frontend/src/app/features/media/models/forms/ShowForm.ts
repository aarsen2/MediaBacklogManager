import { BaseForm } from "./BaseForm";

export interface ShowForm extends BaseForm {
    mediaType: 'show';
    seasonCount: number;
    episodeCount: number;
    contentRating: string;
}