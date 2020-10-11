import React, { createContext } from 'react';
import axios from 'axios';
import { Grid, makeStyles, Theme, createStyles, FormControl, InputLabel, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button, TableFooter } from "@material-ui/core";
import { myConfig } from '../../configurations';
import { DailyContadoresDataDevices, TonersUsedDataDevices, Device, DeviceDataViewModel, DailyTonersDataDevices } from './device';
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


    const [stateDevice, setDevice] = React.useState<IDeviceList>();
    const [deviceContadoresDailyData, setDeviceContadoresDailyData] = React.useState<DailyContadoresDataDevices>();
    const [deviceContadoresPreviousMonthData, setDeviceContadoresPreviousMonthData] = React.useState<DailyContadoresDataDevices>();
    const [deviceTonersDailyData, setDeviceTonersDailyData] = React.useState<TonersUsedDataDevices>();
    const [deviceTonersCurrentData, setDeviceTonersCurrentData] = React.useState<DailyTonersDataDevices>();
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
      if (deviceContadoresDailyData !== undefined && deviceTonersDailyData !== undefined && deviceTonersCurrentData !== undefined && deviceContadoresPreviousMonthData !== undefined){
        settingDeviceDataViewModel();
      }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [deviceContadoresDailyData, deviceTonersDailyData, deviceTonersCurrentData, deviceContadoresPreviousMonthData]);
  React.useEffect(() => {
    if (inputDate !== undefined && selectedValue !== '') {
      GetDeviceDataFromSelectedRangeDate()
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [inputDate]);



    const GetDevices = async () => {
        await axios.get(myConfig.backUrl + `sensor/GetAllDevices`).then((response) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };

    const GetDeviceData = async () => {
      await axios.get(myConfig.backUrl + `dailyRecord/GetContadoresDataFromActualOrPreviousMonth/` + selectedValue + "/" + true).then((response) => {
          setDeviceContadoresDailyData( response.data );
      });
      await axios.get(myConfig.backUrl + `dailyRecord/GetTonersDataFromActualOrPreviousMonth/` + selectedValue + "/" + true).then((response) => {
          setDeviceTonersDailyData(response.data);
      });
      await axios.get(myConfig.backUrl + `sensor/GetCurrentTonersDevicesValues/` + selectedValue).then((response) => {
        setDeviceTonersCurrentData(response.data);
      });
      await axios.get(myConfig.backUrl + `dailyRecord/GetContadoresDataFromActualOrPreviousMonth/` + selectedValue + "/" + false).then((response) => {
        setDeviceContadoresPreviousMonthData(response.data);
      });
    };

  const GetDeviceDataFromSelectedRangeDate = async () => {
    var start = inputDate.startDate.toJSON();
    var end = inputDate.endDate.toJSON();

    await axios.get(myConfig.backUrl + `dailyRecord/GetContadoresDataFromSelectedRangeDate/` + selectedValue + "/" + start + "/" + end)
    .then((response) => {
      setDeviceContadoresDailyData(response.data);
    });
    await axios.get(myConfig.backUrl + `dailyRecord/GetTonersDataFromSelectedRangeDate/` + selectedValue + "/" + start + "/" + end)
    .then((response) => {
      setDeviceTonersDailyData(response.data);
    });
  }

    const settingDeviceDataViewModel = () => {
      if (deviceContadoresDailyData !== undefined && 
        deviceTonersDailyData !== undefined && 
        deviceTonersCurrentData !== undefined && 
        deviceContadoresPreviousMonthData !== undefined) {
            setDeviceDataViewModel({
              objId: parseInt(selectedValue),
              thisMonthQuantityColorSheets: deviceContadoresDailyData.colorCopies!.toString(),
              thisMonthQuantityBandWSheets: deviceContadoresDailyData.blackAndWhiteCopies!.toString(),
              thisMonthQuantityTotalSheets: (deviceContadoresDailyData.blackAndWhiteCopies! + deviceContadoresDailyData.colorCopies!).toString(),
              thisMonthQuantityBlackToners: deviceTonersDailyData!.blackTonersUsed!.toString(),
              thisMonthQuantityCyanToners: deviceTonersDailyData!.cyanTonersUsed!.toString(),
              thisMonthQuantityMagentaToners: deviceTonersDailyData!.magentaTonersUsed!.toString(),
              thisMonthQuantityYellowToners: deviceTonersDailyData!.yellowTonersUsed!.toString()
            });

            setInfoForTonners({
              blackToner: deviceTonersCurrentData!.blackTonersUsed, 
              cyanToner: deviceTonersCurrentData!.cyanTonersUsed,
              magentaToner: deviceTonersCurrentData!.magentaTonersUsed,
              yellowToner: deviceTonersCurrentData!.yellowTonersUsed,
            });
            setInfoForPreviousMonth({
              objId: parseInt(selectedValue),
              thisMonthQuantityBandWSheets: deviceContadoresPreviousMonthData!.blackAndWhiteCopies!.toString(),
              thisMonthQuantityColorSheets: deviceContadoresPreviousMonthData!.colorCopies!.toString(),
              thisMonthQuantityTotalSheets: (deviceContadoresPreviousMonthData!.blackAndWhiteCopies + deviceContadoresPreviousMonthData!.colorCopies!).toString()
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
                      <TableCell className={classes.dataRow} style={{paddingLeft:'0px'}}>
                        <DatePicker
                          selected={inputDate.startDate}
                          onChange={handleInputStartDate}
                        />
                      </TableCell>
                        <TableCell className={classes.dataRow} style={{ paddingRight: '0px' }}>
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
                        {deviceDataViewModel.thisMonthQuantityBlackToners}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityCyanToners}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityMagentaToners}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        {deviceDataViewModel.thisMonthQuantityYellowToners}
                      </TableCell>
                      <TableCell className={classes.dataRow}>
                        <Button
                          variant="contained"
                          color="primary"
                          size="small"
                          onClick={() => OpenTonersModal()}
                        >
                          Current toners info
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
