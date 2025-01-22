import IMonth from "../../Utils/IMonth";
import IYear from "../../Utils/IYear";

export default class OptionsGenerator {
  static getYearsOptions(yearsList: IYear[]): any[] {
    let res: any = [];
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

  static getMonthsOptions(monthsList: IMonth[]): any[] {
    let res: any = [];
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
