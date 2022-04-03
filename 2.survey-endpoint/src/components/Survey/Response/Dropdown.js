import React from "react";

const Dropdown = ({ id, options, onChange }) => {
  return (
    <select
      className="form-select my-3"
      defaultValue={"DEFAULT"}
      id={id}
      onChange={onChange}
    >
      <option value="DEFAULT" disabled>
        Choose an answer
      </option>
      {options
        .sort((a, b) => (a.order > b.order ? 1 : -1))
        .map((option) => {
          return (
            <option key={option.responseId} value={option.responseId}>
              {option.text}
            </option>
          );
        })}
    </select>
  );
};

export default React.memo(Dropdown);
