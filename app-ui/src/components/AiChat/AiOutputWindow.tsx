interface IProps {}

export default function AiOutputWindow(props: IProps) {
  return (
    <div className="shadow rounded">
      <textarea
        style={{ resize: "none" }}
        className="form-control"
        id="AiOutputWindow-container"
        readOnly={true}
        rows={6}
      ></textarea>
    </div>
  );
}
