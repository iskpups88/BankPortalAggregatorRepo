import React, { Component } from 'react';
import { Product } from './Product';

export class ProductList extends Component {
    displayName = ProductList.name

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };

        fetch('api/Products/')
            .then(response => response.json())
            .then(data => {
                this.setState({ products: data, loading: false });
            });
    }

    static renderproductsTable(products) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Название, банк</th>
                        <th>Ставка, %</th>
                        <th>Минимальная сумма, ₽</th>
                        <th>Срок</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <Product key={product.id} data={product} />
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ProductList.renderproductsTable(this.state.products);

        return (
            <div>
                <h1>Вклады</h1>                
                {contents}
            </div>
        );
    }
}
