import React from "react";
import Checkbox from "./Response/Checkbox";
import Dropdown from "./Response/Dropdown";
import TextInput from "./Response/TextInput";

const SurveyResponse = ({
  item,
  onTextInputChange,
  onDropDownChange,
  onCheckboxChange,
}) => {
  const renderResponse = () => {
    if (item) {
      if (item.questionType === "TextInput") {
        return (
          <TextInput id={item.questionBankId} onChange={onTextInputChange} />
        );
      }
      if (item.questionType === "Dropdown") {
        return (
          <Dropdown
            id={item.questionBankId}
            options={item.responses}
            onChange={onDropDownChange}
          />
        );
      }
      if (item.questionType === "Checkbox") {
        return (
          <div className="d-flex justify-content-between flex-wrap my-3">
            {item.responses
              .sort((a, b) => (a.order > b.order ? 1 : -1))
              .map((response) => (
                <Checkbox
                  key={response.responseId}
                  id={`${item.questionBankId}_${response.responseId}`}
                  label={response.text}
                  onChange={onCheckboxChange}
                />
              ))}
          </div>
        );
      }
    }
    return;
  };

  return <>{renderResponse()}</>;
};

export default React.memo(SurveyResponse);
