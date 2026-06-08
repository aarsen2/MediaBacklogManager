export interface AuthResponse {
    token: string,
    expiresIn?: number,
    refreshToken?: string
}