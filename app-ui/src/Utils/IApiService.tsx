interface IApiService {
    serviceBase: string;
    getServiceResponse(serviceEndpoint: string): unknown;
}

export default IApiService;
