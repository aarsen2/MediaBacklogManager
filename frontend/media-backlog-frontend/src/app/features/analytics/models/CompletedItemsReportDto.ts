import { CompletedItemsRowDto } from "./CompletedItemsRowDto";

export interface CompletedItemsReportDto {
    title: string;
    generatedAt: string;
    mediaItems: CompletedItemsRowDto[];
}