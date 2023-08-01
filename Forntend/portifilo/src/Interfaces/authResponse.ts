export default interface authResponse{
    data?: {
        code: number;
        token: string;
    };
    error?: any;
}
