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
import { Grid, TableFooter, Tooltip, Checkbox } from '@material-ui/core';
import { myConfig } from '../../configurations';
import { Employee, CognitoUser, Contract, ContractEmployee, ContractUser } from '../contracts/contract';
import Dropdown from 'reactstrap/lib/Dropdown';
import DropdownToggle from 'reactstrap/lib/DropdownToggle';
import DropdownMenu from 'reactstrap/lib/DropdownMenu';
import { Link } from 'react-router-dom';
import { ToastsStore } from 'react-toasts';

export interface IEmployeeList {
    listOfEmployee: Employee[]
}
export interface IUserList {
    listOfUser: CognitoUser[]
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
        var query = window.location.search.substring(1);
        console.log(query)//"app=article&act=news_content&aid=160990"
        var vars = query.split("&");
        console.log(vars) //[ 'app=article', 'act=news_content', 'aid=160990' ]
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            console.log(pair)//[ 'app', 'article' ][ 'act', 'news_content' ][ 'aid', '160990' ] 
            if (pair[0] === variable) { return pair[1]; }
        }
        return "";
    }

    useEffect(() => {
        setContractId(getQueryVariable("contractId"));
    }, [contractId]);

    useEffect(() => {
        GetEmployees();
        GetUsers();
    });

    const GetEmployees = async () => {
        let id: string = getQueryVariable("contractId"); 
        await axios.get<Employee[]>(myConfig.backUrl + `Employee`).then(async (response) => {
            console.log("employees", response.data);
            setEmployee({ ...stateEmployee, listOfEmployee: response.data });
            await axios.get<ContractEmployee[]>(myConfig.backUrl + `contract/getContractEmployeesRelations/` + id).then((innerResponse) => {
                response.data.forEach(employee => {
                    employee.sendReport = false;
                    innerResponse.data.forEach(contractEmployee => {
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
        await axios.get<CognitoUser[]>(myConfig.backUrl + `User`).then(async (response) => {
            console.log("user", response.data);
            setUser({ ...stateUser, listOfUser: response.data });
            await axios.get<ContractUser[]>(myConfig.backUrl + `contract/getContractUsersRelations/` + id).then((innerResponse) => {
                response.data.forEach(user => {
                    user.sendReport = false;
                    innerResponse.data.forEach(contractUser => {
                        if (user.userID! === contractUser.userId!) {
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
        let userList: CognitoUser[] = stateUser!.listOfUser!;
        userList.forEach(user => {
            if (user.userID! === event.target.name) {
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
                    userId : user.userID!
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
        await axios.put(myConfig.backUrl + 'contract/updateEmployeesAndUsers', contract).then(() => {
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
                                    <TableCell className={classes.titlesRow} size='medium'>E-person adress</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium'>Send report?</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium' colSpan={2}>Person actions</TableCell>
                                </TableRow>
                            </TableHead>

                            <TableBody>
                                <TableRow>
                                    <Tooltip title="This is the list of users from It-One who have already registered in the prtg app">
                                        <Dropdown onClick={showItOneUsers}>
                                            <DropdownToggle caret>
                                                It-One user
                                    </DropdownToggle>
                                            <DropdownMenu right>
                                            </DropdownMenu>
                                        </Dropdown>
                                    </Tooltip>
                                </TableRow>
                                {stateUser !== undefined && stateUser.listOfUser !== undefined ? stateUser.listOfUser.map((user) =>
                                    (
                                        showItOne ? (
                                            <TableRow key={user.userID!}>
                                                <TableCell className={classes.dataRow}>{user.userName}</TableCell>
                                                <TableCell className={classes.dataRow}>{user.attributes.email}</TableCell>
                                                <TableCell className={classes.dataRow}>
                                                    <Checkbox checked={user.sendReport!} onChange={handleChangeCheckboxForUsers} name={user.userID!} />
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

                                <br />

                                <TableRow>
                                    <Dropdown onClick={showDeviceOwnerEmployees}>
                                        <DropdownToggle caret>
                                            Device owner employees
                                    </DropdownToggle>
                                        <DropdownMenu right>
                                        </DropdownMenu>
                                    </Dropdown>
                                </TableRow>
                                {stateEmployee !== undefined && stateEmployee.listOfEmployee !== undefined ? stateEmployee.listOfEmployee.map((employee) =>
                                    (
                                        showDeviceOwner ? (
                                            <TableRow key={employee.id!.toString()}>
                                                <TableCell className={classes.dataRow}>{employee.name}</TableCell>
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
                            person={personToEdit!}
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
                    <Link to="/devices">
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
