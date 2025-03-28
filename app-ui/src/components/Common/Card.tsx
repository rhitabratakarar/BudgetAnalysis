interface IProps {
  label: string;
  description?: string
}

export default function Card(props: IProps) {
  return (
    <div className="card text-center mb-3 shadow" style={{ width: "15rem", minWidth: "15rem" }}>
      <div className="card-body">
        <h5 className="card-title">{props.label}</h5>
        <p className="card-text">
          {props.description}
        </p>
        <a href="#" className="btn btn-primary">
          Go somewhere
        </a>
      </div>
    </div>
  );
}
