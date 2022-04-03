import React from "react";

const Input = React.forwardRef(({ id, label, type, value, onChange }, ref) => {
  return (
    <div className="mb-3">
      <label htmlFor={id} className="form-label">
        {label}
      </label>
      <input
        type={type || "text"}
        className="form-control"
        id={id}
        name={id}
        value={value}
        autoComplete="off"
        ref={ref}
        onChange={onChange}
      />
    </div>
  );
});

export default React.memo(Input);
