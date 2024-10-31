interface IApiService {
    serviceBase: string;
    getServiceResponse(serviceEndpoint: string): Promise<unknown>;
    isServiceAvailable(testServiceEndpoint: string): Promise<boolean>;
}

export default IApiService;
