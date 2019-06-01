import React from 'react';
import AuthService from '../services/AuthService';

export class LogoutCallback extends React.PureComponent {

    AuthService;

    componentDidMount() {
        this.AuthService = AuthService.getInstance();
        this.AuthService.signoutRedirectCallback().then(() => this.onRedirectSuccess())
            .catch((error) => this.onRedirectError(error));
    }

    constructor(props) {
        super(props);

        this.state = {
            redirectUrl: this.props.location.state
        };
    }

    onRedirectSuccess = () => {
        this.AuthService.UserManager.clearStaleState();
        localStorage.clear();
        this.props.history.push("/products");
    };

    onRedirectError = (error) => {
        console.log(error);
        this.props.history.push("/products");

    };

    render() {
        return (<div>Redirecting...</div>);
    }
}