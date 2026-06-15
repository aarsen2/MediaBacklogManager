import { ReadMediaBase } from "./ReadMediaBase";

export interface ReadBookDto extends ReadMediaBase {
    author: string;
    pageCount: number;
    language: string;
    type: 'book'
}