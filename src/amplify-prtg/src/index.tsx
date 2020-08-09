import 'bootstrap/dist/css/bootstrap.css'
import * as React from 'react'
import * as ReactDOM from 'react-dom'
import { Provider } from 'react-redux'
import App from './App'
import ContractList from './components/contracts/contract-list'
import { Route } from 'react-router-dom'
import { createBrowserHistory } from 'history';
import { ConnectedRouter } from 'connected-react-router'
import SensorList from './components/sensors/sensor-list'
import PricesList from './components/contracts/prices/prices-list'
import OwnersList from './components/owners/owners-list'
import PersonsList from './components/persons/person-list'
import configureStore from './store/store'
import DevicesList from './components/devices/device-list'

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
        <Route path="/devices" component={DevicesList} />
      </div>        
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);