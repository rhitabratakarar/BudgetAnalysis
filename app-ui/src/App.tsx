import "./App.css";
import Navbar from "./components/Navbar";
import * as React from "react";
import Currentpage from "./Utils/CurrentPage";
import Home from "./components/Home";
import Insert from "./components/Insert";
import Delete from "./components/Delete";
import BulkDelete from "./components/BulkOperations/BulkDelete";


function App() {
  const [currentPage, setCurrentPage] = React.useState<Currentpage>(
    Currentpage.Home
  );

  function getPageMapping(): React.ReactNode {
    if (currentPage === Currentpage.Home)
      return <Home />
    if (currentPage === Currentpage.Insert)
      return <Insert />
    if (currentPage === Currentpage.Delete)
      return <Delete />
    if (currentPage === Currentpage.BulkDelete)
      return <BulkDelete />
  }

  return (
    <div className="App">
      <Navbar currentPage={currentPage} setCurrentPage={setCurrentPage} />
      {getPageMapping()}
    </div>
  );
}

export default App;
