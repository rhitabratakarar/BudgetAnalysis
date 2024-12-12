import { useEffect, useState } from "react";
import IApiService from "../../Utils/IApiService";
import SearchResults from "./SearchResults";
import config from "../../config";
import TableDataResult from "./TableDataResult";

interface IProps {
  apiService: IApiService | undefined;
  searchStatement: string;
}

export default function Search(props: IProps) {
  const [tableDataResults, setTableDataResults] = useState([new TableDataResult()]);

  useEffect(() => {
    if (props.searchStatement.trim() !== "") {
      const queryParams: URLSearchParams = new URLSearchParams({
        searchStatement: props.searchStatement,
      });
      props.apiService
        ?.getServiceResponseWithQuery<TableDataResult[]>(
          config.GetSearchResults,
          queryParams
        )
        .then((data) => {
          setTableDataResults(data);
        });
    }
  }, []);

  return (
    <div className="container-fluid">
      <SearchResults tableDataResults={tableDataResults} />
    </div>
  );
}
