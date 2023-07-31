export default interface authResponse{
    data?: {
        code: number;
        email: string;
        role: string;
        token: string;
    };
    error?: any;
}
