import "./App.css";
import Navbar from "./components/Navbar";
import * as React from "react";
import Currentpage from "./Utils/CurrentPage";

function App() {
  const [currentPage, setCurrentPage] = React.useState<Currentpage>(
    Currentpage.Home
  );

  return (
    <div className="App">
      <Navbar currentPage={currentPage} setCurrentPage={setCurrentPage} />
    </div>
  );
}

export default App;
