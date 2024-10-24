export default function BulkDelete() {
  return (
    <div className="container-fluid">
      <h6 className="mb-4">
        <strong>Note: </strong>This operation is irreversible.
      </h6>
      <select className="form-select mb-3 w-25" aria-label="Year Select">
        <option selected>Select Year</option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
      </select>
      <select className="form-select mb-3 w-25" aria-label="Month Select">
        <option selected>Select Month</option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
      </select>
      <button type="button" className="btn btn-danger">
        Delete
      </button>
    </div>
  );
}
