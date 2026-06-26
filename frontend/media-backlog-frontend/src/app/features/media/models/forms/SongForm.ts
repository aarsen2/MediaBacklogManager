import { BaseForm } from "./BaseForm";

export interface SongForm extends BaseForm {
    mediaType: 'song';
    artist: string;
    runTime: number;
}