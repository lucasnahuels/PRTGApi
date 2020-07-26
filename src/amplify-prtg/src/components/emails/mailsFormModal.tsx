import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button, RadioGroup, FormControlLabel, Radio} from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import axios from 'axios';
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { myConfig } from '../../configurations';

export interface EmailFormModalProps {
    show: boolean,
    hideModal: Function,
    getAllEmails: Function,
    isEdit: boolean,
    emailId?: number | undefined
    adress?: string,
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

const EmailFormModal = ({ show, hideModal, getAllEmails, isEdit, emailId, adress}: EmailFormModalProps) => {

    const classes = useStyles();
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);

    const [state, setState] = React.useState({
        emailAdress: '',
        personValue: ''
    });

    useEffect(() => { fillList(); }, []);
    //when format is "useEffect(() => {}, []);" only render the first time instead of every time there´re changes

    const fillList = () =>{
        if(isEdit){
            setState({
            emailAdress : adress!,
            personValue : ''
        }) 
        }
    }
    
    // const CheckEmailExistence = (): boolean =>{
    //     // let notInTheList : boolean = true;
    //     // listOfEmails!.forEach(email => {
    //     //     if(email.emailAdress === state.emailAdress!) 
    //     //         notInTheList = false;
    //     // });
    //     // return notInTheList;

    //     const email = listOfEmails!.find(x => x.emailAdress === state.emailAdress);
    //     return (email) ? true : false;
    // }

    const AddEmail = () => {
        // if(CheckEmailExistence()){
        //     ToastsStore.error('The email adress already exists');
        //     return;
        // }

        // let emailData: Email = {
        //     emailAdress: state.emailAdress!
        // };
        // axios.post(myConfig.backUrl + 'emails', emailData).then(() => {
        //     handleClose();
        //     ToastsStore.success('The email was saved');
        //     getAllEmails();
        // }).catch(() => {
        //     ToastsStore.error('The email was not saved');
        // })
    }

    const UpdateEmail = async () => {
        // if (CheckEmailExistence()) {
        //     ToastsStore.error('The email adress already exists');
        //     return;
        // }

        // let emailData: Email = {
        //     emailId : emailId,
        //     emailAdress: state.emailAdress!
        // };
        // await axios.put(myConfig.backUrl + 'emails/' + emailId!.toString(), emailData).then(() => {
        //     handleClose();
        //     ToastsStore.success('The email was saved');
        //     getAllEmails();
        // }).catch(() => {
        //     ToastsStore.error('The Email was not saved');
        // });
    }

    const handleInputEmailAdressChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, emailAdress: e.target.value });
    }
    const handleChangeRadioButton = (e: ChangeEvent<HTMLInputElement>) => {
      setState({ ...state, personValue: e.target.value });
    };

    

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
                    <div style={{ textAlign: 'center' }}>
                        <h2 id='emailform-modal-title'>Add email</h2>
                        <div id='emailform-modal-description'>
                            <TextField label='email adress' id='inputEmailAdress' name='inputEmailAdress' placeholder='input the email adress' value={state.emailAdress} onChange={handleInputEmailAdressChange} />
                            <br />
                            <br />
                            <RadioGroup 
                                row 
                                aria-label="person" 
                                name="person" 
                                style={{ marginLeft:'20%' }}
                                value={state.personValue} 
                                onChange={handleChangeRadioButton}>
                                    <FormControlLabel value="It-one person" control={<Radio />} label="It-one person" />
                                    <FormControlLabel value="Device owner person" control={<Radio />} label="Device owner person" />
                            </RadioGroup>
                            <br/><br/><br/><br/>
                            {!isEdit ? (
                                <Button variant='contained' color='default' onClick={() => AddEmail()} >Save new</Button>
                            ) : (
                                    <Button variant='contained' color='default' onClick={() => UpdateEmail()} >Save update</Button>
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

export default EmailFormModal
