export default function Card() {
  return (
    <div className="card shadow-sm mx-2 flex-shrink-0 mb-3 flex-grow-1" style={{ width: "18rem", maxWidth: "18rem" }}>
      <img src="..." className="card-img-top" alt="" />
      <div className="card-body">
        <p className="card-text mb-2">
          Some quick example text to build on the card title and make up the
          bulk of the card's content.
        </p>
        <button className="btn btn-primary">View Details</button>
      </div>
    </div>
  );
}
