import React from 'react';
import axios from 'axios';
import { useEffect } from 'react';
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
import { Grid, TablePagination, TableFooter} from '@material-ui/core';
import { myConfig } from '../../configurations';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';
import { Contract } from '../contracts/contract';

const OwnersList = () => {
    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            titlesRow :{
                fontWeight : 'bold',
                textAlign : 'center'
            },
            dataRow:{
                textAlign : 'center'
            },
            margins: {
                paddingTop: '70px',
                paddingBottom: '120px',
            },
            buttonAdd: {
                float: 'right',
                backgroundColor: '#c62d1f',
                borderRadius: '18px',
                color: 'white',
                fontSize: '10px',
                fontWeight: 'bold',
                '&:hover': {
                    backgroundColor: 'red',
                }
            }
        })
    );
    const classes = useStyles();

    const [stateMail, setOwner] = React.useState<Contract>();
    const [showModal, setShowModal] = React.useState(false);
    const [showDeleteConfirmModal, setShowDeleteConfirmModal] = React.useState(false);
    const [formIsEdit, setFormIsEdit] = React.useState(false);
    const [ownerIdToEdit, setOwnerIdToEdit] = React.useState(0);
    const [ownerAdressToEdit, setOwnerAdressToEdit] = React.useState('');
    const [ownerIdToDelete, setOwnerIdToDelete] = React.useState(0);
    const [ownerAdressToDelete, setOwnerAdressToDelete] = React.useState('');
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);
    
    useEffect(() => { GetOwners() }, []);

    const GetOwners = async () => {
        
    };
   
    const ShowOwnersForm = (isEdit?: boolean, ownerToEdit? : string) => {
        setShowModal(true);
        if (!isEdit) {
            setFormIsEdit(false);
        }
        else {
            setFormIsEdit(true);
            // setOwnerIdToEdit();
            // setOwnerAdressToEdit();
        }
    }

    const ShowDeleteConfirm = async (ownerToDelete? : number) => {
        setShowDeleteConfirmModal(true);
        // setOwnerIdToDelete();
        // setOwnerAdressToDelete();
    }

    const HideForm = () => {
        setShowModal(false);
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

    return (
        <div className={classes.margins}>
        <Grid container xs={12} item>
            <Grid item xs={3}></Grid>
            <Grid item xs={6}>
                <Button className={classes.buttonAdd} onClick={() => ShowOwnersForm(false)}>
                    Add new Owner
                </Button>
                <TableContainer component={Paper}>
                    <Table size='medium'>
                        <TableHead aria-label="simple table">
                            <TableRow>
                                <TableCell className={classes.titlesRow} size='medium'>Owner</TableCell>
                                <TableCell className={classes.titlesRow} size='medium' colSpan={2}>Owner actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                           
                                    <TableRow >
                                        <TableCell className={classes.dataRow}></TableCell>
                                        <TableCell className={classes.dataRow}>
                                            <Button variant='contained' color='default' onClick={() => ShowOwnersForm()}> <EditIcon /> </Button>
                                        </TableCell>
                                        <TableCell className={classes.dataRow}>
                                            <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm()}><DeleteIcon /></Button>
                                        </TableCell>

                                    </TableRow>
                          
                        </TableBody>
                        <TableFooter>
                            <TableRow>
                                {/* <TablePagination
                                    rowsPerPageOptions={[3, 6, 9, { label: 'All', value: -1 }]}
                                    colSpan={3}
                                    count={stateOwner !== undefined && stateOwner.listOfOwner !== undefined ? stateOwner.listOfOwner.length : 0}
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
                
                {/* {showModal ?
                    <OwnerFormModal
                        show={showModal}
                        hideModal={HideForm}
                        getAllOwners={GetOwners}
                        isEdit={formIsEdit}
                        // listOfOwners={}
                        ownerId={ownerIdToEdit}
                        adress={ownerAdressToEdit} />
                    : null
                }
                {showDeleteConfirmModal ?
                    <OwnerDeleteConfirmModal 
                        show={showDeleteConfirmModal} 
                        hideModal={HideForm} 
                        getAllOwners={GetOwners} 
                        ownerId={ownerIdToDelete} 
                        ownerAdress={ownerAdressToDelete} />
                    : null
                } */}
            </Grid>
            <Grid item xs={3}></Grid>
        </Grid>
        </div>
    )
}

export default OwnersList
