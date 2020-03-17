import React from 'react';
import axios from 'axios';
import { useEffect } from 'react';
import { Contract } from './contract';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import { Button } from '@material-ui/core';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';

export interface IContractList {
    listOfContract: Contract[]
}

const ContractList = () => {
    // const useStyles = makeStyles((theme: Theme) =>
    //     createStyles({
    //     })
    // );

    const [stateContract, setContract] = React.useState<IContractList>();

    const GetContracts = async () => {
        const response = await axios.get(`https://localhost:5001/api/Contracts`);
        setContract({ ...stateContract, listOfContract: response.data });
    };

    useEffect(() => { GetContracts() }, []);

    return (
        <div>
            <Table size='medium'>
                <TableHead>
                    <TableRow>
                        <TableCell size='medium'>Company name</TableCell>
                        <TableCell size='medium'>Printer</TableCell>
                        <TableCell size='medium'>Month</TableCell>
                        <TableCell size='medium'>Black and white sheets</TableCell>
                        <TableCell size='medium'>Color Sheets</TableCell>
                        <TableCell size='medium'>Black Toner</TableCell>
                        <TableCell size='medium'>Cyan tonner</TableCell>
                        <TableCell size='medium'>Yellow tonner</TableCell>
                        <TableCell size='medium'>Magenta tonner</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {stateContract !== undefined && stateContract.listOfContract !== undefined ? stateContract.listOfContract.map(contract => (
                        <TableRow key={`${contract.contractId}`}>
                            <TableCell>{contract.nameOfCompany}</TableCell>
                            <TableCell>{contract.printer}</TableCell>
                            <TableCell>{contract.month}</TableCell>
                            <TableCell>{contract.blackAndWhiteSheets}</TableCell>
                            <TableCell>{contract.colorSheets}</TableCell>
                            <TableCell>{contract.blackToner}</TableCell>
                            <TableCell>{contract.cyanToner}</TableCell>
                            <TableCell>{contract.yellowToner}</TableCell>
                            <TableCell>{contract.magentaToner}</TableCell>
                        </TableRow>
                    )) : null}
                </TableBody>
            </Table>
        </div>
    )
}

export default ContractList
