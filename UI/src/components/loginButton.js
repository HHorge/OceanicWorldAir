import React from "react";
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../constants/authConfig";
import Button from "react-bootstrap/Button";

import '../styles/loginButton.css'


/**
 * Renders a button which, when selected, will open a popup for login
 */
export const SignInButton = () => {
    const { instance } = useMsal();

    const handleLogin = (loginType) => {
        if (loginType === "popup") {
            instance.loginPopup(loginRequest).catch(e => {
                console.log(e);
            });
        }
    }
    return (
        <div className="login-button-wrapper">
            <Button variant="primary" className="login-button" onClick={() => handleLogin("popup")}>Sign in</Button>
        </div>
    );
}
