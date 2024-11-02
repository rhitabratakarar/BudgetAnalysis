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
   */
  async getServiceResponse<T>(serviceEndpoint: string): Promise<T> {
    throw new Error("Method not implemented.");
  }
}

export default ApiService;
