import React, { ChangeEvent } from 'react';
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
import ContractDeleteConfirmModal from './contract-delete-confirm-modal';
import { Grid, TablePagination, TableFooter, TextField } from '@material-ui/core';
import { myConfig } from '../../configurations.js';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';
import Autocomplete from '@material-ui/lab/Autocomplete';

export interface IContractList {
    listOfContract: Contract[]
}

const ContractList = () => {
    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            titlesRow :{
                fontWeight : 'bold'
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
    const [showTonnerModal, setShowTonnerModal] = React.useState(false);
    const [showFormModal, setShowFormModal] = React.useState(false);
    const [showDeleteConfirmModal, setShowDeleteConfirmModal] = React.useState(false);
    const [formIsEdit, setFormIsEdit] = React.useState(false);
    const [contractHookToEdit, setContractHookToEdit] = React.useState<Contract>();
    const [contractIdToDelete, setContractIdToDelete] = React.useState<Number>(0);
    const [contractPrinterToDelete, setContractPrinterToDelete] = React.useState('');
    const [contractInfoForTonners, setContractInfoForTonners] = React.useState<Contract>();
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);
    const [searchTerm, setSearchTerm] = React.useState("");
    const [searchResults, setSearchResults] = React.useState([]);


    const GetContracts = async () => {
        await axios.get(myConfig.backUrl + `Contracts`).then( (response) => {
            setContract({ ...stateContract, listOfContract: response.data });
        });
    };

    const GetContractsConst = async () => {
        await axios.get(myConfig.backUrl + `Contracts`).then((response) => {
            setContractConst({ ...stateContractConst, listOfContract: response.data });
        });
    };

    const OpenTonnersModal = (contractInfo: Contract) =>{
        setShowTonnerModal(true);
        setContractInfoForTonners(contractInfo)
    }

    const ShowContractForm = (isEdit: boolean, contractToEdit?: Contract) => {
        setShowFormModal(true);
        if (!isEdit) {
            setFormIsEdit(false);
        }
        else{
            setFormIsEdit(true);
            setContractHookToEdit(contractToEdit!);
        }
    }

    const ShowDeleteConfirm = async (idToDelete: Number, printerNameToDelete: string) => {
        setShowDeleteConfirmModal(true);
        setContractIdToDelete(idToDelete);
        setContractPrinterToDelete(printerNameToDelete);
    }

    const HideForm = () => {
        setShowFormModal(false);
        setShowTonnerModal(false);
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

    const FilterByPrinterName = (event:any) => {
        if (event.key == 'Enter') {
            alert('test');
            setSearchTerm(event.target.textContent);
        }
        setSearchTerm(event.target.textContent);
    };

    React.useEffect(() => { GetContractsConst(); }, []);
    React.useEffect(() => { GetContracts(); }, []);

    React.useEffect( () => {
        let results : any = [];
        if( stateContractConst !== undefined && stateContractConst.listOfContract !== undefined) {
            results = stateContractConst!.listOfContract!.filter(contract =>
                contract.printer.toLowerCase().includes(searchTerm)
            );
        }
        setContract({ ...stateContract, listOfContract: results });
    }, [searchTerm]);
    
    return (
        <div className={classes.margins}>
        <Grid container xs={12} item>
            <Grid item xs={1}></Grid>
            <Grid item xs={10}>
                <Autocomplete
                    className={classes.searchField}
                    onInputChange={FilterByPrinterName}
                    options={(stateContract !== undefined && stateContract.listOfContract !== undefined ? stateContract.listOfContract : []).map((contract) => contract.printer)}
                    renderInput={(params) => (
                        <TextField {...params} 
                            label="Filter by printer name" 
                            margin="normal" 
                            variant="outlined"
                            onChange={event => {
                                const { value } = event.target;
                                setSearchTerm(value);
                            }}
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
                            {stateContract !== undefined && stateContract.listOfContract !== undefined ?
                                (rowsPerPage > 0
                                    ? stateContract.listOfContract.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                    : stateContract.listOfContract
                                )
                                .map(contract => (
                                <TableRow key={`${contract.contractId}`}>
                                    <TableCell>{contract.nameOfCompany}</TableCell>
                                    <TableCell>{contract.printer}</TableCell>
                                    <TableCell>{contract.month}</TableCell>
                                    <TableCell>{contract.blackAndWhiteSheets}</TableCell>
                                    <TableCell>{contract.colorSheets}</TableCell>
                                    <TableCell>
                                        <Button variant='contained' color='primary' size='small' onClick={()=> OpenTonnersModal(contract)}>Tonners info</Button>
                                    </TableCell>
                                    <TableCell>
                                        <Button variant='contained' color='default' onClick={() => ShowContractForm(true, contract)}> <EditIcon /> </Button>
                                    </TableCell>
                                    <TableCell>
                                        <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm(contract.contractId!, contract.printer)}><DeleteIcon /></Button>
                                    </TableCell>
                                </TableRow>
                            )) : null}
                        </TableBody>
                        <TableFooter>
                            <TableRow>
                                <TablePagination
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
                                />
                            </TableRow>
                        </TableFooter>
                    </Table>
                </TableContainer>

                {showTonnerModal ?
                    <TonnersModal 
                        show={showTonnerModal} 
                        hideModal={HideForm} 
                        contractInfo={contractInfoForTonners}/>
                    : null
                }
                {showFormModal ?
                    <ContractFormModal 
                        show={showFormModal} 
                        hideModal={HideForm} 
                        getAllContracts={GetContracts} 
                        isEdit={formIsEdit} 
                        contractToEdit={contractHookToEdit} />
                    : null
                }
                {showDeleteConfirmModal ?
                    <ContractDeleteConfirmModal 
                        show={showDeleteConfirmModal} 
                        hideModal={HideForm} 
                        getAllContracts={GetContracts} 
                        contractId={contractIdToDelete} 
                        printerName={contractPrinterToDelete} />
                    : null
                }
            </Grid>
            <Grid item xs={1}></Grid>
        </Grid>
        </div>
    )
}

export default ContractList
