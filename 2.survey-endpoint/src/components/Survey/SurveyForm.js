import {
  useState,
  useReducer,
  useEffect,
  useContext,
  useCallback,
} from "react";
import SurveyResponse from "./SurveyResponse";
import styles from "./SurveyForm.module.css";
import Button from "../UI/Button";
import Alert from "../UI/Alert";
import UserContext from "../../store/user-context";
import UseHttp from "../../hooks/use-http";
import Thanks from "./Response/Thanks";
import Config from "../../config.json";

const defaultValue = {
  items: [],
};

const answerReducer = (state, action) => {
  if (action.type === "ADD") {
    let itemIndex;
    if (
      action.item.questionType === "textInput" ||
      action.item.questionType === "dropdown"
    ) {
      itemIndex = state.items.findIndex(
        (item) => item.questionBankId === action.item.questionBankId
      );
    } else if (action.item.questionType === "checkbox") {
      itemIndex = state.items.findIndex(
        (item) =>
          item.questionBankId === action.item.questionBankId &&
          item.responseId === action.item.responseId
      );
    }
    const existingItem = state.items[itemIndex];
    let updateItems;
    if (existingItem) {
      let updateItem = {};
      if (existingItem.questionType === "dropdown") {
        updateItem = { ...existingItem, responseId: action.item.responseId };
        updateItems = [...state.items];
        updateItems[itemIndex] = updateItem;
      } else if (existingItem.questionType === "textInput") {
        if (action.item.responseId.trim() !== "") {
          updateItem = { ...existingItem, responseId: action.item.responseId };
          updateItems = [...state.items];
          updateItems[itemIndex] = updateItem;
        } else {
          updateItems = state.items.filter(
            (item) => item.questionBankId !== action.item.questionBankId
          );
        }
      } else if (existingItem.questionType === "checkbox") {
        updateItems = state.items.filter(
          (item) =>
            !(
              item.questionBankId === action.item.questionBankId &&
              item.responseId === action.item.responseId
            )
        );
      }
    } else {
      if (
        action.item.questionType === "textInput" &&
        action.item.responseId.trim() === ""
      ) {
        return;
      } else {
        updateItems = state.items.concat(action.item);
      }
    }
    return {
      items: updateItems,
    };
  }
  return defaultValue;
};

const SurveyForm = ({ questions }) => {
  const [originalQuestionIds, setOriginalQuestionIds] = useState([]);
  const [answerState, dispatchAnswer] = useReducer(answerReducer, defaultValue);
  const [formError, setFormError] = useState(null);
  const [isSubmitted, setIsSubmitted] = useState(false);
  const ctx = useContext(UserContext);
  const { isLoading, error, sendRequest } = UseHttp();

  useEffect(() => {
    // store question ids to check if all questions have answer
    if (questions !== null) {
      setOriginalQuestionIds(questions.map((item) => item.questionBankId));
    }
  }, [questions]);

  const inputChangeHandler = useCallback((event) => {
    const item = {
      questionBankId: event.target.id,
      questionType: "textInput",
      responseId: event.target.value,
    };
    dispatchAnswer({ type: "ADD", item: item });
  }, []);

  const dropdownChangeHandler = useCallback((event) => {
    const item = {
      questionBankId: event.target.id,
      questionType: "dropdown",
      responseId: event.target.value,
    };
    dispatchAnswer({ type: "ADD", item: item });
  }, []);

  const checkboxChangeHandler = useCallback((event) => {
    const values = event.target.id.split("_");
    // console.log(event.currentTarget.checked);
    const item = {
      questionBankId: values[0],
      questionType: "checkbox",
      responseId: values[1],
    };
    dispatchAnswer({ type: "ADD", item: item });
  }, []);

  const manageSubmit = useCallback((data) => {
    if (data.status === 0) {
      setIsSubmitted(true);
    } else {
      setIsSubmitted(false);
    }
  }, []);

  const fetchData = useCallback(
    async (result) => {
      sendRequest(
        {
          url: `${Config.SERVER_URL}/Response/SaveAndSubmit`,
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: result,
        },
        manageSubmit
      );
    },
    [sendRequest, manageSubmit]
  );

  const submitFormHandler = useCallback(
    (event) => {
      event.preventDefault();

      // get answered question ids
      const answeredQuestionsIds = [
        ...new Set(answerState.items.map((item) => +item.questionBankId)),
      ];

      // check all questions are answered
      if (answeredQuestionsIds.length !== originalQuestionIds.length) {
        setFormError("All questions are required!");
        return;
      }

      answeredQuestionsIds.sort();
      if (
        !originalQuestionIds.every(
          (item, index) => item === answeredQuestionsIds[index]
        )
      ) {
        setFormError("All questions are required!");
        return;
      }

      setFormError(null);
      // create final object to sent to server
      const respondentAnswer = answerState.items.map((item) => {
        return { questionBankId: item.questionBankId, answer: item.responseId };
      });

      const results = {
        userAccountId: ctx.userAccount.userId,
        surveyDetailId: ctx.surveyDetail.id,
        respondentAnswerDto: respondentAnswer,
      };

      fetchData(results);
    },
    [
      originalQuestionIds,
      answerState.items,
      ctx.userAccount.userId,
      ctx.surveyDetail.id,
      fetchData,
    ]
  );

  return (
    <>
      {isSubmitted && <Thanks />}
      {!isSubmitted && (
        <form onSubmit={submitFormHandler}>
          <ul className="list-group list-group-flush">
            <li className={`list-group-item ${styles["list-group-survey"]}`}>
              {formError !== null && <Alert message={formError} />}
              {isLoading && <Alert message="Please wait ..." />}
              {error && <Alert message={`Error code: ${error}`} />}
            </li>
            {questions !== null &&
              questions.map((item, index) => (
                <li
                  className={`list-group-item ${styles["list-group-survey"]}`}
                  key={index}
                >
                  <span className="lead">
                    {item.questionBankId}-{item.text}
                  </span>
                  <SurveyResponse
                    item={item}
                    onTextInputChange={inputChangeHandler}
                    onDropDownChange={dropdownChangeHandler}
                    onCheckboxChange={checkboxChangeHandler}
                  />
                </li>
              ))}
            <li className={`list-group-item ${styles["list-group-survey"]}`}>
              <div className="text-center col-md-12">
                <Button
                  type="submit"
                  className={`btn btn-success ${styles["btn-survey"]}`}
                >
                  Submit
                </Button>
              </div>
            </li>
          </ul>
        </form>
      )}
    </>
  );
};

export default SurveyForm;
