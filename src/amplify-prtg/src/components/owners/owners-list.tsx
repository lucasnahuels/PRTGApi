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
import OwnerFormModal from './ownersFormModal';
import OwnerDeleteConfirmModal from './owner-delete-confirm-modal';
import { Owner } from './owner';

export interface IOwnerList {
    listOfOwners: Owner[]
}

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

    const [stateOwner, setOwner] = React.useState<IOwnerList>();
    const [showModal, setShowModal] = React.useState(false);
    const [showDeleteConfirmModal, setShowDeleteConfirmModal] = React.useState(false);
    const [formIsEdit, setFormIsEdit] = React.useState(false);
    const [ownerToEdit, setOwnerToEdit] = React.useState<Owner>();
    const [ownerToDelete, setOwnerToDelete] = React.useState<Owner>();
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);
    
    useEffect(() => { GetOwners() }, []);

    const GetOwners = async () => {
        await axios.get(myConfig.backUrl + `Owner`).then( (response) => {
            console.log("owners", response.data);
            setOwner({ ...stateOwner, listOfOwners: response.data });
        });
    };
   
    const ShowOwnersForm = (isEdit: boolean, ownerToEdit? : Owner) => {
        setShowModal(true);
        if (!isEdit) {
            setFormIsEdit(false);
        }
        else {
            setFormIsEdit(true);
            setOwnerToEdit(ownerToEdit!);
        }
    }

    const ShowDeleteConfirm = async (ownerToDelete : Owner) => {
        setShowDeleteConfirmModal(true);
        setOwnerToDelete(ownerToDelete);
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
                                <TableCell className={classes.titlesRow} size='medium'>Owner name</TableCell>
                                <TableCell className={classes.titlesRow} size='medium' colSpan={2}>Owner actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {stateOwner !== undefined && stateOwner.listOfOwners !== undefined ?
                                (rowsPerPage > 0
                                    ? stateOwner.listOfOwners.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                    : stateOwner.listOfOwners
                                )
                                .map((owner) => 
                                (
                                    <TableRow key={owner.id}>
                                        <TableCell className={classes.dataRow}>{owner.name}</TableCell>
                                        <TableCell className={classes.dataRow}>
                                            <Button variant='contained' color='default' onClick={() => ShowOwnersForm(true, owner)}> <EditIcon /> </Button>
                                        </TableCell>
                                        <TableCell className={classes.dataRow}>
                                            <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm(owner)}><DeleteIcon /></Button>
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
                                    count={stateOwner !== undefined && stateOwner.listOfOwners !== undefined ? stateOwner.listOfOwners.length : 0}
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
                
                {showModal ?
                    <OwnerFormModal
                        show={showModal}
                        hideModal={HideForm}
                        getAllOwners={GetOwners}
                        isEdit={formIsEdit}
                        owner={ownerToEdit!}
                    />
                    : null
                }
                {showDeleteConfirmModal ?
                    <OwnerDeleteConfirmModal 
                        show={showDeleteConfirmModal} 
                        hideModal={HideForm} 
                        getAllOwners={GetOwners} 
                        owner={ownerToDelete!} 
                    />
                    : null
                }
            </Grid>
            <Grid item xs={3}></Grid>
        </Grid>
        </div>
    )
}

export default OwnersList
