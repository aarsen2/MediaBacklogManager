import { TimeToCompleteRowDto } from "./TimeToCompleteRowDto";

export interface TimeToCompleteReportDto {
  title: string;
  generatedAt: string; 
  timeToCompleteRows: TimeToCompleteRowDto[];
}