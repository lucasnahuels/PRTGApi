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
import PersonFormModal from './personFormModal';
import PersonDeleteConfirmModal from './person-delete-confirm-modal';
import { Grid, TablePagination, TableFooter, Tooltip} from '@material-ui/core';
import { myConfig } from '../../configurations';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';
import { Person } from '../contracts/contract';

export interface IPersonList {
    listOfPerson: Person[]
}

const PersonsList = () => {
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

    const [statePerson, setPerson] = React.useState<IPersonList>();
    const [showModal, setShowModal] = React.useState(false);
    const [showDeleteConfirmModal, setShowDeleteConfirmModal] = React.useState(false);
    const [formIsEdit, setFormIsEdit] = React.useState(false);
    const [personToEdit, setPersonToEdit] = React.useState<Person>();
    const [personToDelete, setPersonToDelete] = React.useState<Person>();
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);
    
    useEffect(() => { GetPersons() }, []);

    const GetPersons = async () => {
        await axios.get(myConfig.backUrl + `Employee`).then((response) => {
            console.log("employees", response.data);
            setPerson({ ...statePerson, listOfPerson: response.data });
        });
    };
   
    const ShowPersonForm = (isEdit: boolean, personToEdit? : Person) => {
        setShowModal(true);
        if (!isEdit) {
            setFormIsEdit(false);
        }
        else {
            setFormIsEdit(true);
            setPersonToEdit(personToEdit!);
        }
    }

    const ShowDeleteConfirm = async (personToDelete : Person) => {
        setShowDeleteConfirmModal(true);
        setPersonToDelete(personToDelete);
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
                <Button className={classes.buttonAdd} onClick={() => ShowPersonForm(false)}>
                    Add new device owner person
                </Button>
                <TableContainer component={Paper}>
                    <Table size='medium'>
                        <TableHead aria-label="simple table">
                            <TableRow>
                                <TableCell className={classes.titlesRow} size='medium'>E-person adress</TableCell>
                                <TableCell className={classes.titlesRow} size='medium'>Send report?</TableCell>
                                <TableCell className={classes.titlesRow} size='medium' colSpan={2}>Person actions</TableCell>
                            </TableRow>
                        </TableHead>

                        <TableBody>
                            <Tooltip title="This is the list of users from IT-ONE who has registered in the prtg app">
                            <TableRow>
                                <h6 style={{textAlign:'center', color:'#9400D3', fontWeight:'bold'}}>
                                    It one employee
                                </h6>
                            </TableRow>
                            </Tooltip>
                            {statePerson !== undefined && statePerson.listOfPerson !== undefined ? statePerson.listOfPerson.map((person) =>
                            (
                                <TableRow key={person.id}>
                                    <TableCell className={classes.dataRow}>{person.email}</TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <input type="checkbox"/>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Button variant='contained' color='default' onClick={() => ShowPersonForm(true, person)}> <EditIcon /> </Button>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm(person)}><DeleteIcon /></Button>
                                    </TableCell>
                                </TableRow>
                            )
                            )
                            : null
                            }
                            <TableRow>
                                <h6 style={{textAlign:'center', color:'#9400D3', fontWeight:'bold'}}>
                                    Device owner employee
                                </h6>
                            </TableRow>
                            {statePerson !== undefined && statePerson.listOfPerson !== undefined ? statePerson.listOfPerson.map((person) =>
                            (
                                <TableRow key={person.id}>
                                    <TableCell className={classes.dataRow}>{person.email}</TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <input type="checkbox"/>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Button variant='contained' color='default' onClick={() => ShowPersonForm(true, person)}> <EditIcon /> </Button>
                                    </TableCell>
                                    <TableCell className={classes.dataRow}>
                                        <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm(person)}><DeleteIcon /></Button>
                                    </TableCell>
                                 </TableRow>
                            )
                            )
                            : null
                            }
                        </TableBody>

                        <TableFooter>
                            <TableRow>
                                {/* <TablePagination
                                    rowsPerPageOptions={[3, 6, 9, { label: 'All', value: -1 }]}
                                    colSpan={3}
                                    count={statePerson !== undefined && statePerson.listOfPerson !== undefined ? statePerson.listOfPerson.length : 0}
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
                    <PersonFormModal
                        show={showModal}
                        hideModal={HideForm}
                        getAllPersons={GetPersons}
                        isEdit={formIsEdit}
                        person={personToEdit!}
                    />
                    : null
                }
                {showDeleteConfirmModal ?
                    <PersonDeleteConfirmModal 
                        show={showDeleteConfirmModal} 
                        hideModal={HideForm} 
                        getAllPersons={GetPersons} 
                        person={personToDelete!} 
                    />
                    : null
                }
            </Grid>
            <Grid item xs={3}></Grid>
        </Grid>
        </div>
    )
}

export default PersonsList
