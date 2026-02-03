 // payload kako dolazi iz JWT-a
export interface JwtPayloadDto {
  sub: string;
  email: string;
  is_admin: string;
  is_owner: string;
  is_user: string;
  ver: string;
  iat: number;
  exp: number;
  aud: string;
  iss: string;
}
