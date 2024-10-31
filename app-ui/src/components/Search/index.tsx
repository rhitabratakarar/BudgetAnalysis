import IApiService from "../../Utils/IApiService";

interface IProps {
  apiService: IApiService;
}

export default function Search(props: IProps) {
  return (
    <div className="container-fluid">
      <table className="table table-bordered">
        <thead>
          <tr>
            <th scope="col">Expense Type</th>
            <th scope="col">Year</th>
            <th scope="col">Month</th>
            <th scope="col">Expense Name</th>
            <th scope="col">Expense Cost</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Mandatory</td>
            <td>2024</td>
            <td>February</td>
            <td>Some Expense Name</td>
            <td>Some Expense Cost</td>
          </tr>
          <tr>
            <td>Mandatory</td>
            <td>2024</td>
            <td>February</td>
            <td>Some Expense Name</td>
            <td>Some Expense Cost</td>
          </tr>
          <tr>
            <td>Mandatory</td>
            <td>2024</td>
            <td>February</td>
            <td>Some Expense Name</td>
            <td>Some Expense Cost</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
}
