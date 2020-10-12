import React from 'react';
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
import { Grid, TablePagination, TableFooter } from '@material-ui/core';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';
import { Device } from '../sensors/device';
import { Link } from 'react-router-dom';
import DevicesListFormModal from './devicesListFormModal';
import LocalAtmIcon from '@material-ui/icons/LocalAtm';
import HighlightOffIcon from '@material-ui/icons/HighlightOff';
import DeviceUnassignConfirmModal from './device-unassign-confirm-modal';
import useApi from '../../helpers/axios-wrapper'

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
                paddingTop: '30px',
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
    const [contractId, setContractId] = React.useState("");
    const [showUnassignConfirmModal, setShowUnassignConfirmModal] = React.useState(false);
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);

    useEffect(() => { 
        setContractId(getQueryVariable("contractId")); 
    }, [contractId]);

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
        GetDevicesByContract()
     // eslint-disable-next-line react-hooks/exhaustive-deps
     }, []);

     
    const GetDevicesByContract = async () => {
        let id : string = getQueryVariable("contractId");
        await axios.get(`sensor/GetAssignedDevices/` + id).then((response) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };

    const ShowDevicesListForm = () => {
        setDevicesListForm(true);
    }

    const HideForm = () => {
        setDevicesListForm(false);
        setShowUnassignConfirmModal(false);
        GetDevicesByContract()
    }

    const ShowUnassignConfirm = async () => {
        setShowUnassignConfirmModal(true);
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
                                    <TableCell className={classes.titlesRow} size='medium'>Device assigned</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium'>Calculate prices</TableCell>
                                    <TableCell className={classes.titlesRow} size='medium'>Unassign device</TableCell>
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
                                                        <Link to={`/prices?deviceObjId=${device.objId!}&contractId=${contractId!}`} >
                                                            <Button variant='contained' color='default'> <LocalAtmIcon /> </Button>
                                                        </Link>
                                                    </TableCell>
                                                    <TableCell className={classes.dataRow}>
                                                        <Button variant='contained' color='primary' onClick={ShowUnassignConfirm}><HighlightOffIcon /></Button>
                                                    </TableCell>

                                                    {showUnassignConfirmModal ?
                                                        <DeviceUnassignConfirmModal
                                                            show={showUnassignConfirmModal}
                                                            hideModal={HideForm}
                                                            contractId={parseInt(contractId!)}
                                                            deviceObjId={device.objId!}
                                                            deviceName={device.device!}
                                                        />
                                                    : null
                                                    }
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
                            contractId={parseInt(contractId!)}
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
