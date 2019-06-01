import React, { Component } from 'react';
import AuthService from '../services/AuthService';

export class Login extends Component {

    AuthService;

    constructor(props) {
        super(props);
        this.AuthService = AuthService.getInstance();
    }

    onLogin = (e) => {
        e.preventDefault();
        this.AuthService.signinRedirect();
    }

    onLogout = (e) => {
        e.preventDefault();
        this.AuthService.logout();
    }

    render() {
        let button = !this.AuthService.isAuthenticated() ?
            <button className="btn btn-primary btn-login" style={{ margin: '10px' }} onClick={this.onLogin}>
                Login
            </button>
            :
            <button className="btn btn-primary btn-login" style={{ margin: '10px' }} onClick={this.onLogout}>
                Logout
            </button>;

        return (
            <div className="row">
                <div className="col-md-12 text-left" style={{ marginTop: '30px' }}>
                    {button}
                </div>
            </div >
        );
    }
}