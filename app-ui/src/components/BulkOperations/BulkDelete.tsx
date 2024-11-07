import { useEffect, useState } from "react";
import IApiService from "../../Utils/IApiService";
import config from "../../config";
import IMonth from "../../Utils/IMonth";
import IYear from "../../Utils/IYears";
import IYearListDTO from "../../DTO/YearListDTO";
import IMonthListDTO from "../../DTO/MonthListDTO";
import OptionsGenerator from "./OptionsGenerator";

interface IProps {
  apiService: IApiService | undefined;
}

export default function BulkDelete(props: IProps) {
  const [yearsList, setYearsList] = useState<IYear[]>([]);
  const [monthsList, setMonthsList] = useState<IMonth[]>([]);

  useEffect(() => {
    // fetch years and set
    props.apiService
      ?.getServiceResponse<IYearListDTO>(config.GetYearListActionMethod)
      .then((data: IYearListDTO) => {
        setYearsList(data.years);
      })
      .catch((reason) => {
        alert("Error while getting Years List from db api: " + reason);
      });

    // fetch months and set
    props.apiService
      ?.getServiceResponse<IMonthListDTO>(config.GetMonthListActionMethod)
      .then((data: IMonthListDTO) => {
        setMonthsList(data.months);
      })
      .catch((reason) => {
        alert("Error while getting Months List from db api: " + reason);
      });
  }, []);

  /**
   * Function to get name of year and month and send it to db api.
   */
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

  /**
   * Method that request deletion to the database api.
   * @param year The Year name which should be deleted.
   * @param month The month name which should be deleted.
   */
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
        <option value={0} key={0}>
          Select Year
        </option>
        {OptionsGenerator.getYearsOptions(yearsList)}
      </select>
      <select
        className="form-select mb-3 w-25 shadow-sm"
        aria-label="Month Select"
        id="month-select"
        defaultValue={0}
      >
        <option value={0} key={0}>
          Select Month
        </option>
        {OptionsGenerator.getMonthsOptions(monthsList)}
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
