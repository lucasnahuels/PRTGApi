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
import { Grid, TablePagination, TableFooter, Checkbox } from '@material-ui/core';
import { myConfig } from '../../configurations';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';
import { Device } from '../sensors/device';
import { Link } from 'react-router-dom';
import MailIcon from '@material-ui/icons/Mail';
import DevicesListFormModal from './devicesListFormModal';
import LocalAtmIcon from '@material-ui/icons/LocalAtm';

export interface IDeviceList {
    listOfDevices: Device[]
}

const DevicesList = () => {
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
            buttonBack: {
                left: '45%',
                right: '45%',
                textAlign: 'center',
                backgroundColor: 'blue',
                borderRadius: '18px',
                color: 'white',
                fontSize: '10px',
                fontWeight: 'bold',
                '&:hover': {
                    backgroundColor: 'lightblue',
                }
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
        })
    );
    const classes = useStyles();

    const [stateDevice, setDevice] = React.useState<IDeviceList>();
    const [devicesListForm, setDevicesListForm] = React.useState(false);
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);

    useEffect(() => { GetDevicesByContract() }, []);

    const GetDevicesByContract = async () => {
        await axios.get(myConfig.backUrl + `sensor/GetAllDevices`).then((response) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };

    const ShowDevicesListForm = () => {
        setDevicesListForm(true);
    }

    const HideForm = () => {
        setDevicesListForm(false);
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
                <Button className={classes.buttonAdd} onClick={ShowDevicesListForm}>
                    Assing new device
                </Button>
                    <TableContainer component={Paper}>
                        <Table size='medium'>
                            <TableHead aria-label="simple table">
                                <TableRow>
                                    <TableCell className={classes.titlesRow} size='medium'>Device name</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium'>E-mail reports</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium'>Calculate prices</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {stateDevice !== undefined && stateDevice.listOfDevices !== undefined ?
                                    (rowsPerPage > 0
                                        ? stateDevice.listOfDevices.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                        : stateDevice.listOfDevices
                                    )
                                        .map((device) =>
                                            (
                                                <TableRow key={device.objId!.toString()}>
                                                    <TableCell className={classes.dataRow}>{device.device!}</TableCell>
                                                    <TableCell className={classes.dataRow}>
                                                        <Link to={`/persons/deviceObjId=${device.objId!}`}>
                                                            <Button variant='contained' color='default'> <MailIcon /> </Button>
                                                        </Link>
                                                    </TableCell>
                                                    <TableCell className={classes.dataRow}>
                                                        <Link to={`/prices/deviceObjId=${device.objId!}`} >
                                                            <Button variant='contained' color='default'> <LocalAtmIcon /> </Button>
                                                        </Link>
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
                                        count={stateDevice !== undefined && stateDevice.listOfDevices !== undefined ? stateDevice.listOfDevices.length : 0}
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

                    {devicesListForm ?
                        <DevicesListFormModal
                            show={devicesListForm}
                            contractId={0}
                            hideModal={HideForm}
                        />
                        : null
                    }
                    
                    <br />
                    <Link to="/contracts">
                        <Button className={classes.buttonBack}> Back </Button>
                    </Link>

                </Grid>
                <Grid item xs={3}></Grid>
            </Grid>
        </div>
    )
}

export default DevicesList
