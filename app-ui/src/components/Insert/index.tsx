import IApiService from "../../Utils/IApiService";

interface IProps {
  apiService: IApiService;
}

export default function Insert(props: IProps) {
  return (
    <div className="container-fluid">
      <select
        className="form-select mb-3 w-25 shadow-sm"
        aria-label="Year Select"
      >
        <option selected>Select Year</option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
      </select>
      <select
        className="form-select mb-3 w-25 shadow-sm"
        aria-label="Month Select"
      >
        <option selected>Select Month</option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
      </select>
      <select
        className="form-select mb-3 w-25 shadow-sm"
        aria-label="Expense Type Select"
      >
        <option selected>Select Expense type</option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
      </select>
      <div className="mb-3 w-25">
        <label htmlFor="ExpenseName" className="form-label">
          Expense Name
        </label>
        <input
          type="text"
          className="form-control shadow-sm"
          id="ExpenseName"
        />
      </div>
      <div className="mb-3 w-25">
        <label htmlFor="ExpenseCost" className="form-label">
          Expense Cost
        </label>
        <input
          type="number"
          className="form-control shadow-sm"
          id="ExpenseCost"
        />
      </div>
      <button type="button" className="btn btn-success shadow-sm">
        Insert
      </button>
    </div>
  );
}
