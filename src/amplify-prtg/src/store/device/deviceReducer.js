import { FETCH_DEVICES_FAILURE, FETCH_DEVICES_REQUEST, FETCH_DEVICES_SUCCESS } from "./deviceTypes";

const initialState = {
    loading: true,
    devices: [],
    error: ''
}

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case FETCH_DEVICES_REQUEST:
            return {
                ...state,
                loading: true
            };
        case FETCH_DEVICES_SUCCESS:
            return {
                loading: false,
                devices: action.devices,
                error: ''
            };
        case FETCH_DEVICES_FAILURE:
            return {
                loading: false,
                devices: [],
                error: action.payload
            };
        default: return state
    }
};

export default reducer