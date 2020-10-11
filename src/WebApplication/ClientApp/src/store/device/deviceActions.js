import axios from 'axios'
import { 
    FETCH_DEVICES_REQUEST, 
    FETCH_DEVICES_SUCCESS, 
    FETCH_DEVICES_FAILURE } from "./deviceTypes"

export const fetchDevicesRequest = () => {
    return {
        type: FETCH_DEVICES_REQUEST
    }
}

export const fetchDevicesSuccess = devices => {
    return {
        type: FETCH_DEVICES_SUCCESS,
        payload: devices
    }
}
export const fetchDevicesFailure = error => {
    return {
        type: FETCH_DEVICES_FAILURE,
        payload: error
    }
}

export const fetchDevices = () => {
    return (dispatch) => {
        dispatch(fetchDevicesRequest)
        axios.get(`device`)
        .then(response => {
            const devices = response.data
            dispatch(fetchDevicesSuccess(devices))
        })
        .catch(error => {
            const errorMsg = error.message
            dispatch(fetchDevicesFailure(errorMsg))
        })
    }
}