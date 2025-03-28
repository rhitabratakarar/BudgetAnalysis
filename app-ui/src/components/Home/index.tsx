import { useRef } from "react";
import IApiService from "../../Utils/IApiService";
import MonthlyExpenseCard from "../Widgets/MonthlyExpenseCard";

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
    <div className="container-fluid d-flex flex-row py-4 overflow">
      <button id="scroll-control-left" onClick={handleScrollLeft}>
        Test
      </button>
      <div
        id="home-cards-container"
        className="container-fluid d-flex flex-row py-4 overflow-x-scroll overflow-y-hidden flex-nowrap gap-5"
      >
        <MonthlyExpenseCard />
        <MonthlyExpenseCard />
        <MonthlyExpenseCard />
        <MonthlyExpenseCard />
        <MonthlyExpenseCard />
        <MonthlyExpenseCard />
      </div>
      <button id="scroll-control-right" onClick={handleScrollRight}>
        Test
      </button>
    </div>
  );
}
