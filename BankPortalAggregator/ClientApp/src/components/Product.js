import React, { Component } from 'react';
import { requestHelper } from '../helpers/RequestHelper';
import { withRouter } from "react-router";
import InputRange from 'react-input-range';
import Dropdown from 'react-dropdown'
import 'react-input-range/lib/css/index.css';
import 'react-dropdown/style.css'

class Product extends Component {
    displayName = Product.name
    constructor(props) {
        super(props);
        this.state = {
            id: props.data.id,
            name: props.data.name,
            bankName: props.data.bank,
            percent: props.data.depositVariations[0].percent,
            minSum: props.data.minSum,
            maxSum: props.data.maxSum,
            sum: props.data.minSum,
            variations: props.data.depositVariations,
            term: props.data.depositVariations[0].term
        };
    }

    onClick = (e) => {
        e.preventDefault();
        let user = JSON.parse(localStorage.getItem('user'));
        if (user && user.token) {
            let variation = this.state.variations.find(variation =>
                variation.term === this.state.term && variation.percent === this.state.percent);
            if (variation) {
                var options = {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json', ...requestHelper.authHeader() },
                    body: JSON.stringify({
                        Id: this.state.id,
                        Sum: this.state.sum,
                        DepositVariations: [variation]
                    })
                };

                fetch('api/Deposits/', options)
                    .then(requestHelper.handleResponse, requestHelper.handleError)
                    .then(response => console.log(response));
            }
        } else {
            this.props.history.push('/login', { redirectUrl: '/products' });
        }
        console.log(this.state.id);
    }

    render() {
        let variations = this.state.variations.map(function (variation) {
            let obj = {
                label: variation.term,
                value: variation.percent
            }
            return obj;
        });

        return (
            <tr>
                <td>{this.state.bankName + ', ' + this.state.name}</td>
                <td>{this.state.percent}</td>
                <td>
                    <InputRange
                        maxValue={this.state.maxSum}
                        minValue={this.state.minSum}
                        value={this.state.sum}
                        step={10000}
                        onChange={value => this.setState({ sum: value })} />
                </td>
                <td>
                    <Dropdown options={variations} onChange={value => this.setState({ percent: value.value, term: value.label })} value={this.state.term} />
                </td>
                <td>
                    <a onClick={this.onClick} className="btn btn-primary">Открыть вклад</a>
                </td>
            </tr>
        )
    }
}

export const ProductRouter = withRouter(Product);
