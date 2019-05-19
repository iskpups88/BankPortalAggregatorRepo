import React, { Component } from 'react';
import { ProductRouter } from './Product';

export class ProductList extends Component {
    displayName = ProductList.name

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };

        fetch('api/Deposits/')
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
                        <th>Сумма вклада</th>
                        <th width='150px'>Срок</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <ProductRouter key={product.id} data={product} />
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
