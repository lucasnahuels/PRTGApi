import React from 'react';
import './custom.css';
import MainScreen from './mainScreen';
import { withAuthenticator } from '@aws-amplify/ui-react'
import Amplify from 'aws-amplify';
import awsconfig from './aws-exports';
Amplify.configure(awsconfig);

const App = () => {
    return (
        <React.Fragment>
            <MainScreen/>
        </React.Fragment>
    );
};

export default withAuthenticator(App);