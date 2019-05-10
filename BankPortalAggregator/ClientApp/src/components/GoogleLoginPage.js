import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { GoogleLogout } from 'react-google-login';
import { userService } from '../services/userService';

export class GoogleLoginPage extends Component {
    displayName = GoogleLoginPage.name

    constructor(props) {
        super(props);
        let isUserSingedIn = JSON.parse(localStorage.getItem('user')) ? true : false;
        this.state = {
            isSignedIn: isUserSingedIn
        };
    }

    successResponseGoogle = (response) => {
        this.setState({ isSignedIn: true })
        console.log(response);

        userService.login(response.code);
    }

    errorResponseGoogle = (response) => {
        console.log(response);
    }

    logout = (response) => {
        console.log(response);
        this.setState({ isSignedIn: false });
        userService.logout()
    }

    render() {
        let contents = !this.state.isSignedIn ?
            <GoogleLogin
                clientId={process.env.REACT_APP_GOOGLE_CLIENT_ID}
                buttonText="Login"
                onSuccess={this.successResponseGoogle}
                onFailure={this.errorResponseGoogle}
                cookiePolicy='none'
                redirectUri='https://localhost:44345/GoogleCallBack'
                uxMode='redirect'
                accessType='offline'
                responseType='code'
                prompt='consent'
            />
            :
            <GoogleLogout
                buttonText="Logout"
                onLogoutSuccess={this.logout}
            />;

        return (
            <div>
                {contents}
            </div>
        );
    }
}
