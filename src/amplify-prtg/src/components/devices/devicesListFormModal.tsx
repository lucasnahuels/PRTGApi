import React, { ChangeEvent, useEffect } from "react";
import { makeStyles, Theme, createStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import { Button, MenuItem } from "@material-ui/core";
import axios from "axios";
import {
    ToastsStore,
    ToastsContainer,
    ToastsContainerPosition,
} from "react-toasts";
import { myConfig } from "../../configurations";
import { Device } from "../sensors/device";

export interface IDeviceList {
    listOfDevices: Device[]
}

export interface DevicesListFormModalProps {
    show: boolean,
    contractId : number,
    hideModal : Function
}

function getModalStyle() {
    const top = 50;
    const left = 50;

    return {
        top: `${top}%`,
        left: `${left}%`,
        transform: `translate(-${top}%, -${left}%)`,
    };
}

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        paper: {
            position: "absolute",
            backgroundColor: theme.palette.background.paper,
            border: "1px solid #000",
            boxShadow: theme.shadows[5],
            padding: theme.spacing(2, 4, 3),
        },
        root: {
            display: "flex",
            flexWrap: "wrap",
        },
        formControl: {
            margin: theme.spacing(1),
            minWidth: 120,
        },
        selectEmpty: {
            marginTop: theme.spacing(2),
        },
    })
);

const DevicesListFormModal = ({show, contractId, hideModal}: DevicesListFormModalProps) => {
    const classes = useStyles();
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);

    const [stateDevice, setDevice] = React.useState<IDeviceList>();

    useEffect(() => {
        GetPossibleDevices()
    }, []);

    const GetPossibleDevices = async () => {
        //the posible device cannot be one already assigned to another contract neither one already assigned to this contract
        await axios.get(myConfig.backUrl + `sensor/GetAllDevices`).then((response) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };

    const handleClose = () => {
        hideModal();
    };

    return (
        <div>
            <ToastsContainer
                position={ToastsContainerPosition.TOP_RIGHT}
                store={ToastsStore}
            />
            <Modal
                aria-labelledby="ownerform-modal-title"
                aria-describedby="ownerform-modal-description"
                open={show}
                onClose={handleClose}
            >
                <div style={modalStyle} className={classes.paper}>
                    <div style={{ textAlign: "center" }}>
                        <h5 id="ownerform-modal-title">Select the device you want to assign to this contract</h5>
                        <br/>
                        <div id="ownerform-modal-description">
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
                            <br />
                            <br />
                            <Button variant="contained" color="default" onClick={handleClose}>
                                Cancel
                            </Button>
                            {/*en el momento del click y manda el elemento como parametro por defecto.. Si fuera handleClose(), el onClick estaria esperando lo que le retorna esa funcion (x ej. una llamada a aotra funcion)*/}
                        </div>
                    </div>
                </div>
            </Modal>
        </div>
    );
};

export default DevicesListFormModal;
