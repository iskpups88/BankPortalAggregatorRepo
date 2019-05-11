import React, { Component } from 'react';
import { requestHelper } from '../helpers/RequestHelper';
import { withRouter } from "react-router";

class Product extends Component {
    displayName = Product.name
    constructor(props) {
        super(props);
        this.state = {
            id: props.data.id,
            name: props.data.name,
            bankName: props.data.bank,
            percent: props.data.percent,
            minSum: props.data.minSum
        };
    }

    onClick = (e) => {
        e.preventDefault();
        let user = JSON.parse(localStorage.getItem('user'));
        if (user && user.token) {
            var options = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json', ...requestHelper.authHeader() },
                body: JSON.stringify({ Id: this.state.id })
            };

            fetch('api/Deposits/GetAuthorizeResource', options)
                .then(requestHelper.handleResponse, requestHelper.handleError)
                .then(response => console.log(response));
        } else {
            this.props.history.push('/login');
        }
        console.log(this.state.id);
    }

    render() {
        return (
            <tr>
                <td>{this.state.bankName + ', ' + this.state.name}</td>
                <td>{this.state.percent}</td>
                <td>{this.state.minSum}</td>
                <td></td>
                <td>
                    <a onClick={this.onClick} className="btn btn-primary">Открыть вклад</a>
                </td>
            </tr>
        )
    }
}

export const ProductRouter = withRouter(Product);
