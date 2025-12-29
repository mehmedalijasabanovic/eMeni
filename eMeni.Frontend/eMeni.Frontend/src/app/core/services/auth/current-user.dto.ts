export interface CurrentUserDto {
  userId: number;
  email: string;
  isAdmin: boolean;
  isOwner: boolean;
  isUser: boolean;
  tokenVersion: number;
}
