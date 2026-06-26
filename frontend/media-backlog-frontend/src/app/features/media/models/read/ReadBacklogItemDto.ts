import { ReadMediaDto } from './ReadMediaDto';


export interface ReadBacklogItemDto {
  id: number;              
  userId: string;          
  mediaId: number;         
  status: string; 
  prioritized: boolean;
  userRating: number;
  notes?: string;

  dateAdded: string;      
  dateCompleted?: string;

  media: ReadMediaDto;     
}