import { MediaForm } from "./MediaForm";

export interface AlbumForm extends MediaForm {
    artist: string;
    runTime: number
    TrackCount: number

}