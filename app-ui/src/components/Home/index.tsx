import IApiService from "../../Utils/IApiService";
import Card from "../Common/Card";

interface IProps {
  apiService: IApiService | undefined;
}

export default function Home(props: IProps) {
  return (
    <div className="container-fluid d-flex flex-row flex-wrap">
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
    </div>
  );
}
