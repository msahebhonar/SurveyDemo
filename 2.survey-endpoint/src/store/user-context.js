import React, { useState, useEffect, useCallback } from "react";

const UserContext = React.createContext({
  isLoggedIn: false,
  userAccount: {},
  surveyDetail: {},
  onLogin: (data) => {},
  onLogout: () => {},
  onSurveySelected: () => {},
});

export const UserContextProvider = ({ children }) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [userAccount, setUserAccount] = useState({});
  const [surveyDetail, setSurveyDetail] = useState({});

  useEffect(() => {
    let loginInfo = localStorage.getItem("account");
    if (loginInfo) {
      loginInfo = JSON.parse(loginInfo);
      if (loginInfo.status === 1) {
        setIsLoggedIn(true);
        setUserAccount({ userId: loginInfo.userId, email: loginInfo.email });
      }
    }
  }, []);

  const logoutHandler = useCallback(() => {
    setUserAccount({});
    localStorage.removeItem("account");
    setIsLoggedIn(false);
  }, []);

  const loginHandler = useCallback(async (data) => {
    setUserAccount({ userId: data.userId, email: data.email });
    var accountInfo = { status: 1, userId: data.userId, email: data.email };
    localStorage.setItem("account", JSON.stringify(accountInfo));
    setIsLoggedIn(true);
  }, []);

  const surveyHandler = useCallback((surveyDetail) => {
    setSurveyDetail(surveyDetail);
  }, []);

  return (
    <UserContext.Provider
      value={{
        isLoggedIn: isLoggedIn,
        userAccount: userAccount,
        surveyDetail: surveyDetail,
        onLogin: loginHandler,
        onLogout: logoutHandler,
        onSurveySelected: surveyHandler,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export default UserContext;
