import React from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button } from '@material-ui/core';
import axios from 'axios';
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { myConfig } from '../../configurations';
import { Contract, ContractDevice } from '../contracts/contract';

export interface DeviceUnassignConfirmModalProps {
    show: boolean,
    hideModal: Function,
    contractId: number | undefined,
    deviceObjId: Number | undefined,
    deviceName: string | undefined
}

function getModalStyle() {
    const top = 28;
    const left = 35;

    return {
        top: `${top}%`,
        left: `${left}%`,
        transform: `translate(-${top}%, -${left}%)`
    };
}

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        paper: {
            position: 'absolute',
            margin: 100,
            width: 400,
            backgroundColor: theme.palette.background.paper,
            border: '1px solid #000',
            boxShadow: theme.shadows[5],
            padding: theme.spacing(2, 4, 3)
        },
    })
);

const DeviceUnassignConfirmModal = ({ show, hideModal, contractId, deviceObjId, deviceName }: DeviceUnassignConfirmModalProps) => {

    const classes = useStyles();
    const [modalStyle] = React.useState(getModalStyle);

    const UnassignDevice = async () => {
        let devicesAssigned: ContractDevice[] = [];
        let deviceAssigned: ContractDevice = {
            objId: deviceObjId!.toString()
        };
        devicesAssigned.push(deviceAssigned);
        let contract: Contract = {
            id: contractId!,
            contractDevices: devicesAssigned
        };
        await axios.put(myConfig.backUrl + 'contract/unassignDevice', contract).then(() => {
            ToastsStore.success('The device was unassigned');
            hideModal();
        }).catch(() => {
            ToastsStore.error('The device was not unassigned');
        });
    }

    const handleClose = () => {
        hideModal();
    };

    return (
        <div>
            <ToastsContainer position={ToastsContainerPosition.TOP_RIGHT} store={ToastsStore} />
            <Modal
                aria-labelledby='contractform-modal-title'
                aria-describedby='contractform-modal-description'
                open={show}
                onClose={handleClose}
            >
                <div style={modalStyle} className={classes.paper}>
                    <div style={{ textAlign: 'center' }}>
                        <h3>Are you sure to unassign the device "{deviceName}" to the contract?</h3>
                        <br /><br />
                        <Button variant='contained' color='primary' onClick={UnassignDevice}>Yes</Button>
                        <Button variant='contained' color='secondary' onClick={handleClose}>No</Button>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default DeviceUnassignConfirmModal
