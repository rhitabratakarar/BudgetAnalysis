import "./App.css";
import Navbar from "./components/Common/Navbar";
import * as React from "react";
import Currentpage from "./Utils/CurrentPage";
import Home from "./components/Home";
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

  const [apiService, setApiService] = React.useState<IApiService>();
  const [searchStatement, setSearchStatement] = React.useState<string>("");

  // initialize api service.
  React.useEffect(() => {
    const url: URL = new URL(
      config.BudgetAnalysisApiController,
      config.HTTP_DbApiURL_Base
    );
    const apiServ: IApiService = new ApiService(url.toString());
    setApiService(apiServ);
  }, []);

  return (
    <div className="App">
      <Navbar
        currentPage={currentPage}
        setCurrentPage={setCurrentPage}
        setSearchStatement={setSearchStatement}
      />
      <div className="px-3 py-4">
        {currentPage === Currentpage.Home ? (
          <Home apiService={apiService} />
        ) : (
          <Empty />
        )}
        {currentPage === Currentpage.BulkDelete ? (
          <BulkDelete apiService={apiService} />
        ) : (
          <Empty />
        )}
        {currentPage === Currentpage.Search ? (
          <Search apiService={apiService} searchStatement={searchStatement} />
        ) : (
          <Empty />
        )}
      </div>
    </div>
  );
}

export default App;
