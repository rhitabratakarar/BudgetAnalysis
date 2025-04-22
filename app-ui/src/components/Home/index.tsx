import IApiService from "../../Utils/IApiService";
import MonthlyExpenseCard from "../Widgets/MonthlyExpenseCard";
import MonthlyExpenseComparison from "../Widgets/MonthlyExpenseComparison";
import YearlyExpenseCard from "../Widgets/YearlyExpenseCard";
import YearlyExpenseComparison from "../Widgets/YearlyExpenseComparison";

interface IProps {
  apiService: IApiService | undefined;
}

export default function Home(props: IProps) {
  const handleScrollLeft = () => {
    (
      document.getElementById("home-cards-container") as HTMLDivElement
    ).scrollLeft -= 150;
  };

  const handleScrollRight = () => {
    (
      document.getElementById("home-cards-container") as HTMLDivElement
    ).scrollLeft += 150;
  };

  return (
    <div className="container-fluid d-flex flex-row py-2 overflow align-items-center gap-2">
      <div
        id="scroll-control-left"
        onClick={handleScrollLeft}
        style={{ borderRadius: "50%", height: "40px", width: "40px" }}
        className="btn btn-light shadow"
      >
        <i className="bi bi-caret-left"></i>
      </div>
      <div
        id="home-cards-container"
        className="container-fluid d-flex flex-row py-4 overflow-x-scroll overflow-y-hidden flex-nowrap gap-5"
      >
        <MonthlyExpenseCard />
        <YearlyExpenseCard />
        <MonthlyExpenseComparison />
        <YearlyExpenseComparison />
      </div>
      <div
        id="scroll-control-right"
        onClick={handleScrollRight}
        style={{ borderRadius: "50%", height: "40px", width: "40px" }}
        className="btn btn-light shadow"
      >
        <i className="bi bi-caret-right"></i>
      </div>
    </div>
  );
}
