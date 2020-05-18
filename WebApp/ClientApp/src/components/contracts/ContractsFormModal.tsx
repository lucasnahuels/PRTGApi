import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import axios from 'axios';
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { Contract } from './contract';
import { myConfig } from '../../configurations';

export interface ContractFormModalProps {
    show: boolean,
    hideModal: Function,
    getAllContracts: Function,
    isEdit: boolean,
    contractToEdit? : Contract 
}


function getModalStyle() {
    const top = 28;
    const left = 31;

    return {
        top: `${15}%`,
        left: `${left}%`,
        transform: `translate(-${top}%, -${left}%)`,
    };
}

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        paper: {
            position: 'absolute',
            margin: 100,
            width: 600,
            backgroundColor: theme.palette.background.paper,
            border: '1px solid #000',
            boxShadow: theme.shadows[5],
            padding: theme.spacing(2, 4, 3)
        },
        formRoot: {
            margin: theme.spacing(1),
            width: '25ch',
        },
        container: {
            display: 'flex',
            flexWrap: 'wrap',
        },
    })
);

const ContractFormModal = ({ show, hideModal, getAllContracts, isEdit, contractToEdit }: ContractFormModalProps) => {

    const classes = useStyles();
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);
    const [state, setState] = React.useState({
        nameOfCompany: '',
        printer: '',
        blackAndWhiteSheets: 0,
        colorSheets: 0,
        blackToner: 0,
        cyanToner: 0,
        yellowToner: 0,
        magentaToner: 0,
        month: 0,
    })
    
    useEffect(() => { fillList(); }, []); 

    const fillList = () => {
        if (isEdit) {
            setState({
                nameOfCompany: contractToEdit!.nameOfCompany,
                printer: contractToEdit!.printer,
                blackAndWhiteSheets: contractToEdit!.blackAndWhiteSheets,
                colorSheets: contractToEdit!.colorSheets,
                blackToner: contractToEdit!.blackToner,
                cyanToner: contractToEdit!.cyanToner,
                yellowToner: contractToEdit!.yellowToner,
                magentaToner: contractToEdit!.magentaToner,
                month: contractToEdit!.month
            });
        }
    }

    const AddContract = () => {
    let contractData: Contract = { nameOfCompany: state.nameOfCompany,
                                    printer: state.printer,
                                    month: state.month,
                                    blackAndWhiteSheets: state.blackAndWhiteSheets,
                                    colorSheets: state.colorSheets,
                                    blackToner: state.blackToner,
                                    cyanToner: state.cyanToner,
                                    magentaToner: state.magentaToner,
                                    yellowToner: state.yellowToner 
                                };
        axios.post(myConfig.backUrl + 'contracts', contractData).then(() => {
            handleClose();
            ToastsStore.success('The contract was saved');
            getAllContracts();
        }).catch(() => {
            ToastsStore.error('The contract was not saved');
        })
    }

    const UpdateContract = async () => {
        let contractData: Contract = {
                                    contractId: contractToEdit!.contractId!,
                                    nameOfCompany: state.nameOfCompany,
                                    printer: state.printer,
                                    month: state.month,
                                    blackAndWhiteSheets: state.blackAndWhiteSheets,
                                    colorSheets: state.colorSheets,
                                    blackToner: state.blackToner,
                                    cyanToner: state.cyanToner,
                                    magentaToner: state.magentaToner,
                                    yellowToner: state.yellowToner
                                 };
        await axios.put(myConfig.backUrl + 'contracts/' + contractToEdit!.contractId!.toString(), contractData).then(() => {
            handleClose();
            ToastsStore.success('The contract was saved');
            getAllContracts();
        }).catch(() => {
            ToastsStore.error('The contract was not saved');
        });
    }

    const handleInputNameOfCompanyChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, nameOfCompany: e.target.value }); 
    }
    const handleInputNameOfPrinterChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, printer: e.target.value });
    }
    const handleInputMonthChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, month: parseInt(e.target.value) });
    }
    const handleInputBWSheetsChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, blackAndWhiteSheets: parseInt(e.target.value) }); 
    }
    const handleInputColorSheetsChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, colorSheets: parseInt(e.target.value) });
    }
    const handleInputBlackTonnerChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, blackToner: parseInt(e.target.value) });
    }
    const handleInputCyanTonnerChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, cyanToner: parseInt(e.target.value) });
    }
    const handleInputMagentaTonnerChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, magentaToner: parseInt(e.target.value) });
    }
    const handleInputYellowTonnerChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, yellowToner: parseInt(e.target.value) });
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
                    <h2 id='contractform-modal-title'>Add contract</h2>
                    <div id='contractform-modal-description'>
                        <TextField className={classes.formRoot} required label='company name' id='inputName' name='inputName' placeholder='input the name of the company' value={state.nameOfCompany} onChange={handleInputNameOfCompanyChange} />
                        <TextField className={classes.formRoot} required label='printer name' id='inputPrinter' name='inputPrinter' placeholder='input the name of the printer' value={state.printer} onChange={handleInputNameOfPrinterChange} />
                        <br /><br />
                        <TextField className={classes.formRoot} required label='month' id='inputMonth' name='inputMonth' placeholder='input the month' value={state.month} onChange={handleInputMonthChange} />
                        <br /><br />
                        <TextField className={classes.formRoot} required label='black and white sheets' id='inputQuantityBWSheets' name='inputQuantityBWSheets' placeholder='input the quantity of the black and white sheets' value={state.blackAndWhiteSheets} onChange={handleInputBWSheetsChange} />
                        <TextField className={classes.formRoot} required label='color sheets' id='inputColorSheets' name='inputColorSheets' placeholder='input the quantity of color sheets' value={state.colorSheets} onChange={handleInputColorSheetsChange} />
                        <br /><br />
                        <TextField className={classes.formRoot} required label='black tonner' id='inputBlackTonner' name='inputBlackTonner' placeholder='input the the quantity of black tonner remaining' value={state.blackToner} onChange={handleInputBlackTonnerChange} />
                        <TextField className={classes.formRoot} required label='cyan tonner' id='inputCyanTonner' name='inputCyanTonner' placeholder='input the the quantity of cyan tonner remaining' value={state.cyanToner} onChange={handleInputCyanTonnerChange} />
                        <TextField className={classes.formRoot} required label='magenta tonner' id='inputMagentaTonner' name='inputMagentaTonner' placeholder='input the the quantity of magenta tonner remaining' value={state.magentaToner} onChange={handleInputMagentaTonnerChange} />
                        <TextField className={classes.formRoot} required label='yellow tonner' id='inputYellowTonner' name='inputYellowTonner' placeholder='input the the quantity of yellow tonner remaining' value={state.yellowToner} onChange={handleInputYellowTonnerChange} />
                        <br /><br />

                        {!isEdit ? (
                            <Button variant='contained' color='default' onClick={() => AddContract()}>Save new</Button>
                        ) : (
                                <Button variant='contained' color='default' onClick={() => UpdateContract()} >Save update</Button>
                            )
                        }
                        <Button variant='contained' color='default' onClick={handleClose} >Cancel</Button> {/*en el momento del click y manda el elemento como parametro por defecto.. Si fuera handleClose(), el onClick estaria esperando lo que le retorna esa funcion (x ej. una llamada a aotra funcion)*/}
                    </div>
                </div>    
                </div>
            </Modal>
        </div>
    );
}

export default ContractFormModal
