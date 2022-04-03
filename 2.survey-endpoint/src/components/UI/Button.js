import React from "react";

const Button = ({ type, children, className, onClick }) => {
  return (
    <button className={className} type={type || "button"} onClick={onClick}>
      {children}
    </button>
  );
};

export default React.memo(Button);
