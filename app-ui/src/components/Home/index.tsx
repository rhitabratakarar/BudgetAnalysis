import IApiService from "../../Utils/IApiService";
import Card from "../Common/Card";

interface IProps {
  apiService: IApiService;
}

export default function Home(props: IProps) {
  return (
    <div className="container-fluid d-flex flex-row flex-wrap justify-content-between">
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
    </div>
  );
}
