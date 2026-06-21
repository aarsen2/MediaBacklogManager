import { MediaForm } from "./MediaForm";

export interface BookForm extends MediaForm {
    author: string;
    pageCount: number
    language: string;
}