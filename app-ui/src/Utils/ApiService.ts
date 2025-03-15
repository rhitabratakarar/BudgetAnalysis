import IApiService from "./IApiService";

/**
 * This class should be used to connect to the backend service.
 */
class ApiService implements IApiService {
  serviceBase: string = "";

  constructor(serviceBase: string = "") {
    if (!serviceBase) throw new Error("Could not initialize service base.");
    this.serviceBase = serviceBase;
  }
  /**
   * This method is used to test a service whether it is connecting or not.
   * @param testServiceEndpoint This is the testing endpoint of the connecting api service.
   * @returns boolean
   */
  async isServiceAvailable(testServiceEndpoint: string): Promise<boolean> {
    const resp: Response = await fetch(
      new URL(testServiceEndpoint, this.serviceBase)
    );
    return resp.status === 200;
  }
  /**
   * This method is used to get response from a connected api service.
   * @param serviceEndpoint Endpoint of service to which request should be made.
   * @returns Promise<Response> as T
   */
  async getServiceResponse<T>(serviceEndpoint: string): Promise<T> {
    const response = await fetch(
      new URL(this.serviceBase).href + "/" + serviceEndpoint
    );
    return (await response.json()) as T;
  }

  /**
   * This method is used to retrieve a response from a targetted service endpoint while sending query params in it.
   * @param serviceEndpoint This is the request endpoint.
   * @param queryParams This is the query params to send with the request.
   * @returns Promise<Response> as T
   */
  async getServiceResponseWithQuery<T>(
    serviceEndpoint: string,
    queryParams: URLSearchParams
  ): Promise<T> {
    const response = await fetch(
      new URL(this.serviceBase).href +
        "/" +
        serviceEndpoint +
        "?" +
        queryParams.toString()
    );
    return (await response.json()) as T;
  }
  /**
   * This method is used to post a request to the api endpoint
   * @param serviceEndpoint The api endpoint to post the request
   * @param body The Form Data body to post to the server.
   * @returns Promise<T> as Response
   */
  async postServiceRequestWithBody<T>(
    serviceEndpoint: string,
    body: FormData
  ): Promise<T> {
    const response = await fetch(
      new URL(this.serviceBase).href + "/" + serviceEndpoint,
      { method: "POST", body: body }
    );
    return (await response.json()) as T;
  }

  /**
   * This method is used to send a delete request to the api endpoint
   * @param serviceEndpoint The api endpoint to post the request
   * @param queryParams The query parameters to send the delete request with.
   * @returns Promise<T> as Response
   */
  async deleteRequestWithQuery<T>(
    serviceEndpoint: string,
    queryParams: URLSearchParams
  ): Promise<T> {
    const response = await fetch(
      new URL(this.serviceBase).href +
        "/" +
        serviceEndpoint +
        "?" +
        queryParams.toString(),
      { method: "DELETE" }
    );
    return (await response.json()) as T;
  }
}

export default ApiService;
