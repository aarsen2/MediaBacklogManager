import { BaseForm } from "./BaseForm";

export interface BookForm extends BaseForm {
    mediaType: 'book';
    author: string;
    pageCount: number
    language: string;
}