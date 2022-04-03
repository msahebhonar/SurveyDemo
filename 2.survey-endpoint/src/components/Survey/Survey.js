import { useState, useEffect, useContext } from "react";
import UseHttp from "../../hooks/use-http";
import UserContext from "../../store/user-context";
import Alert from "../UI/Alert";
import styles from "./Survey.module.css";
import SurveyForm from "./SurveyForm";
import Config from "../../config.json";

const Survey = () => {
  const [questions, setQuestions] = useState(null);
  const { isLoading, error, sendRequest } = UseHttp();
  const ctx = useContext(UserContext);

  useEffect(() => {
    const manageQuestions = (data) => {
      setQuestions(data);
    };

    sendRequest(
      {
        url: `${Config.SERVER_URL}/Survey/GetSurveyQuestions?surveyDetailId=${ctx.surveyDetail.id}`,
      },
      manageQuestions
    );
  }, [sendRequest, ctx.surveyDetail.id]);

  let message = null;
  if (isLoading) {
    message = "Please wait ...";
  }
  if (error) {
    message = `Error Code: ${error}`;
  }

  return (
    <>
      {message !== null && <Alert message={message} />}
      <div>
        <h2 className={`my-3 ${styles["text-survey"]}`}>
          {ctx.surveyDetail.title}
        </h2>
        <SurveyForm questions={questions} />
      </div>
    </>
  );
};

export default Survey;
