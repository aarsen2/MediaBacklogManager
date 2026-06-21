export interface CreateUserMediaDto {
    mediaId: number;
    status: string;
    prioritized: boolean;
    userRating: number,
    notes: string;
}