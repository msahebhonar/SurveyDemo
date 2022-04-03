import { useState, useCallback } from "react";
import Input from "../UI/Input";
import Button from "../UI/Button";
import styles from "./Login.module.css";
import img from "../../assets/play.svg";
import Alert from "../UI/Alert";

const Login = ({ isLoading, error, onLogin }) => {
  const [inputEmail, setInputEmail] = useState("");
  const [inputPassword, setInputPassword] = useState("");
  const [formIsValid, setFormIsValid] = useState(true);

  const emailChangeHandler = useCallback((event) => {
    setInputEmail(event.target.value);
  }, []);

  const passwordChangeHandler = useCallback((event) => {
    setInputPassword(event.target.value);
  }, []);

  const loginHandler = useCallback(
    (event) => {
      event.preventDefault();

      if (inputEmail.trim() === "" || inputPassword.trim() === "") {
        setFormIsValid(false);
        return;
      }
      setFormIsValid(true);
      onLogin(inputEmail, inputPassword);
      setInputEmail("");
      setInputPassword("");
    },
    [inputEmail, inputPassword, onLogin]
  );

  let message = null;
  if (isLoading) {
    message = "Please wait ...";
  }
  if (!formIsValid) {
    message = "Invalid Email or Password!";
  }
  if (error) {
    message = `Error code: ${error}`;
  }

  return (
    <div className="container mt-5">
      <div className="row">
        <div className="col-lg-4"></div>
        <div className="col-lg-4">
          <form onSubmit={loginHandler}>
            <Input
              type="email"
              id="email"
              name="email"
              label="Email"
              value={inputEmail}
              onChange={emailChangeHandler}
            />
            <Input
              type="password"
              id="password"
              name="email"
              label="Password"
              value={inputPassword}
              onChange={passwordChangeHandler}
            />
            <div className="d-grid">
              <Button
                type="submit"
                className={`btn btn-success ${styles["btn-survey"]}`}
              >
                Login <img src={img} alt="login" height="10" width="10" />
              </Button>
            </div>
            {message !== null ? <Alert message={message} /> : ""}
          </form>
        </div>
        <div className="col-lg-4"></div>
      </div>
    </div>
  );
};

export default Login;
