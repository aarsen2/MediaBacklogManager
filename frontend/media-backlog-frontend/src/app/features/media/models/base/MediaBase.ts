export interface MediaBase {
    title: string;
    description: string;
    releaseDate: string;
    generalRating: number;
    type: MediaType;
    assets: any[]; // replace with propper asset storage later
    genres: any[];       // List of genre types. I'll figure this out later.
}