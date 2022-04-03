import { useState, useEffect, useContext, useCallback } from "react";
import UseHttp from "../../hooks/use-http";
import UserContext from "../../store/user-context";
import Table from "./Table/Table";
import Survey from "./Survey";
import Alert from "../UI/Alert";
import Config from "../../config.json";

const SurveyList = () => {
  const [surveyDetail, setSurveyDetail] = useState([]);
  const [isSelected, setIsSelected] = useState(false);
  const ctx = useContext(UserContext);
  const { isLoading, sendRequest } = UseHttp();

  useEffect(() => {
    const manageSurvey = (data) => {
      setSurveyDetail(data);
    };

    sendRequest(
      {
        url: `${Config.SERVER_URL}/Survey/GetSurveyList?userAccountId=${ctx.userAccount.userId}`,
      },
      manageSurvey
    );
  }, [sendRequest, ctx.userAccount.userId]);

  const viewSurveyHandler = useCallback(
    (surveyDetail) => {
      ctx.onSurveySelected(surveyDetail);
      setIsSelected(true);
    },
    [ctx]
  );

  return (
    <>
      <div className="container mt-3">
        <div className="row">
          <div className="col-12">
            {isSelected ? (
              <Survey />
            ) : (
              <>
                <div className="mb-3">
                  <h4>List of Active Surveys</h4>
                </div>
                {isLoading && <Alert message="Please wait ..." />}
                {!isLoading && (
                  <Table
                    columns={["Action", "Title", "Description"]}
                    data={surveyDetail}
                    onView={viewSurveyHandler}
                  />
                )}
              </>
            )}
          </div>
        </div>
      </div>
    </>
  );
};

export default SurveyList;
