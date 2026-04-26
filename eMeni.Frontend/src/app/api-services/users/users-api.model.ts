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

export interface GetUserByIdDto {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  phone: string;
  city: string;
}

export interface UpdateUserCommand {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
}

export interface ChangePasswordCommand {
  currentPassword: string;
  newPassword: string;
}
