export interface UserDto {
  id: string;
  username: string;
  displayName: string;
  email?: string;
  roles?: string[];
}