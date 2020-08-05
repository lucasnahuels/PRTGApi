import React from 'react';
import './custom.css';
import MainScreen from './mainScreen';
import { withAuthenticator } from '@aws-amplify/ui-react'
import Amplify from 'aws-amplify';
import awsconfig from './aws-exports';
import { axiosRequestInterceptor } from './helpers/axios';
import axios from "axios"
Amplify.configure(awsconfig);

axios.interceptors.request.use(axiosRequestInterceptor, e => Promise.reject(e));

const App = () => {
    return (        
        <div>
            <MainScreen/>
        </div>
    );
};

export default withAuthenticator(App);