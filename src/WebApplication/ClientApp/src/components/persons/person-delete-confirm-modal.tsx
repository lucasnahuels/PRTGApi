import React from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button } from '@material-ui/core';
import useApi from '../../helpers/axios-wrapper'
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { Employee } from '../contracts/contract';

export interface PersonDeleteConfirmModalProps {
    show: boolean,
    hideModal: Function,
    getAllPersons: Function,
    person: Employee | undefined
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

const PersonDeleteConfirmModal = ({ show, hideModal, getAllPersons, person }: PersonDeleteConfirmModalProps) => {

    const classes = useStyles();
    const [modalStyle] = React.useState(getModalStyle);
    const axios = useApi();
    const DeletePerson = async () => {
        await axios.delete('employee/' + person!.id!.toString()).then( () => {
            ToastsStore.success('The Person was deleted');
            getAllPersons();
            handleClose();
        }).catch( () => {
            ToastsStore.error('The Person was not deleted');
        });
    }

    const handleClose = () => {
        hideModal();
    };

    return (
        <div>
            <ToastsContainer position={ToastsContainerPosition.TOP_RIGHT} store={ToastsStore} />
            <Modal
                aria-labelledby='personform-modal-title'
                aria-describedby='personform-modal-description'
                open={show}
                onClose={handleClose}
            >
                <div style={modalStyle} className={classes.paper}>
                <div style={{textAlign:'center'}}>    
                 <h3>Are you sure to delete the employee "{person!.email}"</h3>
                    <br/><br/>
                        <Button variant='contained' color='primary' onClick={DeletePerson}>Yes</Button>
                        <Button variant='contained' color='secondary' onClick={handleClose}>No</Button>
                </div>
                </div>
            </Modal>
        </div>
    );
}

export default PersonDeleteConfirmModal
