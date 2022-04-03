import { useContext, useCallback } from "react";
import Home from "./components/Home";
import Login from "./components/Login/Login";
import UseHttp from "./hooks/use-http";
import UserContext from "./store/user-context";
import Config from "./config.json";

function App() {
  const ctx = useContext(UserContext);
  const { isLoading, error, sendRequest } = UseHttp();

  const loginHandler = useCallback(
    async (email, password) => {
      sendRequest(
        {
          url: `${Config.SERVER_URL}/UserAccount/Login`,
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: { email, password },
        },
        ctx.onLogin
      );
    },
    [sendRequest, ctx.onLogin]
  );

  return (
    <>
      {!ctx.isLoggedIn && (
        <Login isLoading={isLoading} error={error} onLogin={loginHandler} />
      )}
      {ctx.isLoggedIn && <Home />}
    </>
  );
}

export default App;
