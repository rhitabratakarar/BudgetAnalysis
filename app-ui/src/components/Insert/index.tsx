import { useEffect, useState } from "react";
import IApiService from "../../Utils/IApiService";
import config from "../../config";
import IYearListDTO from "../../DTO/IYearListDTO";
import IMonthListDTO from "../../DTO/IMonthListDTO";
import { ExpenseType } from "../../Utils/ExpenseType";
import IYear from "../../Utils/IYear";
import IMonth from "../../Utils/IMonth";

interface IProps {
  apiService: IApiService | undefined;
}

export default function Insert(props: IProps) {
  let [yearList, setYearList] = useState<IYearListDTO>();
  let [monthList, setMonthList] = useState<IMonthListDTO>();

  useEffect(() => {
    // call the api service to get year and month fields.

    let years: Promise<IYearListDTO> | undefined =
      props.apiService?.getServiceResponse<IYearListDTO>(
        config.GetYearListActionMethod
      );
    let months: Promise<IMonthListDTO> | undefined =
      props.apiService?.getServiceResponse<IMonthListDTO>(
        config.GetMonthListActionMethod
      );

    // assign the values to states if the values are resolved.

    years?.then((data) => {
      setYearList(data);
    });

    months?.then((data) => {
      setMonthList(data);
    });
  }, []);

  return (
    <div className="container-fluid">
      <select
        className="form-select my-4 w-25 shadow-sm"
        aria-label="Year Select"
        defaultValue={0}
      >
        <option value={0} key={"year-0"}>
          Select Year
        </option>
        {yearList?.years.map((year: IYear) => {
          return (
            <option value={year.id} key={`year-${year.id}`}>
              {year.yearCode}
            </option>
          );
        })}
      </select>
      <select
        className="form-select my-4 w-25 shadow-sm"
        aria-label="Month Select"
        defaultValue={0}
      >
        <option value={0} key={"month-0"}>
          Select Month
        </option>
        {monthList?.months.map((month: IMonth) => {
          return (
            <option value={month.id} key={`month-${month.id}`}>
              {month.monthName}
            </option>
          );
        })}
      </select>
      <select
        className="form-select my-4 w-25 shadow-sm"
        aria-label="Expense Type Select"
        defaultValue={0}
      >
        <option value={0} key={"expense-0"}>
          Select Expense type
        </option>
        <option
          value={ExpenseType.Mandatory}
          key={`expense-${ExpenseType.Mandatory}`}
        >
          Mandatory
        </option>
        <option
          value={ExpenseType.Optional}
          key={`expense-${ExpenseType.Optional}`}
        >
          Optional
        </option>
      </select>
      <div className="my-4 w-25">
        <label htmlFor="ExpenseName" className="form-label">
          Expense Name
        </label>
        <input
          type="text"
          className="form-control shadow-sm"
          id="ExpenseName"
        />
      </div>
      <div className="my-4 w-25">
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
