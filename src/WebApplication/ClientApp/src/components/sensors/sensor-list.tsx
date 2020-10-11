import React, { createContext } from 'react';
import useApi from '../../helpers/axios-wrapper'
import { Grid, makeStyles, Theme, createStyles, FormControl, InputLabel, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button, TableFooter } from "@material-ui/core";
import { Device, DeviceData, DeviceDataViewModel } from './device';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import { Toner } from '../toners/toner';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import TonersModal from '../toners/toners-modal';
import PreviousMonthModal from './previous-month-modal';

export interface IDeviceList {
  listOfDevices: Device[]
}
export const Context = createContext({});

const SensorList = () => {
    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            margins: {
                paddingTop: '20px',
                paddingBottom: '60px',
            },
            formControl: {
                margin: theme.spacing(1),
                minWidth: 120,
                marginLeft:'0%',
            },
            titlesRow: {
                fontWeight: 'bold',
                textAlign : 'center'
            },
            dataRow:{
                textAlign : 'center'
            },
            buttonPreviousMonth: {
                position: 'absolute',
                left: '45%',
                right: '45%',
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

    const axios = useApi();
    const [stateDevice, setDevice] = React.useState<IDeviceList>();
    const [deviceData, setDeviceData] = React.useState<DeviceData>();
    const [deviceDataViewModel, setDeviceDataViewModel] = React.useState<DeviceDataViewModel>({
        objId: 0,
        thisMonthQuantityColorSheets: "",
        thisMonthQuantityBandWSheets: "",
        thisMonthQuantityTotalSheets: ""
    });
    const [selectedValue, setSelectedValue] = React.useState('');
    const [showTonnerModal, setShowTonnerModal] = React.useState(false);
    const [showPreviousMonthModal, setShowPreviousMonthModal] = React.useState(false);
    const [infoForTonners, setInfoForTonners] = React.useState<Toner>({
        blackToner: 0,
        cyanToner : 0,
        magentaToner : 0,
        yellowToner : 0
    });
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const [infoForPreviousMonth, setInfoForPreviousMonth] = React.useState<DeviceDataViewModel>({
      objId: 0,
      thisMonthQuantityColorSheets: "",
      thisMonthQuantityBandWSheets: "",
      thisMonthQuantityTotalSheets: "",
    });
    const [inputDate, setInputDate] = React.useState({
      startDate: new Date((new Date()).getFullYear(), (new Date()).getMonth(), 1),
      endDate: new Date(),
    });

    React.useEffect(() => {
            GetDevices(); 
        // eslint-disable-next-line react-hooks/exhaustive-deps
        }, []);
    React.useEffect(() => { 
            if (selectedValue !== '' && selectedValue !== undefined) {
              GetDeviceData();
            }
        // eslint-disable-next-line react-hooks/exhaustive-deps
        }, [selectedValue]);
    React.useEffect(() => {
      settingDeviceDataViewModel();
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [deviceData]);

    const GetDevices = async () => {
        await axios.get(`sensor/GetAllDevices`).then((response) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };

    const GetDeviceData = async () => {
        await axios.get(`sensor/GetDeviceData/` + selectedValue).then((response) => {
            if(response.data){
                setDeviceData( response.data );
            }
        });
    };

    const settingDeviceDataViewModel = () => {
        if (deviceData !== undefined) {
            let color: number = 0;
            let blackAndWhite: number = 0;
            if (
                deviceData.contadores.channels[1].lastValue !==
                "No hay datos" ||
                deviceData.contadores.channels[6].lastValue !==
                "No hay datos"
            ) {
                color =
                parseInt(deviceData.contadores.channels[1].lastValue!) +
                parseInt(deviceData.contadores.channels[6].lastValue!);
            }
            if (
                deviceData.contadores.channels[0].lastValue !==
                "No hay datos" ||
                deviceData.contadores.channels[5].lastValue !==
                "No hay datos"
            ) {
                blackAndWhite =
                parseInt(deviceData.contadores.channels[0].lastValue!) +
                parseInt(deviceData.contadores.channels[5].lastValue!);
            }
            setDeviceDataViewModel({
                objId: deviceData.objId!,
                thisMonthQuantityColorSheets: color.toString(),
                thisMonthQuantityBandWSheets: blackAndWhite.toString(),
                thisMonthQuantityTotalSheets: (blackAndWhite + color).toString(),
            });
            setInfoForTonners({
              blackToner: parseInt(deviceData.toners.channels[0].lastValue.split(" ")[0]), 
              cyanToner: parseInt(deviceData.toners.channels[1].lastValue.split(" ")[0]),
              magentaToner: parseInt(deviceData.toners.channels[2].lastValue.split(" ")[0]),
              yellowToner: parseInt(deviceData.toners.channels[4].lastValue.split(" ")[0]),
            });
        }
    }
        
    const HandleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        setSelectedValue(event.target.value as string);
    };

    const OpenPreviousMonthModal = () => {
      setShowPreviousMonthModal(true);
    };

    const OpenTonersModal = () => {
        setShowTonnerModal(true);
    }
    
    const HideModal = () => {
        setShowTonnerModal(false);
        setShowPreviousMonthModal(false);
    };

    const handleInputStartDate = (date : Date) => {
      setInputDate({
         ...inputDate, startDate :  date,
      });
    };
    const handleInputEndDate = (date: Date) => {
      setInputDate({
        ...inputDate, endDate: date,
      });
    };

    
    return (
      <div className={classes.margins}>
        <Grid container xs={12} item>
          <Grid item xs={1}></Grid>
          <Grid item xs={10}>
              <FormControl className={classes.formControl}>
                <InputLabel id="demo-simple-select-label">Device</InputLabel>
                <Select
                  labelId="demo-simple-select-label"
                  id="demo-simple-select"
                  value={selectedValue}
                  onChange={HandleChange}
                >
                  {stateDevice !== undefined &&
                  stateDevice.listOfDevices !== undefined
                    ? stateDevice.listOfDevices.map((device) => (
                        <MenuItem
                          key={device.objId!.toString()}
                          value={device.objId!.toString()}
                        >
                          {device.device}
                        </MenuItem>
                      ))
                    : null}
                </Select>
              </FormControl>

          {selectedValue !== '' ? (
            <div>

              <TableContainer component={Paper}>
                <Table size="medium">
                  <TableHead aria-label="simple table">
                  </TableHead>
                  <TableBody>
                    <TableRow key={`${deviceDataViewModel.objId}`}>
                      <TableCell className={classes.dataRow} style={{paddingLeft:'30%'}}>
                        <DatePicker
                          selected={inputDate.startDate}
                          onChange={handleInputStartDate}
                        />
                      </TableCell>
                        <TableCell className={classes.dataRow} style={{ paddingRight: '30%' }}>
                        <DatePicker
                          selected={inputDate.endDate}
                          onChange={handleInputEndDate}
                          />
                      </TableCell>
                    </TableRow>
                  </TableBody>
                  <TableFooter>
                  </TableFooter>
                </Table>
              </TableContainer>

              <br /><br />
              
              <TableContainer component={Paper}>
                <Table size="medium">
                  <TableHead aria-label="simple table">
                    <TableRow>
                      <TableCell className={classes.titlesRow} size="medium">
                        Color sheets
                      </TableCell>
                      <TableCell className={classes.titlesRow} size="medium">
                        B&W sheets
                      </TableCell>
                      <TableCell className={classes.titlesRow} size="medium">
                        Total
                      </TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    <TableRow key={`${deviceDataViewModel.objId}`}>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityColorSheets}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityBandWSheets}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityTotalSheets}
                      </TableCell>
                    </TableRow>
                  </TableBody>
                  <TableFooter>
                    <TableRow>
                    </TableRow>
                  </TableFooter>
                </Table>
              </TableContainer>

              <br /><br/>

              <TableContainer component={Paper}>
                <Table size="medium">
                  <TableHead aria-label="simple table">
                    <TableRow>
                      <TableCell className={classes.titlesRow} size="medium">
                        Unit of black tonners
                      </TableCell>
                      <TableCell className={classes.titlesRow} size="medium">
                        Unit of cyan tonners
                      </TableCell>
                      <TableCell className={classes.titlesRow} size="medium">
                        Unit of magenta tonners
                      </TableCell>
                      <TableCell className={classes.titlesRow} size="medium">
                        Unit of yellow tonners
                      </TableCell>
                      <TableCell
                        className={classes.titlesRow}
                        size="medium"
                      ></TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    <TableRow key={`${deviceDataViewModel.objId}`}>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityColorSheets}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityBandWSheets}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityTotalSheets}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityTotalSheets}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        <Button
                          variant="contained"
                          color="primary"
                          size="small"
                          onClick={() => OpenTonersModal()}
                        >
                          Tonners info
                        </Button>
                      </TableCell>
                    </TableRow>
                  </TableBody>
                  <TableFooter>
                    <TableRow>
                    </TableRow>
                  </TableFooter>
                </Table>
              </TableContainer>

              <br/>

              <Button
                className={classes.buttonPreviousMonth}
                onClick={() => OpenPreviousMonthModal()}
              >
                See previous month
              </Button>
              {showPreviousMonthModal ? (
                <PreviousMonthModal
                  show={showPreviousMonthModal}
                  hideModal={HideModal}
                  info={infoForPreviousMonth}
                />
              ) : null}

              {showTonnerModal ? (
                <TonersModal
                  show={showTonnerModal}
                  hideModal={HideModal}
                  info={infoForTonners}
                />
              ) : null}
            </div>
          ) : null}

          </Grid>
          <Grid item xs={1}></Grid>
        </Grid>
      </div>
    );
}

export default SensorList;
