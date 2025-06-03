import UserInputWindow from "./UserInputWindow";
import AiOutputWindow from "./AiOutputWindow";

interface IProps {}

export default function AiChat(props: IProps) {
  return (
    <div className="container text-center">
      <div className="row">
        <div className="col-sm-6">
          <UserInputWindow />
        </div>
        <div className="col-sm-6">
          <AiOutputWindow />
        </div>
      </div>
    </div>
  );
}
