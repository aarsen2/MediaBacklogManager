import { PriorityItemsRowDto } from "./PriorityItemsRowDto";

export interface PriorityItemsReportDto {
  title: string;
  generatedAt: string;
  mediaItems: PriorityItemsRowDto[];
}