interface IApiService {
  serviceBase: string;
  getServiceResponse<T>(serviceEndpoint: string): Promise<T>;
  isServiceAvailable(testServiceEndpoint: string): Promise<boolean>;
  getServiceResponseWithQuery<T>(
    serviceEndpoint: string,
    queryParams: URLSearchParams
  ): Promise<T>;
  postServiceRequestWithBody<T>(
    serviceEndpoint: string,
    body: FormData
  ): Promise<T>;
}

export default IApiService;
