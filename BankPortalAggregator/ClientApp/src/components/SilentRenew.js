import React from 'react';
import AuthService from '../services/AuthService';

export class SilientRenew extends React.PureComponent {

    AuthService;

    componentDidMount() {
        this.AuthService = AuthService.getInstance();
        this.AuthService.signinSilentCallback().then((user) => this.onRedirectSuccess(user))
            .catch((error) => this.onRedirectError(error));
    }

    constructor(props) {
        super(props);

        this.state = {
            redirectUrl: this.props.location.state
        };
    }

    onRedirectSuccess = (user) => {
        console.log(user);
        let returnUrl = this.state.redirectUrl;
        if (returnUrl) {
            this.props.history.push(returnUrl.redirectUrl);
        } else {
            this.props.history.push("/products");
        }
    };

    onRedirectError = (error) => {
        console.log(error);
        let returnUrl = this.state.redirectUrl;
        if (returnUrl) {
            this.props.history.push(returnUrl.redirectUrl + '');
        } else {
            this.props.history.push("/products");
        }
    };

    render() {
        return (<div>Loadind...</div>);
    }
}