import IApiService from "./IApiService";

class ApiService implements IApiService {
  serviceBase: string = "";

  constructor(serviceBase: string = "") {
    if (!serviceBase) throw new Error("Could not initialize service base.");
    this.serviceBase = serviceBase;
  }

  async isServiceAvailable(testServiceEndpoint: string): Promise<boolean> {
    const resp: Response = await fetch(new URL(testServiceEndpoint, this.serviceBase));
    if (resp.status === 200)
      return true;
    else
      return false;
  }

  async getServiceResponse(serviceEndpoint: string): Promise<unknown> {
    throw new Error("Method not implemented.");
  }
}

export default ApiService;
