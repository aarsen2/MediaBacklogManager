import { UpdateMediaBase } from "./UpdateMediaBase";

export interface UpdateBookDto extends UpdateMediaBase {
    author: string;
    pageCount: number;
    language: string;
    type: 'book'
}