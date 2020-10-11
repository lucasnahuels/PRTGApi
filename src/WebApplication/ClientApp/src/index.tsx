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
import DevicesList from './components/devices/device-list'
import configureStore from './store/store'
import auth0Config from './config/auth0.config.json';
import * as serviceWorker from './registerServiceWorker';
import AuthenticatedRoute from './components/auth/AuthenticatedRoute';
import { Auth0Provider } from './auth/react-auth0-spa'

const history = createBrowserHistory();
const store = configureStore(history);

ReactDOM.render(
  <Auth0Provider
    domain={auth0Config.domain}
    client_id={auth0Config.clientId}
    audience={auth0Config.audience}
    redirect_uri={window.location.origin}
  >
    <Provider store={store}>
      <ConnectedRouter history={history}>
        <div>
          <Route path="/" component={App} />
          <AuthenticatedRoute path="/contracts" component={ContractList} />
          <AuthenticatedRoute path="/owners" component={OwnersList} />
          <AuthenticatedRoute path="/sensors" component={SensorList} />
          <AuthenticatedRoute path="/prices" component={PricesList} />
          <AuthenticatedRoute path="/persons" component={PersonsList} />
          <AuthenticatedRoute path="/devices" component={DevicesList} />
        </div>        
      </ConnectedRouter>
    </Provider>,       
  </Auth0Provider>,
  document.getElementById("root")
);

serviceWorker.unregister();