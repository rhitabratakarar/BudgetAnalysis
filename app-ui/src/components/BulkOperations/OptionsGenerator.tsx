import IMonth from "../../Utils/IMonth";
import IYear from "../../Utils/IYears";

export default class OptionsGenerator {
  /**
   * static method to generate list of <options> elements based on given array
   * @param yearsList list of IYear
   * @returns list of options based on IYear
   */
  static getYearsOptions(
    yearsList: IYear[]
  ): React.DetailedHTMLProps<
    React.OptionHTMLAttributes<HTMLOptionElement>,
    HTMLOptionElement
  >[] {
    let res = [];
    for (let i: number = 0; i < yearsList.length; i++) {
      res.push(
        <option
          id={yearsList[i].id.toString()}
          key={yearsList[i].id.toString()}
          value={i + 1}
        >
          {yearsList[i].yearCode.toString()}
        </option>
      );
    }
    return res;
  }

  /**
   * static method to generate list of <options> elements based on given array
   * @param monthsList list of IMonth
   * @returns list of options based on IMonth
   */
  static getMonthsOptions(
    monthsList: IMonth[]
  ): React.DetailedHTMLProps<
    React.OptionHTMLAttributes<HTMLOptionElement>,
    HTMLOptionElement
  >[] {
    let res = [];
    for (let i: number = 0; i < monthsList.length; i++) {
      res.push(
        <option
          id={monthsList[i].id.toString()}
          key={monthsList[i].id.toString()}
          value={i + 1}
        >
          {monthsList[i].monthName.toString()}
        </option>
      );
    }
    return res;
  }
}
