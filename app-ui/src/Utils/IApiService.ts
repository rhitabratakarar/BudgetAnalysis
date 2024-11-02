interface IApiService {
    serviceBase: string;
    getServiceResponse<T>(serviceEndpoint: string): Promise<T>;
    isServiceAvailable(testServiceEndpoint: string): Promise<boolean>;
}

export default IApiService;
