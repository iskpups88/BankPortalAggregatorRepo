import React, { Component } from 'react';
import { requestHelper } from '../helpers/RequestHelper';

export class Product extends Component {
    displayName = Product.name

    getInputRef = (node) => { this._inputRef = node; }

    onClick = (e) => {
        e.preventDefault();
        let user = JSON.parse(localStorage.getItem('user'));
        if (user && user.token) {
            var options = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json', ...requestHelper.authHeader() },
                body: JSON.stringify({ Id: this.props.data.id })
            };

            fetch('api/products/GetAuthorizeResource', options)
                .then(requestHelper.handleResponse, requestHelper.handleError)
                .then(response => console.log(response));
        }
        console.log(this.props.data.id);
    }

    render() {
        return (
            <tr>
                <td>{this.props.data.bank + ', ' + this.props.data.name}</td>
                <td>{this.props.data.percent}</td>
                <td>{this.props.data.minSum}</td>
                <td></td>
                <td><a onClick={this.onClick} className="btn btn-primary">Открыть вклад</a></td>
            </tr>
        )
    }
}

