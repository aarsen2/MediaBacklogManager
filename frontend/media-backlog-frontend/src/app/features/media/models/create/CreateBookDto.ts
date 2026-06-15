import { CreateMediaBase } from "./CreateMediaBase";

export interface CreateBookDto extends CreateMediaBase {
    author: string;
    pageCount: number;
    language: string;
 }