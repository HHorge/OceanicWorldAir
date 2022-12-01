import { msalConfig } from '../constants/authConfig';
import { PublicClientApplication } from '@azure/msal-browser'
import Button from 'react-bootstrap/Button'

import React from 'react';

class LoginMethod extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isAuthenticated: false,
            user: {}
        };

        this.login = this.login.bind(this)

        this.publicClientApplication = new PublicClientApplication({
            auth: {
                clientId: msalConfig.appId,
                redirectUri: msalConfig.redirectUri
            },
            cache: {
                cacheLocation: "sessionStorage",
                storeAuthStateInCookie: true
            }
        });

    }

    async login() {
        try {
            await this.publicClientApplication.loginPopup(
                {
                    scopes: msalConfig.scopes,
                    prompt: "select_account"
                }
            );
           
                this.props.setLoggedIn(true)
                console.log("signed in success")

        }
        catch (err) {
            this.setState({
                isAuthenticated: false,
                user: {},
                error: err
            })
        }

    };

    async logout() {
        this.publicClientApplication.logoutPopup();
    };

    render() {
        return(
            <Button onClick={() => {this.login()}}>
                Log in
            </Button>
        )
    }

}

export default LoginMethod;