export interface CreateUserCommand {
  email: string;
  passwordHash: string;
  firstName: string;
  lastName: string;
  phone: string;
  cityId: number;
}

export interface CreateUserResponse {
  id: number;
}
