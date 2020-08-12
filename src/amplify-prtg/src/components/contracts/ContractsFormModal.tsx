import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button, Select, InputLabel, Tooltip, MenuItem } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import axios from 'axios';
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { Contract } from './contract';
import { myConfig } from '../../configurations';
import { useForm } from "react-hook-form";
import { Owner } from '../owners/owner';
import { Device } from '../sensors/device';

export interface ContractFormModalProps {
    show: boolean,
    hideModal: Function,
    getAllContracts: Function,
    isEdit: boolean,
    contractToEdit? : Contract 
}
export interface IDeviceList {
    listOfDevices?: Device[],
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
        transform: `translate(-${top}%, -${left}%)`,
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
        formRoot: {
            margin: theme.spacing(1),
            width: '25ch',
        },
        container: {
            display: 'flex',
            flexWrap: 'wrap',
            justifyContent: 'center',
        },
    })
);

const ContractFormModal = ({ show, hideModal, getAllContracts, isEdit, contractToEdit }: ContractFormModalProps) => {

    const classes = useStyles();
    const [modalStyle] = React.useState(getModalStyle);

    const [selectedOwnerValue, setSelectedOwnerValue] = React.useState("");
    const [stateDevice, setDevice] = React.useState<IDeviceList>();
    const [stateOwner, setOwner] = React.useState<IOwnerList>();
    const [contract, setContract] = React.useState<Contract>();

    type FormData = {
        id? : number,
        blackAndWhiteLimitSet:number,
        colorLimitSet:number,
        blackAndWhitePrice:number,
        colorPrice: number,
        surplusBlackAndWhitePrice: number,
        surplusColorPrice: number, 
    };

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const { register, setValue, handleSubmit } = useForm<FormData>({
      defaultValues: {
        id: isEdit ? contractToEdit!.id! : 0,
        blackAndWhiteLimitSet: isEdit? contractToEdit!.blackAndWhiteLimitSet : 0,
        colorLimitSet: isEdit? contractToEdit!.colorLimitSet : 0,
        blackAndWhitePrice: isEdit? contractToEdit!.blackAndWhitePrice : 0,
        colorPrice: isEdit? contractToEdit!.colorPrice : 0,
        surplusBlackAndWhitePrice: isEdit? contractToEdit!.surplusBlackAndWhitePrice : 0,
        surplusColorPrice: isEdit? contractToEdit!.surplusColorPrice : 0,
      },
    });
    const onSubmit = handleSubmit(({
        blackAndWhiteLimitSet,
        colorLimitSet,
        blackAndWhitePrice,
        colorPrice,
        surplusBlackAndWhitePrice,
        surplusColorPrice,
    }) => {
        let contractData: Contract = {
            ownerId: parseInt(selectedOwnerValue),
            blackAndWhiteLimitSet : blackAndWhiteLimitSet,
            colorLimitSet : colorLimitSet,
            blackAndWhitePrice : blackAndWhitePrice,
            colorPrice : colorPrice,
            surplusBlackAndWhitePrice : surplusBlackAndWhitePrice,
            surplusColorPrice : surplusColorPrice,
        };
        console.log(selectedOwnerValue);
        console.log(contractData);
        setContract(contractData);
    }); 
    
    useEffect(() => { fillList(); }); 
    const fillList = () => {
        if (isEdit) {
            setSelectedOwnerValue(contractToEdit!.owner!.name!);
        }
    };
    
    React.useEffect(() => {
        console.log("renderGetDevices");
        GetDevices();
        console.log("renderGetOwners");
        GetOwners();
    });

    const GetDevices = async () => {
        await axios.get(myConfig.backUrl + `sensor/GetAllDevices`).then((response) => {
            setDevice({ ...stateDevice, listOfDevices: response.data });
        });
    };
    const GetOwners = async () => {
        await axios.get(myConfig.backUrl + `Owner`).then((response) => {
            setOwner({ ...stateOwner, listOfOwners: response.data });
        });
    };

    const AddContract = () => {
        debugger
        axios.post(myConfig.backUrl + 'contract', contract).then(() => {
            handleClose();
            ToastsStore.success('The contract was saved');
            getAllContracts();
        }).catch(() => {
            ToastsStore.error('The contract was not saved');
        })
    }

    const UpdateContract = async () => {
        await axios.put(myConfig.backUrl + 'contract/', contract).then(() => {
            handleClose();
            ToastsStore.success('The contract was saved');
            getAllContracts();
        }).catch(() => {
            ToastsStore.error('The contract was not saved');
        });
    }

    const HandleChangeOwner = (event: React.ChangeEvent<{ value: unknown }>) => {
        setSelectedOwnerValue(event.target.value as string);
    };

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
                <form onSubmit={onSubmit}>
                    <div style={modalStyle} className={classes.paper}>
                    <div style={{ textAlign: 'center' }}>
                        {
                        !isEdit ? (
                            <h2 id='contractform-modal-title'>Add contract</h2>
                        )
                        :
                        (
                            <h2 id='contractform-modal-title'>Update contract</h2>
                        )}

                        <div id='contractform-modal-description'>
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
                                            ? stateOwner.listOfOwners.map((owner) => (
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
                                <TextField className={classes.formRoot} required type="number"
                                    label='black and white limit set' placeholder='black and white limit set'
                                    id='inputBWLimitSheets' name="blackAndWhiteLimitSet" inputRef={register}
                                />
                                <TextField className={classes.formRoot} required type="number" 
                                    label='color sheets limit set'
                                    id='inputColorLimitSheets' name='colorLimitSet' inputRef={register}
                                />
                            <br /><br />
                                <TextField className={classes.formRoot} required type="number" inputProps={{step:'any'}}
                                    label='black and white normal price per unit'
                                    id='inputPriceBWSheet' name='blackAndWhitePrice' inputRef={register}
                                />
                                <TextField className={classes.formRoot} required type="number" inputProps={{step:'any'}}
                                    label='color normal price per unit'
                                    id='inputPriceColorSheet' name='colorPrice' inputRef={register}
                                />
                            <br /><br />
                                <TextField className={classes.formRoot} required type="number" inputProps={{step:'any'}}
                                    label='black and white surplus price per unit'
                                    id='inputPriceBWSurplusSheet' name='surplusBlackAndWhitePrice' inputRef={register} 
                                />
                                <TextField className={classes.formRoot} required type="number" inputProps={{step:'any'}}
                                    label='color surplus price per unit' 
                                    id='inputPriceColorSurplusSheet' name='surplusColorPrice' inputRef={register}  
                                />
                            <br /><br />
                            {!isEdit ? (
                                <Button type="submit" variant='contained' color='default' onClick={() => AddContract()}>Save new</Button>
                            ) : (
                                    <Button variant='contained' color='default' onClick={() => UpdateContract()} >Save update</Button>
                                )
                            }
                            <Button variant='contained' color='default' onClick={handleClose} >Cancel</Button> {/*en el momento del click y manda el elemento como parametro por defecto.. Si fuera handleClose(), el onClick estaria esperando lo que le retorna esa funcion (x ej. una llamada a aotra funcion)*/}
                        </div>
                    </div>    
                    </div>
                </form>
            </Modal>
        </div>
    );
}

export default ContractFormModal
