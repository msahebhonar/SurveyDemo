import Button from "../../UI/Button";
import styles from "./Thanks.module.css";

const Thanks = () => {
  const clickHandler = () => {
    window.location.href = "../SurveyList";
  };

  return (
    <div className="card">
      <div className={`card-body ${styles["card-survey"]}`}>
        Thank you for the time!
      </div>
      <div className="card-footer">
        <Button
          className={`btn btn-success ${styles["btn-survey"]}`}
          onClick={clickHandler}
        >
          Back
        </Button>
      </div>
    </div>
  );
};

export default Thanks;
