import 'bootstrap/dist/css/bootstrap.css';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'connected-react-router';
import { createBrowserHistory } from 'history';
import configureStore from './store/configureStore';
import App from './App';
import ContractList from './components/contracts/contract-list';
import { Route } from 'react-router-dom'
import SensorList from './components/sensors/sensor-list';
import PricesList from './components/contracts/prices/prices-list';
import OwnersList from './components/owners/owners-list';
import PersonsList from './components/persons/person-list';

const history = createBrowserHistory();
const store = configureStore(history);

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <div>
        <Route path="/" component={App} />
        <Route path="/contracts" component={ContractList} />
        <Route path="/owners" component={OwnersList} />
        <Route path="/sensors" component={SensorList} />
        <Route path="/prices" component={PricesList} />
        <Route path="/persons" component={PersonsList} />
      </div>
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);