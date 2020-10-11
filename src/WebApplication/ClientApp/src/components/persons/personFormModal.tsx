import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import useApi from '../../helpers/axios-wrapper'
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { Employee } from '../contracts/contract';

export interface PersonFormModalProps {
    show: boolean,
    hideModal: Function,
    getAllPersons: Function,
    isEdit: boolean,
    person?: Employee | undefined
}


function getModalStyle() {
    const top = 50;
    const left = 50;

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
            backgroundColor: theme.palette.background.paper,
            border: '1px solid #000',
            boxShadow: theme.shadows[5],
            padding: theme.spacing(2, 4, 3)
        },
        root: {
            display: 'flex',
            flexWrap: 'wrap'
        },
        formControl: {
            margin: theme.spacing(1),
            minWidth: 120
        },
        selectEmpty: {
            marginTop: theme.spacing(2)
        }
    })
);

const PersonFormModal = ({ show, hideModal, getAllPersons, isEdit, person}: PersonFormModalProps) => {

    const classes = useStyles();
    const [modalStyle] = React.useState(getModalStyle);

    const [email, setEmail] = React.useState("");
    const [name, setName] = React.useState("");

    const axios = useApi();

    useEffect(() => { 
        fillList();
     // eslint-disable-next-line react-hooks/exhaustive-deps
     }, []);

    const fillList = () =>{
        if(isEdit){
            setEmail(person!.email)
            setName(person!.name!)
        }
    }

    const AddPerson = () => {
        let personData: Employee = {
            email: email
        };
        axios.post('employee', personData).then(() => {
            ToastsStore.success('The person was saved');
            getAllPersons();
            handleClose();
        }).catch(() => {
            ToastsStore.error('The person was not saved');
        })
    }

    const UpdatePerson = async () => {
        let personData: Employee = {
            id : person!.id!,
            email: email
        };
        await axios.put('employee/', personData).then(() => {
            ToastsStore.success('The person was saved');
            getAllPersons();
            handleClose();
        }).catch(() => {
            ToastsStore.error('The Person was not saved');
        });
    }

    const handleInputPersonAdressChange = (e: ChangeEvent<HTMLInputElement>) => {
        setEmail(e.target.value);
    }
    const handleInputPersonNameChange = (e: ChangeEvent<HTMLInputElement>) => {
        setName(e.target.value);
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
                    <div style={{ textAlign: 'center' }}>
                        {!isEdit ? (
                            <h2 id='personform-modal-title'>Add device owner person</h2>
                        )
                        :
                        (
                            <h2 id='personform-modal-title'>Update device owner person</h2>
                        )}
                        <div id='personform-modal-description'>
                            <TextField label='person name' id='inputPersonName' name='inputPersonName' placeholder='input the person name' value={name} onChange={handleInputPersonNameChange} />
                            <br />
                            <TextField label='person adress' id='inputPersonAdress' name='inputPersonAdress' placeholder='input the person adress' value={email} onChange={handleInputPersonAdressChange} />
                            <br/><br/><br/>
                            {!isEdit ? (
                                <Button variant='contained' color='default' onClick={() => AddPerson()} >Save new</Button>
                            ) : (
                                    <Button variant='contained' color='default' onClick={() => UpdatePerson()} >Save update</Button>
                                )
                            }
                            <Button variant='contained' color='default' onClick={handleClose} >Cancel</Button> 
                        </div>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default PersonFormModal
