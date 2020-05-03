import React from 'react';
import axios from 'axios';
import { useEffect } from 'react';
import { Contract } from './contract';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableContainer from '@material-ui/core/TableContainer';
import Paper from '@material-ui/core/Paper';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import TonnersModal from './tonners-modal';
import ContractFormModal from './ContractsFormModal';

export interface IContractList {
    listOfContract: Contract[]
}

export interface ContractListProps{
    show: boolean
}

const ContractList = ({show} : ContractListProps) => {
    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            titlesRow :{
                fontWeight : 'bold'
            },
            margins:{
                marginTop: '60px',
                marginLeft: '60px',
                marginRight: '60px',
                marginBottom: '60px',
            },
            buttonAdd:{
                float: 'right',
                backgroundColor: '#c62d1f',
                borderRadius: '18px',
                color : 'white',
                fontSize : '10px',
                fontWeight :'bold',
                '&:hover': {
                    backgroundColor: 'red',
                }
            },
        })
    );
    const classes = useStyles();

    const [stateContract, setContract] = React.useState<IContractList>();
    const [showModal, setShowModal] = React.useState(false)

    const GetContracts = async () => {
        const response = await axios.get(`https://localhost:44370/api/Contracts`);
        setContract({ ...stateContract, listOfContract: response.data });
    };

    useEffect(() => { GetContracts() }, []);

    const OpenTonnersModal = () =>{
        setShowModal(true);
    }

    const ShowContractForm = (isEdit: boolean, productId?: Number | undefined) => {
        setShowModal(true);
        if (!isEdit) {
            // <ContractFormModal show={showModal} hideModal={HideForm} getAllContracts={GetContracts} isEdit={false}/>
        }
        else{
            // <ContractFormModal show={showModal} hideModal={HideForm} getAllContracts={GetContracts} isEdit={true} productId={productId}/>
        }
    }

    const HideForm = () => {
        setShowModal(false);
    }
    
    return (
        <div className={classes.margins}>
            <Button className={classes.buttonAdd} onClick={() => ShowContractForm(false)}>
                Add new contract
            </Button>
            <TableContainer component={Paper}>
                <Table size='medium'>
                    <TableHead aria-label="simple table">
                        <TableRow>
                            <TableCell className={classes.titlesRow} size='medium'>Company name</TableCell>
                            <TableCell className={classes.titlesRow} size='medium'>Printer</TableCell>
                            <TableCell className={classes.titlesRow} size='medium'>Month</TableCell>
                            <TableCell className={classes.titlesRow} size='medium'>Black and white sheets</TableCell>
                            <TableCell className={classes.titlesRow} size='medium'>Color Sheets</TableCell>
                            <TableCell className={classes.titlesRow} size='medium'></TableCell>
                            <TableCell className={classes.titlesRow} size='medium'></TableCell>
                            <TableCell className={classes.titlesRow} size='medium'></TableCell>
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
                                <TableCell>
                                    <Button variant='contained' color='primary' size='small' onClick={OpenTonnersModal}>Tonners info</Button>
                                </TableCell>
                                <TableCell>
                                    <Button variant='contained' color='default' onClick={() => ShowContractForm(true, contract.contractId)}> <EditIcon /> </Button>
                                </TableCell>
                                <TableCell>
                                    <Button variant='contained' color='secondary'><DeleteIcon /></Button>
                                </TableCell>
                                <TonnersModal show={showModal} hideModal={HideForm} blackToner={contract.blackToner} cyanToner={contract.cyanToner} yellowToner={contract.yellowToner} magentaToner={contract.magentaToner} />
                            </TableRow>
                        )) : null}
                    </TableBody>
                </Table>
            </TableContainer>

        </div>
    )
}

export default ContractList
