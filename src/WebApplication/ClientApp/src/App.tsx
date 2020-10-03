import React from 'react';
import './custom.css';
import MainScreen from './mainScreen';
import { axiosRequestInterceptor } from './helpers/axios';
import axios from "axios"

axios.interceptors.request.use(axiosRequestInterceptor, e => Promise.reject(e));

const App = () => {
    return (        
        <div>
            <MainScreen/>
        </div>
    );
};

export default App;