import Currentpage from "../../Utils/CurrentPage";

interface IProps {
  currentPage: Currentpage;
  setCurrentPage: React.Dispatch<React.SetStateAction<Currentpage>>;
  setSearchStatement: React.Dispatch<React.SetStateAction<string>>;
}

export default function Navbar(props: IProps) {

  function closeSearchPage() {
    props.setCurrentPage(Currentpage.Home);
  }

  function openSearchPage() {
    props.setSearchStatement((document.getElementById("expenseSearch") as HTMLInputElement).value);
    props.setCurrentPage(Currentpage.Search);
  }

  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary shadow px-2">
      <div className="container-fluid">
        <a className="navbar-brand" href="/">
          Budget Analysis UI
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item mx-2">
              <button
                className={`nav-link ${
                  Currentpage.Home === props.currentPage ? "active" : ""
                }`}
                onClick={() => props.setCurrentPage(Currentpage.Home)}
              >
                Home
              </button>
            </li>
            <li className="nav-item dropdown mx-2">
              <button
                className={`nav-link dropdown-toggle ${
                  Currentpage.BulkDelete === props.currentPage ? "active" : ""
                }`}
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                Bulk Operations
              </button>
              <ul className="dropdown-menu">
                <li>
                  <button
                    className="dropdown-item"
                    onClick={() => props.setCurrentPage(Currentpage.BulkDelete)}
                  >
                    Bulk Delete
                  </button>
                </li>
              </ul>
            </li>
          </ul>
          <div className="d-flex" role="search">
            <input
              className="form-control mx-3"
              type="search"
              placeholder="Search an expense"
              aria-label="Search"
              id="expenseSearch"
            />
            {props.currentPage === Currentpage.Search ? (
              <button className="btn btn-outline-danger" onClick={closeSearchPage}>
                Close
              </button>
            ) : (
              <button className="btn btn-outline-success" onClick={openSearchPage}>
                Search
              </button>
            )}
          </div>
        </div>
      </div>
    </nav>
  );
}
