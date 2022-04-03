import { useContext } from "react";
import UserContext from "../../store/user-context";
import Button from "../UI/Button";
import styles from "./NavBar.module.css";

const NavBar = () => {
  const ctx = useContext(UserContext);

  return (
    <nav
      className={`navbar navbar-expand-lg navbar-light ${styles["bg-survey"]}`}
    >
      <div className="container-fluid">
        <span className={`navbar-brand ${styles["survey-brand"]}`} href="#">
          Welcome {ctx.userAccount.email}!
        </span>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#logout"
          aria-controls="logout"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div
          className="collapse navbar-collapse justify-content-md-end"
          id="logout"
        >
          <Button
            className={`btn btn-outline-dark ${styles["btn-survey"]}`}
            type="button"
            onClick={() => ctx.onLogout()}
          >
            Logout
          </Button>
        </div>
      </div>
    </nav>
  );
};

export default NavBar;
