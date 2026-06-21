import { MediaForm } from "./MediaForm";

export interface SongForm extends MediaForm {
    artist: string;
    runTime: number;
}