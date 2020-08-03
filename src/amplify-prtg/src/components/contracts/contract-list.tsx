import React from 'react';
import axios from 'axios';
import { Contract } from './contract';
import { makeStyles, createStyles } from '@material-ui/core/styles';
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
import ContractFormModal from './ContractsFormModal';
import ContractDeleteConfirmModal from './contract-delete-confirm-modal';
import { Grid, TablePagination, TableFooter, TextField } from '@material-ui/core';
import { myConfig } from '../../configurations.js';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { Link } from 'react-router-dom';

export interface IContractList {
    listOfContract: Contract[]
}

const ContractList = () => {
    const useStyles = makeStyles(() =>
        createStyles({
            titlesRow :{
                fontWeight : 'bold',
                textAlign : 'center'
            },
            dataRow:{
                textAlign : 'center'
            },
            margins:{
                paddingTop: '60px',
                paddingBottom: '60px',
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
            buttonCalculate: {
                position: 'absolute',
                left: '45%',
                right: '45%',
                backgroundColor: 'blue',
                borderRadius: '18px',
                color: 'white',
                fontSize: '10px',
                fontWeight: 'bold',
                '&:hover': {
                    backgroundColor: 'lightblue',
                }
            },
            searchField:{
                float: 'left',
                display: 'inline',
                width:'220px'
            }
        })
    );
    const classes = useStyles();

    const [stateContract, setContract] = React.useState<IContractList>();
    const [stateContractConst, setContractConst] = React.useState<IContractList>();
    const [showFormModal, setShowFormModal] = React.useState(false);
    const [showDeleteConfirmModal, setShowDeleteConfirmModal] = React.useState(false);
    const [formIsEdit, setFormIsEdit] = React.useState(false);
    const [contractToEdit, setContractToEdit] = React.useState<Contract>();
    const [contractIdToDelete, setContractIdToDelete] = React.useState<Number>(0);
    const [contractDeviceNameToDelete, setContractDeviceNameToDelete] = React.useState('');
    const [searchTerm, setSearchTerm] = React.useState("");
    const [value, setValue] = React.useState<string | null>("");
    const [inputValue, setInputValue] = React.useState('');

    const GetContracts = async () => {
        await axios.get(myConfig.backUrl + `contract`).then( (response) => {
            console.log("contracts", response.data);
            setContract({ ...stateContract, listOfContract: response.data });
        });
    };

    const GetContractsConst = async () => {
        await axios.get(myConfig.backUrl + `contract`).then((response) => {
            console.log("contractsConst", response.data);
            setContractConst({ ...stateContractConst, listOfContract: response.data });
        });
    };

    const ShowContractForm = (isEdit: boolean, contractToEdit?: Contract) => {
        setShowFormModal(true);
        if (!isEdit) {
            setFormIsEdit(false);
        }
        else{
            setFormIsEdit(true);
            setContractToEdit(contractToEdit!);
        }
    }

    const ShowDeleteConfirm = async (idToDelete: Number, deviceNameToDelete: string) => {
        setShowDeleteConfirmModal(true);
        setContractIdToDelete(idToDelete!);
        setContractDeviceNameToDelete(deviceNameToDelete!);
    }

    const HideForm = () => {
        setShowFormModal(false);
        setShowDeleteConfirmModal(false);
    }

    // const handleChangePage = (event: React.MouseEvent<HTMLButtonElement> | null, newPage: number) => {
    //     setPage(newPage);
    // };

    // const handleChangeRowsPerPage = (
    //     event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    // ) => {
    //     setRowsPerPage(parseInt(event.target.value, 10));
    //     setPage(0);
    // };

    React.useEffect(() => { 
        console.log("renderGetContractsConst");
        GetContractsConst(); 
    }, []);
    React.useEffect(() => { 
        console.log("renderGetContracts");
        GetContracts(); 
    }, []);

    React.useEffect( () => {
        console.log("renderSearchTerm");
        let results : any = [];
        if( stateContractConst !== undefined && stateContractConst.listOfContract !== undefined) {
            results = stateContractConst!.listOfContract!.filter(contract =>
                contract.device!.device!.toLowerCase().includes(searchTerm)
            );
        }
        setContract({ ...stateContract, listOfContract: results });
    }, [searchTerm]);
    
    return (
        <div className={classes.margins}>
        <Grid container xs={12} item>
            <Grid item xs={1}></Grid>
            <Grid item xs={10}>
                {/* <Autocomplete
                    className={classes.searchField}
                    value={value}
                    onChange={(event: React.ChangeEvent<{}>, newValue: string|null) => {            
                        setValue(newValue);
                        event.preventDefault();
                    }}
                    inputValue={inputValue}
                    onInputChange={(event, newInputValue) => {
                        setSearchTerm(newInputValue);
                        setInputValue(newInputValue);
                        event.preventDefault();
                    }}
                    options={(stateContract !== undefined && stateContract.listOfContract !== undefined ? stateContract.listOfContract : []).map((contract) => contract.device!.device!)}
                    renderInput={(params) => (
                        <TextField {...params} 
                            label="Filter by device name" 
                            margin="normal" 
                            variant="outlined"
                        />
                    )}
                /> */}
                <Button className={classes.buttonAdd} onClick={() => ShowContractForm(false)}>
                    Add new contract
                </Button>
                <TableContainer component={Paper}>
                    <Table size='medium'>
                        <TableHead aria-label="simple table">
                            <TableRow>
                                <TableCell className={classes.titlesRow} size='medium'>Owner</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>B&W limit set</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>Color limit set</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>B&W normal price per unit</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>Color normal price per unit</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>B&W surplus price per unit</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>Color surplus price per unit</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>E-mail reports</TableCell>
                                <TableCell className={classes.titlesRow} size='medium' colSpan={2}>Contract actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {stateContract !== undefined && stateContract.listOfContract !== undefined ? stateContract.listOfContract.map((contract) =>
                                (
                                <TableRow> {contract.id!}
                                    {/* <TableCell className={classes.dataRow}>{contract.owner!.name!}</TableCell> */}
                                    <TableCell className={classes.dataRow}>{contract.blackAndWhiteLimitSet}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.colorLimitSet}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.blackAndWhitePrice}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.colorPrice}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.surplusBlackAndWhitePrice}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.surplusColorPrice}</TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Link to={`/persons/deviceObjId=${contract.deviceId}`}>
                                            <Button variant='contained' color='default'> <EditIcon /> </Button>
                                        </Link>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Button variant='contained' color='default' onClick={() => ShowContractForm(true, contract)}> <EditIcon /> </Button>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm(contract.id!, contract.device!.device!)}><DeleteIcon /></Button>
                                    </TableCell>
                                </TableRow>
                                )
                                )
                                :
                                null}
                        </TableBody>
                        <TableFooter>
                            <TableRow>
                                {/* <TablePagination
                                    rowsPerPageOptions={[3, 6, 9, { label: 'All', value: -1 }]}
                                    colSpan={3}
                                    count={stateContract !== undefined && stateContract.listOfContract !== undefined ? stateContract.listOfContract.length : 0}
                                    rowsPerPage={rowsPerPage}
                                    page={page}
                                    SelectProps={{
                                        inputProps: { 'aria-label': 'rows per page' },
                                        native: true,
                                    }}
                                    onChangePage={handleChangePage}
                                    onChangeRowsPerPage={handleChangeRowsPerPage}
                                    ActionsComponent={TablePaginationActions}
                                /> */}
                            </TableRow>
                        </TableFooter>
                    </Table>
                </TableContainer>

                {showFormModal ?
                    <ContractFormModal 
                        show={showFormModal} 
                        hideModal={HideForm} 
                        getAllContracts={GetContracts} 
                        isEdit={formIsEdit} 
                        contractToEdit={contractToEdit} />
                    : null
                }
                {showDeleteConfirmModal ?
                    <ContractDeleteConfirmModal 
                        show={showDeleteConfirmModal} 
                        hideModal={HideForm} 
                        getAllContracts={GetContracts} 
                        contractId={contractIdToDelete} 
                        deviceName={contractDeviceNameToDelete} />
                    : null
                }
                <br/>
                <Link to="/prices/deviceObjId=" >
                    <Button className={classes.buttonCalculate}>Calculate prices from selected device</Button>
                </Link>
            </Grid>
            <Grid item xs={1}></Grid>
        </Grid>
        </div>
    )
}

export default ContractList
