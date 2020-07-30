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
import EmailFormModal from './emailsFormModal';
import EmailDeleteConfirmModal from './email-delete-confirm-modal';
import { Grid, TablePagination, TableFooter} from '@material-ui/core';
import { myConfig } from '../../configurations';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';
import { Contract } from '../contracts/contract';

const EmailsList = () => {
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

    const [stateMail, setMail] = React.useState<Contract>();
    const [showModal, setShowModal] = React.useState(false);
    const [showDeleteConfirmModal, setShowDeleteConfirmModal] = React.useState(false);
    const [formIsEdit, setFormIsEdit] = React.useState(false);
    const [mailIdToEdit, setMailIdToEdit] = React.useState(0);
    const [mailAdressToEdit, setMailAdressToEdit] = React.useState('');
    const [mailIdToDelete, setMailIdToDelete] = React.useState(0);
    const [mailAdressToDelete, setMailAdressToDelete] = React.useState('');
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);
    
    useEffect(() => { GetMails() }, []);

    const GetMails = async () => {
        
    };
   
    const ShowEmailForm = (isEdit?: boolean, mailToEdit? : string) => {
        setShowModal(true);
        if (!isEdit) {
            setFormIsEdit(false);
        }
        else {
            setFormIsEdit(true);
            // setMailIdToEdit();
            // setMailAdressToEdit();
        }
    }

    const ShowDeleteConfirm = async (mailToDelete? : number) => {
        setShowDeleteConfirmModal(true);
        // setMailIdToDelete();
        // setMailAdressToDelete();
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
                <Button className={classes.buttonAdd} onClick={() => ShowEmailForm(false)}>
                    Add new employee
                </Button>
                <TableContainer component={Paper}>
                    <Table size='medium'>
                        <TableHead aria-label="simple table">
                            <TableRow>
                                <TableCell className={classes.titlesRow} size='medium'>E-mail adress</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>Send report?</TableCell>
                                <TableCell className={classes.titlesRow} size='medium' colSpan={2}>Email actions</TableCell>
                            </TableRow>
                        </TableHead>

                        <TableBody>
                            <TableRow>
                                <h6 style={{textAlign:'center', color:'#9400D3', fontWeight:'bold'}}>
                                    It one employee
                                </h6>
                            </TableRow>
                            <TableRow >
                                <TableCell className={classes.dataRow}></TableCell>
                                <TableCell className={classes.dataRow}>
                                    <input type="checkbox"/>
                                </TableCell>
                                <TableCell className={classes.dataRow}>
                                    <Button variant='contained' color='default' onClick={() => ShowEmailForm()}> <EditIcon /> </Button>
                                </TableCell>
                                <TableCell className={classes.dataRow}>
                                    <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm()}><DeleteIcon /></Button>
                                </TableCell>
                            </TableRow>

                            <TableRow>
                                <h6 style={{textAlign:'center', color:'#9400D3', fontWeight:'bold'}}>
                                    Device owner employee
                                </h6>
                            </TableRow>
                            <TableRow >
                                <TableCell className={classes.dataRow}></TableCell>
                                <TableCell className={classes.dataRow}>
                                    <input type="checkbox"/>
                                </TableCell>
                                <TableCell className={classes.dataRow}>
                                    <Button variant='contained' color='default' onClick={() => ShowEmailForm()}> <EditIcon /> </Button>
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
                                    count={stateMail !== undefined && stateMail.listOfMail !== undefined ? stateMail.listOfMail.length : 0}
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
                
                {showModal ?
                    <EmailFormModal
                        show={showModal}
                        hideModal={HideForm}
                        getAllEmails={GetMails}
                        isEdit={formIsEdit}
                        // listOfEmails={}
                        emailId={mailIdToEdit}
                        adress={mailAdressToEdit} />
                    : null
                }
                {showDeleteConfirmModal ?
                    <EmailDeleteConfirmModal 
                        show={showDeleteConfirmModal} 
                        hideModal={HideForm} 
                        getAllEmails={GetMails} 
                        emailId={mailIdToDelete} 
                        emailAdress={mailAdressToDelete} />
                    : null
                }
            </Grid>
            <Grid item xs={3}></Grid>
        </Grid>
        </div>
    )
}

export default EmailsList
