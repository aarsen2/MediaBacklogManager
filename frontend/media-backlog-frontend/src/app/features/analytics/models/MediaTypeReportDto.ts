import { MediaTypeSummaryRowDto } from "./MediaTypeSummaryRowDto";

export interface MediaTypeReportDto {
  title: string;
  generatedAt: string;
  mediaTypeRows: MediaTypeSummaryRowDto[];
}