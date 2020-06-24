import React, { useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button } from '@material-ui/core';
import axios from 'axios';
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { myConfig } from '../../configurations';

export interface ContractDeleteConfirmModalProps {
    show: boolean,
    hideModal: Function,
    getAllContracts: Function,
    contractId: Number | undefined,
    printerName: string | undefined
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

const ContractDeleteConfirmModal = ({ show, hideModal, getAllContracts, contractId, printerName }: ContractDeleteConfirmModalProps) => {

    const classes = useStyles();
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);

    // tslint:disable-next-line: no-floating-promises
    useEffect(() => { }, []);

    const DeleteContract = async () => {
        await axios.delete(myConfig.backUrl + 'contracts/' + contractId!.toString()).then(() => {
            handleClose();
            ToastsStore.success('The contract was deleted');
            getAllContracts();
        }).catch(() => {
            ToastsStore.error('The contract was not deleted');
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
                    <h3>Are you sure to delete the printer "{printerName}"</h3>
                        <br /><br />
                        <Button variant='contained' color='primary' onClick={DeleteContract}>Yes</Button>
                        <Button variant='contained' color='secondary' onClick={handleClose}>No</Button>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default ContractDeleteConfirmModal
