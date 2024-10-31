import "./App.css";
import Navbar from "./components/Common/Navbar";
import * as React from "react";
import Currentpage from "./Utils/CurrentPage";
import Home from "./components/Home";
import Insert from "./components/Insert";
import BulkDelete from "./components/BulkOperations/BulkDelete";
import Empty from "./components/Empty/Empty";
import Search from "./components/Search";

function App() {
  const [currentPage, setCurrentPage] = React.useState<Currentpage>(
    Currentpage.Home
  );

  return (
    <div className="App">
      <Navbar currentPage={currentPage} setCurrentPage={setCurrentPage} />
      {currentPage === Currentpage.Home ? <Home /> : <Empty />}
      {currentPage === Currentpage.Insert ? <Insert /> : <Empty />}
      {currentPage === Currentpage.BulkDelete ? <BulkDelete /> : <Empty />}
      {currentPage === Currentpage.Search ? <Search /> : <Empty />}
    </div>
  );
}

export default App;
