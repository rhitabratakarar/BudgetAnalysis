import IApiService from "./IApiService";

class ApiService implements IApiService {
  serviceBase: string = "";

  constructor(serviceBase: string = "") {
    if (!serviceBase) throw new Error("Could not initialize service base.");
    this.serviceBase = serviceBase;
  }

  getServiceResponse(serviceEndpoint: string): unknown {
    throw new Error("Method not implemented.");
  }
}

export default ApiService;
