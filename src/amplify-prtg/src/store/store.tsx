import { createStore, applyMiddleware, combineReducers } from 'redux'
import thunk from 'redux-thunk'
import { composeWithDevTools } from 'redux-devtools-extension'
import logger from 'redux-logger'
import rootReducer from './rootReducer'
import { connectRouter, routerMiddleware } from 'connected-react-router'
import { History } from 'history'

export default function configureStore(history: History<any>) {
    const middleware = [
        thunk,
        routerMiddleware(history)
    ];

    return createStore(
        combineReducers({
            ...rootReducer,
            router: connectRouter(history)
        }),
        composeWithDevTools(applyMiddleware(...middleware, logger, thunk))
    );    
}