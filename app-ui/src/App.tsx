import "./App.css";
import Navbar from "./components/Common/Navbar";
import * as React from "react";
import Currentpage from "./Utils/CurrentPage";
import Home from "./components/Home";
import Insert from "./components/Insert";
import BulkDelete from "./components/BulkOperations/BulkDelete";
import Empty from "./components/Empty/Empty";
import Search from "./components/Search";
import IApiService from "./Utils/IApiService";
import ApiService from "./Utils/ApiService";
import config from "./config";

function App() {
  const [currentPage, setCurrentPage] = React.useState<Currentpage>(
    Currentpage.Home
  );

  const [apiService, setApiService] = React.useState<IApiService>(new ApiService(""));

  // initialize api service.
  React.useEffect(() => {
    const apiServ: IApiService = new ApiService(config.HTTP_DbApiURL_Base);
    setApiService(apiServ);
  }, []);

  return (
    <div className="App">
      <Navbar currentPage={currentPage} setCurrentPage={setCurrentPage} />
      {currentPage === Currentpage.Home ? <Home apiService={apiService}/> : <Empty />}
      {currentPage === Currentpage.Insert ? <Insert apiService={apiService} /> : <Empty />}
      {currentPage === Currentpage.BulkDelete ? <BulkDelete apiService={apiService} /> : <Empty />}
      {currentPage === Currentpage.Search ? <Search apiService={apiService} /> : <Empty />}
    </div>
  );
}

export default App;
