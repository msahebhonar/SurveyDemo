import React from "react";

const TextArea = ({ id, onChange }) => {
  return (
    <div className="my-3">
      <textarea
        className="form-control"
        id={id}
        rows="2"
        cols="50"
        onChange={onChange}
      ></textarea>
    </div>
  );
};

export default React.memo(TextArea);
