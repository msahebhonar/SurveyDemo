const Alert = ({ message }) => {
  return (
    <div className="alert alert-warning mt-3" role="alert">
      {message}
    </div>
  );
};

export default Alert;
