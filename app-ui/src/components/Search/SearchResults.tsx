import TableDataResult from "./TableDataResult";

interface IProps {
  tableDataResults: TableDataResult[];
}

export default function SearchResults(props: IProps) {
  return (
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
        {props.tableDataResults.map(
          (dataResult: TableDataResult, index: number) => {
            return (
              <tr key={index}>
                <td>{dataResult.expenseType}</td>
                <td>{dataResult.year}</td>
                <td>{dataResult.month}</td>
                <td>{dataResult.expenseName}</td>
                <td>{dataResult.expenseCost}</td>
              </tr>
            );
          }
        )}
      </tbody>
    </table>
  );
}
