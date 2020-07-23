import React from 'react';
import axios from 'axios';
import { Grid, makeStyles, Theme, createStyles, FormControl, InputLabel, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button, TableFooter, TablePagination } from "@material-ui/core";
import { myConfig } from '../../configurations';
import { Device, DeviceData, DeviceDataViewModel } from './device';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import TablePaginationActions from '@material-ui/core/TablePagination/TablePaginationActions';

export interface IDeviceList {
    listOfDevices: Device[]
}

const SensorList = () => {
    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            margins: {
                paddingTop: '60px',
                paddingBottom: '60px',
            },
            formControl: {
                margin: theme.spacing(1),
                minWidth: 120,
            },
            titlesRow: {
                fontWeight: 'bold'
            },
        })
    );
    const classes = useStyles();

    const [stateDevice, setDevice] = React.useState<IDeviceList>();
    const [stateDeviceData, setDeviceData] = React.useState<DeviceData>();
    const [deviceDataViewModel, setDeviceDataViewModel] = React.useState<DeviceDataViewModel>({
        objId: 0,
        thisMonthQuantityColorSheets: "",
        thisMonthQuantityBandWSheets: "",
        thisMonthQuantityTotalSheets: ""
    });
    const [selectedValue, setSelectedValue] = React.useState("0");
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(3);
    const [showTonnerModal, setShowTonnerModal] = React.useState(false);

    React.useEffect(() => {
            console.log("renderGetDevices");
            GetDevices(); 
        }, []);
    React.useEffect(() => { 
            if(selectedValue !== '0'){
                GetDeviceData(); 
            }
        }, [selectedValue]);
    React.useEffect(() => {
        if(stateDeviceData !== undefined){
            let color: number = 0; let blackAndWhite: number = 0;
            if (stateDeviceData?.contadores.channels[1].lastValue !== "No hay datos"
                || stateDeviceData?.contadores.channels[6].lastValue !== "No hay datos") {
                color = (parseInt(stateDeviceData?.contadores.channels[1].lastValue!) +
                    parseInt(stateDeviceData?.contadores.channels[6].lastValue!)
                );
            }
            if (stateDeviceData?.contadores.channels[0].lastValue !== "No hay datos"
                || stateDeviceData?.contadores.channels[5].lastValue !== "No hay datos") {
                blackAndWhite = (parseInt(stateDeviceData?.contadores.channels[0].lastValue!) +
                    parseInt(stateDeviceData?.contadores.channels[5].lastValue!)
                );
            }
            setDeviceDataViewModel({
                objId: stateDeviceData?.objId!,
                thisMonthQuantityColorSheets: color.toString(),
                thisMonthQuantityBandWSheets: blackAndWhite.toString(),
                thisMonthQuantityTotalSheets: (blackAndWhite + color).toString()
            });
        }
    }, [stateDeviceData]);

    const GetDevices = async () => {
        await axios.get(myConfig.backUrl + `sensor/GetAllDevices`).then((response) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };

    const GetDeviceData = async () => {
        await axios.get(myConfig.backUrl + `sensor/GetDeviceData/` + selectedValue).then((response) => {
            if(response.data){
                setDeviceData( response.data );
            }
        });
    };
        
    const HandleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        setSelectedValue(event.target.value as string);
    };

    const OpenTonnersModal = (deviceInfo: Device) => {
        setShowTonnerModal(true);
        // setContractInfoForTonners(contractInfo)
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
                <Grid item xs={5}></Grid>
                <Grid item xs={1}>
                    <FormControl className={classes.formControl}>
                        <InputLabel id="demo-simple-select-label">Device</InputLabel>
                        <Select
                            labelId="demo-simple-select-label"
                            id="demo-simple-select"
                            value={selectedValue}
                            onChange={HandleChange}
                        >
                            {
                                stateDevice !== undefined && stateDevice.listOfDevices !== undefined ? stateDevice.listOfDevices.map(device =>
                                    <MenuItem key={device.objId!.toString()} value={device.objId!.toString()}>{device.device}</MenuItem>
                                ): null
                            }
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item xs={5}></Grid>
            </Grid>
            {
            selectedValue != '0' ?
            <div>
                        <TableContainer component={Paper}>
                            <Table size='medium'>
                                <TableHead aria-label="simple table">
                                    <TableRow>
                                        <TableCell className={classes.titlesRow} size='medium'>color sheets</TableCell>
                                        <TableCell className={classes.titlesRow} size='medium'>B&W sheets</TableCell>
                                        <TableCell className={classes.titlesRow} size='medium'>Total</TableCell>
                                        <TableCell className={classes.titlesRow} size='medium'></TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    <TableRow key={`${deviceDataViewModel.objId}`}>
                                        <TableCell>{deviceDataViewModel.thisMonthQuantityColorSheets}</TableCell>
                                        <TableCell>{deviceDataViewModel.thisMonthQuantityBandWSheets}</TableCell>
                                        <TableCell>{deviceDataViewModel.thisMonthQuantityTotalSheets}</TableCell>
                                        <TableCell>
                                            <Button variant='contained' color='primary' size='small' >Tonners info</Button>
                                        </TableCell>
                                    </TableRow>
                                </TableBody>
                                <TableFooter>
                                    <TableRow>
                                        <TablePagination
                                            rowsPerPageOptions={[3, 6, 9, { label: 'All', value: -1 }]}
                                            colSpan={3}
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
            </div>
            : null
            }
        </div>
    )
}

export default SensorList;
