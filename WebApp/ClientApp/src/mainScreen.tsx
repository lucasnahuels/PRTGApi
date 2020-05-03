import React, { ChangeEvent } from 'react';
import ContractList from './components/contracts/contract-list';
import MailList from './components/mails/mail-list';
import SearchAppBar from './toolbar';

const MainScreen = () => {
    return (
        <React.Fragment>
            <SearchAppBar/>
        </React.Fragment>
    );
}
export default MainScreen;