import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import { GoogleLogout } from 'react-google-login';

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

        var options = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ code: response.code })
        };

        fetch('api/account/login', options)
            .then(this.handleResponse, this.handleError)
            .then(user => {
                console.log(user);
                if (user && user.token) {
                    localStorage.setItem('user', JSON.stringify(user));
                }
            });
    }

    errorResponseGoogle = (response) => {
        console.log(response);
    }

    logout = (response) => {
        this.setState({ isSignedIn: false });
        localStorage.removeItem('user');
        console.log(response);
    }

    handleResponse(response) {
        return new Promise((resolve, reject) => {
            if (response.ok) {
                // return json if it was returned in the response
                var contentType = response.headers.get("content-type");
                if (contentType && contentType.includes("application/json")) {
                    response.json().then(json => resolve(json));
                } else {
                    resolve();
                }
            } else {
                // return error message from response body
                response.text().then(text => reject(text));
            }
        });
    }

    handleError(error) {
        return Promise.reject(error && error.message);
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
