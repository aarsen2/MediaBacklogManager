import { BaseForm } from "./BaseForm";

export interface AlbumForm extends BaseForm {
    mediaType: 'album';

    artist: string;
    runTime: number
    trackCount: number

}