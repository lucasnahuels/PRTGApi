import 'bootstrap/dist/css/bootstrap.css';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import { createBrowserHistory } from 'history';
import configureStore from './store/configureStore';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import MailList from './components/mails/mail-list';
import ContractList from './components/contracts/contract-list';
import { Route, Link, BrowserRouter as Router } from 'react-router-dom'

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const store = configureStore(history);

ReactDOM.render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <div>
            <Route path="/" component={App} />
            <Route path="/contracts" component={ContractList} />
            <Route path="/mails" component={MailList} />
            </div>        
        </ConnectedRouter>
    </Provider>,
    document.getElementById('root'));

registerServiceWorker();
