// core/models/user.model.ts
export interface UserDto {
  id: string;
  username: string;
  displayName: string;

  email?: string;
  avatarUrl?: string;

  roles?: string[];
}