import { useEffect } from "react";
import IApiService from "../../Utils/IApiService";

interface IProps {
  apiService: IApiService | undefined;
}

export default function BulkDelete(props: IProps) {
  useEffect(() => {}, []);

  function proceedForDeletion() {
    const monthSelectElement: HTMLSelectElement = document.getElementById(
      "month-select"
    ) as HTMLSelectElement;

    const monthIndex: number = parseInt(monthSelectElement.value);
    const monthName: string = monthSelectElement.options[monthIndex].text;

    const yearSelectElement: HTMLSelectElement = document.getElementById(
      "year-select"
    ) as HTMLSelectElement;

    const yearIndex: number = parseInt(yearSelectElement.value);
    const yearName: string = yearSelectElement.options[yearIndex].text;

    if (yearIndex !== 0 && monthIndex !== 0) {
      sendDeletionRequest(yearName, monthName);
      alert(`${monthName}/${yearName} bulk delete requested.`);
    } else {
      alert(
        "Please select both year and month to initiate a deletion request."
      );
    }
  }

  async function sendDeletionRequest(year: string, month: string) {
    console.log(year, month);
  }

  return (
    <div className="container-fluid">
      <h6 className="mb-4">
        <strong>Note: </strong>This operation is irreversible.
      </h6>
      <select
        className="form-select mb-3 w-25 shadow-sm"
        aria-label="Year Select"
        id="year-select"
        defaultValue={0}
      >
        <option selected value={0}>
          Select Year
        </option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
      </select>
      <select
        className="form-select mb-3 w-25 shadow-sm"
        aria-label="Month Select"
        id="month-select"
        defaultValue={0}
      >
        <option selected value={0}>
          Select Month
        </option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
      </select>
      <button
        type="button"
        className="btn btn-danger shadow-sm"
        onClick={proceedForDeletion}
      >
        Bulk Delete
      </button>
    </div>
  );
}
