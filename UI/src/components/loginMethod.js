import { config } from '../config';
import {PublicClientApplication} from '@azure/msal-browser'
import React from 'react';

class loginMethod extends React.Component {
    constructor(props){
        super(props);
        this.state ={
            error: null,
            isAuthenticated: false,
            user: {}
        };

        this.login = this.login.bind(this)

        this.publicClientApplication = new PublicClientApplication({
            auth: {
                clientId: config.appId,
                redirectUri: config.redirectUri
            },
            cache: {
                cacheLocation: "sessionStorage",
                storeAuthStateInCookie: true
            }
        });

    }

    async login(){
        try{
            await this.publicClientApplication.loginPopup(
                {
                    scopes: config.scopes,
                    prompt: "select_account"
                }
            );
            this.state.isAuthenticated=true
            console.log("signed in success")

        }
        catch(err){
            this.setState({
                isAuthenticated: false,
                user: {},
                error: err
            })
        }

    };

    async logout(){
        this.publicClientApplication.logoutPopup();
    };
}

export default loginMethod;