import { DashboardItemDto } from "./DashboardItemDto";

export interface DashboardSectionDto {
    title: string;
    items: DashboardItemDto[];
}