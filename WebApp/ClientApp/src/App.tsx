
import React, { Component } from 'react';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import './custom.css';
import ContractList from './contracts/contract-list';

class App extends Component {
    render() {
        return(
            <React.Fragment>
                <ContractList />
            </React.Fragment>
        );
    }
};
export default App;

