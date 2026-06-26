import { CreateMediaDto } from "../create/CreateMediaDto";
import { UpdateMediaDto } from "./UpdateMediaDto";

export interface UpdateBacklogItemDto {
    mediaId: string;
    status: string;
    prioritized: boolean;
    userRating: number,
    notes: string;
    media: UpdateMediaDto;
}