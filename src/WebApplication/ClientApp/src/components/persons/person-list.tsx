import React from 'react';
import useApi from '../../helpers/axios-wrapper'
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
import { Grid, TableFooter, Tooltip, Checkbox } from '@material-ui/core';
import { Employee, Auth0User, Contract, ContractEmployee, ContractUser } from '../contracts/contract';
import { Link } from 'react-router-dom';
import { ToastsStore } from 'react-toasts';
import ArrowDropDownCircleIcon from '@material-ui/icons/ArrowDropDownCircle';

export interface IEmployeeList {
    listOfEmployee: Employee[]
}
export interface IUserList {
    listOfUser: Auth0User[]
}

const PersonsList = () => {
    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            titlesRow: {
                fontWeight: 'bold',
                textAlign: 'center'
            },
            dataRow: {
                textAlign: 'center'
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
            },
            button: {
                left: '40%',
                right: '40%',
                backgroundColor: 'blue',
                borderRadius: '18px',
                color: 'white',
                fontSize: '10px',
                fontWeight: 'bold',
                '&:hover': {
                    backgroundColor: 'lightblue',
                }
            },
            greyButton: {
                left: '40%',
                right: '40%',
                backgroundColor: 'grey',
                borderRadius: '18px',
                color: 'white',
                fontSize: '10px',
                fontWeight: 'bold',
                '&:hover': {
                    backgroundColor: 'black',
                }
            },
        })
    );
    const classes = useStyles();

    const [stateEmployee, setEmployee] = React.useState<IEmployeeList>();
    const [stateUser, setUser] = React.useState<IUserList>();
    const [showModal, setShowModal] = React.useState(false);
    const [showDeleteConfirmModal, setShowDeleteConfirmModal] = React.useState(false);
    const [formIsEdit, setFormIsEdit] = React.useState(false);
    const [personToEdit, setPersonToEdit] = React.useState<Employee>();
    const [personToDelete, setPersonToDelete] = React.useState<Employee>();
    const [showItOne, setShowItOne] = React.useState(false);
    const [showDeviceOwner, setShowDeviceOwner] = React.useState(false);
    const [contractId, setContractId] = React.useState("");

    function getQueryVariable(variable: string) {
        var query = window.location.search.substring(1);//"app=article&act=news_content&aid=160990"
        var vars = query.split("&");//[ 'app=article', 'act=news_content', 'aid=160990' ]
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");//[ 'app', 'article' ][ 'act', 'news_content' ][ 'aid', '160990' ] 
            if (pair[0] === variable) { return pair[1]; }
        }
        return "";
    }

    const axios = useApi();

    useEffect(() => {
        setContractId(getQueryVariable("contractId"));
    }, [contractId]);

    useEffect(() => {
        GetEmployees();
        GetUsers();
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const GetEmployees = async () => {
        let id: string = getQueryVariable("contractId"); 
        await axios.get<Employee[]>(`Employee`).then(async (response: any) => {
            setEmployee({ ...stateEmployee, listOfEmployee: response.data });
            await axios.get<ContractEmployee[]>(`contract/getContractEmployeesRelations/` + id).then((innerResponse: any) => {
                response.data.forEach((employee: { sendReport: boolean; id: any; }) => {
                    employee.sendReport = false;
                    innerResponse.data.forEach((contractEmployee: { employeeId: any; }) => {
                        if(employee.id! === contractEmployee.employeeId!){
                            employee.sendReport! = true
                        }
                    });
                });
            });
        });
    };

    const GetUsers = async () => {
        let id: string = getQueryVariable("contractId"); 
        await axios.get<Auth0User[]>(`User`).then(async (response: any) => {
            setUser({ ...stateUser, listOfUser: response.data });
            await axios.get<ContractUser[]>(`contract/getContractUsersRelations/` + id).then((innerResponse: any) => {
                response.data.forEach((user: { sendReport: boolean; user_Id: any; }) => {
                    user.sendReport = false;
                    innerResponse.data.forEach((contractUser: { user_Id: any; }) => {
                        if (user.user_Id! === contractUser.user_Id!) {
                            user.sendReport! = true
                        }
                    });
                });
            });
        });
    };

    const ShowPersonForm = (isEdit: boolean, personToEdit?: Employee) => {
        setShowModal(true);
        if (!isEdit) {
            setFormIsEdit(false);
        }
        else {
            setFormIsEdit(true);
            setPersonToEdit(personToEdit!);
        }
    }

    const ShowDeleteConfirm = async (personToDelete: Employee) => {
        setShowDeleteConfirmModal(true);
        setPersonToDelete(personToDelete);
    }

    const HideForm = () => {
        setShowModal(false);
        setShowDeleteConfirmModal(false);
    }

    const showItOneUsers = () => {
        setShowItOne(!showItOne)
    }
    const showDeviceOwnerEmployees = () => {
        setShowDeviceOwner(!showDeviceOwner)
    }

    const handleChangeCheckboxForDevicesOwners = (event: React.ChangeEvent<HTMLInputElement>) => {
        let employeeList: Employee[] = stateEmployee!.listOfEmployee!;
        employeeList.forEach(employee => {
            if (employee.id!.toString() === event.target.name) {
                employee.sendReport = event.target.checked;
            }
        });
        setEmployee({ ...stateEmployee, listOfEmployee: employeeList });
    };
    const handleChangeCheckboxForUsers = (event: React.ChangeEvent<HTMLInputElement>) => {
        let userList: Auth0User[] = stateUser!.listOfUser!;
        userList.forEach(user => {
            if (user.user_Id! === event.target.name) {
                user.sendReport = event.target.checked;
            }
        });
        setUser({ ...stateUser, listOfUser: userList });
    };

    const SaveChanges = async () => {
        //employees
        let employeesAssigned: ContractEmployee[] = [];
        stateEmployee!.listOfEmployee!.forEach(employee => {
            if (employee.sendReport){
                let employeeAssigned: ContractEmployee = {
                    employeeId: employee.id!
                };
                employeesAssigned.push(employeeAssigned);
            }
        });;

        // //users
        let usersAssigned: ContractUser[] = [];
        stateUser!.listOfUser!.forEach(user => {
            if (user.sendReport) {
                let userAssigned: ContractUser = {
                    user_Id : user.user_Id!
                };
                usersAssigned.push(userAssigned);
            }
        });;

        //contract
        let contract: Contract = {
            id: parseInt(contractId!),
            contractEmployees: employeesAssigned,
            contractUsers: usersAssigned
        };

        //put
        await axios.put('contract/updateEmployeesAndUsers', contract).then(() => {
            ToastsStore.success('The update was successfully');
            window.location.href =  "contracts";
        }).catch(() => {
            ToastsStore.error('The update was not successfully');
        });
    }

    return (
        <div className={classes.margins}>
            <Grid container xs={12} item>
                <Grid item xs={2}></Grid>
                <Grid item xs={8}>
                    <Button className={classes.buttonAdd} onClick={() => ShowPersonForm(false)}>
                        Add new device owner employee
                </Button>
                    <TableContainer component={Paper}>
                        <Table size='medium'>
                            <TableHead aria-label="simple table">
                                <TableRow>
                                    <TableCell className={classes.titlesRow} size='medium'>Name</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium'>Owner name</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium'>E-person adress</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium'>Send report?</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium' colSpan={2}>Person actions</TableCell>
                                </TableRow>
                            </TableHead>

                            <TableBody>
                                <TableRow style={{ backgroundColor: '#f0f0f0' }}>
                                    <TableCell colSpan={5}>
                                        <Tooltip title="This is the list of users from It-One who have already registered in the prtg app">
                                            <Button onClick={showItOneUsers} className={classes.greyButton}>
                                                It-One user <ArrowDropDownCircleIcon /> 
                                            </Button>
                                        </Tooltip>
                                    </TableCell>
                                </TableRow>
                                {stateUser !== undefined && stateUser.listOfUser !== undefined ? stateUser.listOfUser.map((user) =>
                                    (
                                        showItOne ? (
                                            <TableRow key={user.user_Id!}>
                                                <TableCell className={classes.dataRow}>{user.nickName}</TableCell>
                                                <TableCell className={classes.dataRow}></TableCell>
                                                <TableCell className={classes.dataRow}>{user.email}</TableCell>
                                                <TableCell className={classes.dataRow}>
                                                    <Checkbox checked={user.sendReport!} onChange={handleChangeCheckboxForUsers} name={user.user_Id!} />
                                                </TableCell>
                                                <TableCell></TableCell>
                                                <TableCell></TableCell>
                                            </TableRow>
                                        )
                                            :
                                            null
                                    )
                                )
                                    : null
                                }

                                <TableRow>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                </TableRow>

                                <TableRow style={{ backgroundColor: '#f0f0f0' }}>
                                    <TableCell colSpan={5}>
                                        <Button onClick={showDeviceOwnerEmployees} className={classes.greyButton}>
                                            Device owner employees <ArrowDropDownCircleIcon />
                                        </Button>
                                    </TableCell>
                                </TableRow>
                                {stateEmployee !== undefined && stateEmployee.listOfEmployee !== undefined ? stateEmployee.listOfEmployee.map((employee) =>
                                    (
                                        showDeviceOwner ? (
                                            <TableRow key={employee.id!.toString()}>
                                                <TableCell className={classes.dataRow}>{employee.name}</TableCell>
                                                <TableCell className={classes.dataRow}>{employee.owner!.name!}</TableCell>
                                                <TableCell className={classes.dataRow}>{employee.email}</TableCell>
                                                <TableCell className={classes.dataRow}>
                                                    <Checkbox checked={employee.sendReport!} onChange={handleChangeCheckboxForDevicesOwners} name={employee.id!.toString()} />
                                                </TableCell>
                                                <TableCell className={classes.dataRow}>
                                                    <Button variant='contained' color='default' onClick={() => ShowPersonForm(true, employee)}> <EditIcon /> </Button>
                                                </TableCell>
                                                <TableCell className={classes.dataRow}>
                                                    <Button variant='contained' color='secondary' onClick={() => ShowDeleteConfirm(employee)}><DeleteIcon /></Button>
                                                </TableCell>
                                            </TableRow>
                                        )
                                            :
                                            null
                                    )
                                )
                                    : null
                                }

                                <TableRow>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                    <TableCell></TableCell>
                                </TableRow>

                            </TableBody>
                            <TableFooter>
                                <TableRow>
                                </TableRow>
                            </TableFooter>
                        </Table>
                    </TableContainer>

                    {showModal ?
                        <PersonFormModal
                            show={showModal}
                            hideModal={HideForm}
                            getAllPersons={GetEmployees}
                            isEdit={formIsEdit}
                            personToEdit={personToEdit!}
                        />
                        : null
                    }
                    {showDeleteConfirmModal ?
                        <PersonDeleteConfirmModal
                            show={showDeleteConfirmModal}
                            hideModal={HideForm}
                            getAllPersons={GetEmployees}
                            person={personToDelete!}
                        />
                        : null
                    }

                    <br />
                    <Link to="/contracts">
                        <Button className={classes.button}> Back </Button>
                    </Link>
                    <Button className={classes.button} onClick={SaveChanges}>Save changes in reports</Button>

                </Grid>
                <Grid item xs={2}></Grid>
            </Grid>
        </div>
    )
}

export default PersonsList
