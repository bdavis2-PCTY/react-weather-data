import React, { Component } from 'react';
import { Route } from 'react-router';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { BrowserRouter as Router, Link } from 'react-router-dom';

import './custom.css';


export default class App extends Component {
    render() {
        return (
            //<Router>
            //    <Route exact={true} path='/' component={Home} />
            //    <Route path='/counter' component={Counter} />
            //    <Route path='/fetch-data' component={FetchData} />
            //</Router>
            <div>
                <div>
                    <h1>Header!</h1>
                    App!
                    <ul>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/fetch-data">Fetch Data</Link></li>
                        <li><Link to="/counter">Counter</Link></li>
                    </ul>
                </div>
                <div>
                    <h4>Content!</h4>
                    <Route path='/' component={Home} exact={true} />
                    <Route path='/counter' component={Counter} />
                    <Route path='/fetch-data' component={FetchData} />
                </div>
            </div>
        );
    }
}
