interface IProps {}

export default function UserInputWindow(props: IProps) {
  return (
    <div className="shadow rounded">
      <textarea style={{resize: "none"}}
        className="form-control"
        id="UserInputWindow-container"
        placeholder="Type your query here..."
        rows={6}
      ></textarea>
    </div>
  );
}
