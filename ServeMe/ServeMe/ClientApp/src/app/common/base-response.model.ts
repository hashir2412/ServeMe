export class BaseResponseModel<T>{
    statusCode: number;
    message: string;
    body: T;
}