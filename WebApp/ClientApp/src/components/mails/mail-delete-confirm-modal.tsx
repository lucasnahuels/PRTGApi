import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button } from '@material-ui/core';
import axios from 'axios';
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { Mail } from './mail';

export interface EmailDeleteConfirmModalProps {
    show: boolean,
    hideModal: Function,
    getAllEmails: Function,
    emailId: number | undefined
    emailAdress: string | undefined
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

const EmailDeleteConfirmModal = ({ show, hideModal, getAllEmails, emailId, emailAdress }: EmailDeleteConfirmModalProps) => {

    const classes = useStyles();
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);

    // tslint:disable-next-line: no-floating-promises
    useEffect(() => { }, []);

    const DeleteEmail = async () => {
        await axios.delete('https://localhost:44370/api/emails/' + emailId!.toString()).then( () => {
            handleClose();
            ToastsStore.success('The E-Mail was deleted');
            getAllEmails();
        }).catch( () => {
            ToastsStore.error('The E-Mail was not deleted');
        });
    }

    const handleClose = () => {
        hideModal();
    };

    return (
        <div>
            <ToastsContainer position={ToastsContainerPosition.TOP_RIGHT} store={ToastsStore} />
            <Modal
                aria-labelledby='emailform-modal-title'
                aria-describedby='emailform-modal-description'
                open={show}
                onClose={handleClose}
            >
                <div style={modalStyle} className={classes.paper}>
                <div style={{textAlign:'center'}}>    
                 <h3>Are you sure to delete the E-Mail "{emailAdress}"</h3>
                    <br/><br/>
                        <Button variant='contained' color='primary' onClick={DeleteEmail}>Yes</Button>
                        <Button variant='contained' color='secondary' onClick={handleClose}>No</Button>
                </div>
                </div>
            </Modal>
        </div>
    );
}

export default EmailDeleteConfirmModal
