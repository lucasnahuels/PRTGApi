import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button, InputLabel, MenuItem, Select, Tooltip } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import useApi from '../../helpers/axios-wrapper'
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { Employee } from '../contracts/contract';
import { Owner } from '../owners/owner';

export interface PersonFormModalProps {
    show: boolean,
    hideModal: Function,
    getAllPersons: Function,
    isEdit: boolean,
    personToEdit?: Employee | undefined
}

export interface IOwnerList {
    listOfOwners: Owner[]
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
        },
        formRoot: {
            margin: theme.spacing(1),
            width: '25ch',
        },
    })
);

const PersonFormModal = ({ show, hideModal, getAllPersons, isEdit, personToEdit}: PersonFormModalProps) => {

    const classes = useStyles();
    const [modalStyle] = React.useState(getModalStyle);

    const [email, setEmail] = React.useState("");
    const [name, setName] = React.useState("");
    const [stateOwner, setOwner] = React.useState<IOwnerList>();
    const [selectedOwnerValue, setSelectedOwnerValue] = React.useState("");

    const axios = useApi();

    useEffect(() => { 
        GetOwners();
        fillList();
     // eslint-disable-next-line react-hooks/exhaustive-deps
     }, []);

    const fillList = () =>{
        if(isEdit){
            setEmail(personToEdit!.email)
            setName(personToEdit!.name!)
        }
    }

    const AddPerson = () => {
        let personData: Employee = {
            name: name,
            email: email,
            ownerId : parseInt(selectedOwnerValue)
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
        console.log(personToEdit);
        let personData: Employee = {
            id : personToEdit!.id!,
            name: name,
            email: email,
            ownerId: parseInt(selectedOwnerValue)
        };
        await axios.put('employee/', personData).then(() => {
            ToastsStore.success('The person was saved');
            getAllPersons();
            handleClose();
        }).catch(() => {
            ToastsStore.error('The Person was not saved');
        });
    }

    const GetOwners = async () => {
        await axios.get(`Owner`).then((response: any) => {
            setOwner({ ...stateOwner, listOfOwners: response.data });
        });
    };

    const HandleChangeOwner = (event: React.ChangeEvent<{ value: unknown }>) => {
        setSelectedOwnerValue(event.target.value as string);
    };

    const handleInputPersonAddressChange = (e: ChangeEvent<HTMLInputElement>) => {
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
                            <Tooltip title="If the owner you need is not in the list, 
                                you will have to add it since the 'Handle owners view'">
                                <div>
                                    <InputLabel id="ownerNameLabel">Owner</InputLabel>
                                    <Select
                                        className={classes.formRoot}
                                        required
                                        id='inputName'
                                        labelId="ownerNameLabel"
                                        value={selectedOwnerValue}
                                        onChange={HandleChangeOwner}
                                    >
                                        {stateOwner !== undefined &&
                                            stateOwner.listOfOwners !== undefined
                                            ? stateOwner.listOfOwners.sort().map((owner) => (
                                                <MenuItem
                                                    key={owner!.id!.toString()}
                                                    value={owner!.id!.toString()}
                                                >
                                                    {owner!.name}
                                                </MenuItem>
                                            ))
                                            : null}
                                    </Select>
                                </div>
                            </Tooltip>
                            <br /><br />
                            <TextField label='full name' id='inputPersonName' name='inputPersonName' placeholder='input the person full name' value={name} onChange={handleInputPersonNameChange} />
                            <br />
                            <TextField label='e-mail address' id='inputPersonAddress' name='inputPersonAddress' placeholder='input the person e-mail address' value={email} onChange={handleInputPersonAddressChange} />
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
