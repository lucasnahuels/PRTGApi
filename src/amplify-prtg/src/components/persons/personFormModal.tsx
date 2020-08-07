import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button, RadioGroup, FormControlLabel, Radio} from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import axios from 'axios';
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { myConfig } from '../../configurations';
import { Person } from '../contracts/contract';

export interface PersonFormModalProps {
    show: boolean,
    hideModal: Function,
    getAllPersons: Function,
    isEdit: boolean,
    person?: Person | undefined
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
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);

    const [email, setEmail] = React.useState("");
    const [name, setName] = React.useState("");

    useEffect(() => { fillList(); }, []);
    //when format is "useEffect(() => {}, []);" only render the first time instead of every time there´re changes

    const fillList = () =>{
        if(isEdit){
            setEmail(person!.email)
            setName(person!.name!)
        }
    }
    
    // const CheckPersonExistence = (): boolean =>{
    //     // let notInTheList : boolean = true;
    //     // listOfPersons!.forEach(person => {
    //     //     if(person.personAdress === state.personAdress!) 
    //     //         notInTheList = false;
    //     // });
    //     // return notInTheList;

    //     const person = listOfPersons!.find(x => x.personAdress === state.personAdress);
    //     return (person) ? true : false;
    // }

    const AddPerson = () => {
        // if(CheckPersonExistence()){
        //     ToastsStore.error('The person adress already exists');
        //     return;
        // }

        // let personData: Person = {
        //     email: email
        // };
        // axios.post(myConfig.backUrl + 'employee', personData).then(() => {
        //     handleClose();
        //     ToastsStore.success('The person was saved');
        //     getAllPersons();
        // }).catch(() => {
        //     ToastsStore.error('The person was not saved');
        // })
    }

    const UpdatePerson = async () => {
        // if (CheckPersonExistence()) {
        //     ToastsStore.error('The person adress already exists');
        //     return;
        // }

        let personData: Person = {
            id : person!.id!,
            email: email
        };
        await axios.put(myConfig.backUrl + 'employee/', personData).then(() => {
            handleClose();
            ToastsStore.success('The person was saved');
            getAllPersons();
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
                        <h2 id='personform-modal-title'>Add device owner person</h2>
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
                            {/*en el momento del click y manda el elemento como parametro por defecto.. Si fuera handleClose(), el onClick estaria esperando lo que le retorna esa funcion (x ej. una llamada a aotra funcion)*/}
                        </div>
                    </div>
                </div>
            </Modal>
        </div>
    );
}

export default PersonFormModal
