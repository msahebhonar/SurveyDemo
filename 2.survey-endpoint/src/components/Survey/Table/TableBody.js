import React from "react";
import img from "../../../assets/eye.svg";

const TableBody = ({ data, onClick }) => {
  const clickHandler = (event) => {
    const surveyDetail = {
      id: event.target.getAttribute("data-survey-id"),
      title: event.target.getAttribute("data-survey-title"),
    };
    onClick(surveyDetail);
  };

  return (
    <tbody>
      {data.length === 0 ? (
        <tr>
          <td colSpan="6">There is no active survey</td>
        </tr>
      ) : (
        data.map((item, index) => {
          return (
            <tr key={index}>
              <td>
                <button
                  type="button"
                  className="btn btn-warning btn-sm"
                  onClick={clickHandler}
                  data-survey-id={item.surveyDetailId}
                  data-survey-title={item.title}
                >
                  <img
                    src={img}
                    alt="view"
                    height="25"
                    width="25"
                    data-survey-id={item.surveyDetailId}
                    data-survey-title={item.title}
                  />
                </button>
              </td>
              <td>{item.title}</td>
              <td>
                <span className="text-truncate">{item.description}</span>
              </td>
            </tr>
          );
        })
      )}
    </tbody>
  );
};

export default React.memo(TableBody);
