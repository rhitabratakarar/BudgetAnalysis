import "./App.css";
import Navbar from "./components/Navbar";
import * as React from "react";
import Currentpage from "./Utils/CurrentPage";
import Home from "./components/Home";
import Insert from "./components/Insert";
import Delete from "./components/Delete";
import BulkDelete from "./components/BulkOperations/BulkDelete";
import Empty from "./components/Empty/Empty";

function App() {
  const [currentPage, setCurrentPage] = React.useState<Currentpage>(
    Currentpage.Home
  );

  return (
    <div className="App">
      <Navbar currentPage={currentPage} setCurrentPage={setCurrentPage} />
      {currentPage === Currentpage.Home ? <Home /> : <Empty />}
      {currentPage === Currentpage.Insert ? <Insert /> : <Empty />}
      {currentPage === Currentpage.Delete ? <Delete /> : <Empty />}
      {currentPage === Currentpage.BulkDelete ? <BulkDelete /> : <Empty />}
    </div>
  );
}

export default App;
