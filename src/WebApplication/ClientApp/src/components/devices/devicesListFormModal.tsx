import React, { useEffect } from "react";
import { makeStyles, Theme, createStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import { Button, MenuItem } from "@material-ui/core";
import {
    ToastsStore,
    ToastsContainer,
    ToastsContainerPosition,
} from "react-toasts";
import { Device } from "../sensors/device";
import { Contract, ContractDevice } from "../contracts/contract";
import useApi from '../../helpers/axios-wrapper'

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
    const [modalStyle] = React.useState(getModalStyle);
    
    const axios = useApi();
    
    const [stateDevice, setDevice] = React.useState<IDeviceList>();

    useEffect(() => {
        GetPossibleDevices()
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);


    const GetPossibleDevices = async () => {
        await axios.get(`sensor/GetUnassignedDevices`).then((response: any) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };

    const HandleMenuItem = async (deviceObjId: string) => {
        let devicesAssigned : ContractDevice[] = [];
        let deviceAssigned : ContractDevice = {
            objId : deviceObjId
        };
        devicesAssigned.push(deviceAssigned);
        let contract : Contract = {
            id : contractId,
            contractDevices : devicesAssigned
        };
        await axios.put('contract/assignDevice', contract).then(() => {
            ToastsStore.success('The device was assigned');
            hideModal();
        }).catch(() => {
            ToastsStore.error('The device was not assigned');
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
                                        onClick={() => HandleMenuItem(device.objId!.toString())}
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
                        </div>
                    </div>
                </div>
            </Modal>
        </div>
    );
};

export default DevicesListFormModal;
