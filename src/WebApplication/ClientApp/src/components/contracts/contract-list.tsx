import React from 'react';
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
import MailIcon from '@material-ui/icons/Mail';
import DeleteIcon from '@material-ui/icons/Delete';
import DevicesIcon from '@material-ui/icons/Devices';
import EditIcon from '@material-ui/icons/Edit';
import ContractFormModal from './ContractsFormModal';
import ContractDeleteConfirmModal from './contract-delete-confirm-modal';
import { Grid, TablePagination, TableFooter, TextField } from '@material-ui/core';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { Link } from 'react-router-dom';
import useApi from '../../helpers/axios-wrapper'

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
                paddingTop: '30px',
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
    const [searchTerm, setSearchTerm] = React.useState('');
    const [value, setValue] = React.useState<string | null>('');
    const [inputValue, setInputValue] = React.useState('');
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);

    const axios = useApi();

    const GetContracts = async () => {
        await axios.get(`contract`).then( (response : any) => {
            setContract({ ...stateContract, listOfContract: response.data });
        });
    };
    
    const GetContractsConst = async () => {
        await axios.get(`contract`).then((response : any) => {
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

    const ShowDeleteConfirm = async (idToDelete: Number) => {
        setShowDeleteConfirmModal(true);
        setContractIdToDelete(idToDelete!);
    }

    const HideForm = () => {
        setShowFormModal(false);
        setShowDeleteConfirmModal(false);
    }

    const handleChangePage = (event: React.MouseEvent<HTMLButtonElement> | null, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    ) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    React.useEffect(() => { 
        GetContracts(); 
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    React.useEffect(() => { 
        GetContractsConst(); 
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    React.useEffect( () => {
        let results : any = [];
        if( stateContractConst !== undefined && stateContractConst.listOfContract !== undefined) {
            results = stateContractConst!.listOfContract!.filter(contract =>
                contract.owner!.name!.toLowerCase().includes(searchTerm.toLowerCase())
            );
        }
        setContract({ ...stateContract, listOfContract: results });
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [searchTerm]);
    
    return (
        <div className={classes.margins}>
        <Grid container xs={12} item>
            <Grid item xs={1}></Grid>
            <Grid item xs={12}>
                <Autocomplete
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
                    options={(stateContract !== undefined && stateContract.listOfContract !== undefined ? stateContract.listOfContract : []).map((contract) => contract.owner!.name!)}
                    renderInput={(params) => (
                        <TextField {...params} 
                            label="Filter by owner name" 
                            margin="normal" 
                            variant="outlined"
                        />
                    )}
                />
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
                                <TableCell className={classes.titlesRow} size='medium'>B&W normal $/u</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>Color normal $/u</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>B&W surplus $/u</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>Color surplus $/u</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>Devices assigned</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>E-mail reports</TableCell>
                                <TableCell className={classes.titlesRow} size='medium' colSpan={2}>Contract actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {stateContract !== undefined && stateContract.listOfContract !== undefined ?
                                (rowsPerPage > 0
                                    ? stateContract.listOfContract.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                    : stateContract.listOfContract
                                )
                                .map(contract => (
                                <TableRow key={contract.id!}>
                                    <TableCell className={classes.dataRow}>{contract.owner!.name!}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.blackAndWhiteLimitSet}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.colorLimitSet}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.blackAndWhitePrice}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.colorPrice}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.surplusBlackAndWhitePrice}</TableCell>
                                    <TableCell className={classes.dataRow}>{contract.surplusColorPrice}</TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Link to={`/devices?contractId=${contract.id}`}>
                                            <Button variant='contained' color='default'> <DevicesIcon /> </Button>
                                        </Link>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Link to={`/persons?contractId=${contract.id}&ownerId=${contract.ownerId!}`}>
                                            <Button variant='contained' color='default'> <MailIcon /> </Button>
                                        </Link>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Button variant='contained' color='default' onClick={() => ShowContractForm(true, contract)}> <EditIcon /> </Button>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm(contract.id!)}><DeleteIcon /></Button>
                                    </TableCell>
                                </TableRow>
                                )
                                )
                                :
                                null}
                        </TableBody>
                        <TableFooter>
                            <TableRow>
                                <TablePagination
                                    rowsPerPageOptions={[3, 6, 9, { label: 'All', value: -1 }]}
                                    colSpan={10}
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
                                />
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
                    />
                    : null
                }
                <br/>
            </Grid>
            <Grid item xs={1}></Grid>
        </Grid>
        </div>
    )
}

export default ContractList