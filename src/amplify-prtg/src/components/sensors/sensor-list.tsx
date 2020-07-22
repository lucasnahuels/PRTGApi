import React from 'react';
import axios from 'axios';
import { Grid, makeStyles, Theme, createStyles, FormControl, InputLabel } from "@material-ui/core";
import { myConfig } from '../../configurations';
import { Device } from './device';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';

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
        })
    );
    const classes = useStyles();

    const [stateDevice, setDevice] = React.useState<IDeviceList>();
    const [selectedValue, setSelectedValue] = React.useState('');
    React.useEffect(() => { GetDevices(); }, []);

    const GetDevices = async () => {
        await axios.get(myConfig.backUrl + `sensor/GetAllDevices`).then((response) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };

    const HandleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        setSelectedValue(event.target.value as string);
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
        </div>
    )
}

export default SensorList;
