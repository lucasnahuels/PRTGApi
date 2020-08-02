import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button, Step, Select, InputLabel, Tooltip, MenuItem } from '@material-ui/core';
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
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);

    const [selectedDeviceValue, setSelectedDeviceValue] = React.useState("");
    const [selectedOwnerValue, setSelectedOwnerValue] = React.useState("");
    const [stateDevice, setDevice] = React.useState<IDeviceList>();
    const [stateOwner, setOwner] = React.useState<IOwnerList>();
    const [contract, setContract] = React.useState<Contract>();

    type FormData = {
        owner : number,
        device :number,
        blackAndWhiteLimitSet:number,
        colorLimitSet:number,
        blackAndWhitePrice:number,
        colorPrice: number,
        surplusBlackAndWhitePrice: number,
        surplusColorPrice: number, 
    };

    const { register, setValue, handleSubmit } = useForm<FormData>({
      defaultValues: {
        owner: 0,
        device: 0,
        blackAndWhiteLimitSet: 0,
        colorLimitSet: 0,
        blackAndWhitePrice: 0,
        colorPrice: 0,
        surplusBlackAndWhitePrice: 0,
        surplusColorPrice: 0,
      },
    });
    const onSubmit = handleSubmit(({ 
        device, 
        owner,
        blackAndWhiteLimitSet,
        colorLimitSet,
        blackAndWhitePrice,
        colorPrice,
        surplusBlackAndWhitePrice,
        surplusColorPrice,
    }) => {
        let ownerId: Owner = { id: owner };
        let deviceObjId: Device = { objId : device};
        let contractData: Contract = {
          ownerId: parseInt(selectedOwnerValue),
          deviceId: parseInt(selectedDeviceValue),
          blackAndWhiteLimitSet : blackAndWhiteLimitSet,
          colorLimitSet : colorLimitSet,
          blackAndWhitePrice : blackAndWhitePrice,
          colorPrice : colorPrice,
          surplusBlackAndWhitePrice : surplusBlackAndWhitePrice,
          surplusColorPrice : surplusColorPrice,
        };
        setContract(contractData);
    }); 

    useEffect(() => { fillList(); }, []); 
    React.useEffect(() => {
        console.log("renderGetDevices");
        GetDevices();
        console.log("renderGetOwners");
        GetOwners();
    }, []);

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

    const fillList = () => {
        if (isEdit) {
            // setState({
            //     nameOfCompany: contractToEdit!.nameOfCompany,
            //     printer: contractToEdit!.printer,
            //     blackAndWhiteSheets: contractToEdit!.blackAndWhiteSheets,
            //     colorSheets: contractToEdit!.colorSheets,
            //     blackToner: contractToEdit!.blackToner,
            //     cyanToner: contractToEdit!.cyanToner,
            //     yellowToner: contractToEdit!.yellowToner,
            //     magentaToner: contractToEdit!.magentaToner,
            //     month: contractToEdit!.month
            // });
        }
    }

    const AddContract = () => {
        axios.post(myConfig.backUrl + 'contract', contract).then(() => {
            handleClose();
            ToastsStore.success('The contract was saved');
            getAllContracts();
        }).catch(() => {
            ToastsStore.error('The contract was not saved');
        })
    }

    const UpdateContract = async () => {
        // await axios.put(myConfig.backUrl + 'contracts/' + contractToEdit!.contractId!.toString(), contract).then(() => {
        //     handleClose();
        //     ToastsStore.success('The contract was saved');
        //     getAllContracts();
        // }).catch(() => {
        //     ToastsStore.error('The contract was not saved');
        // });
    }

    const HandleChangeDevice = (event: React.ChangeEvent<{ value: unknown }>) => {
        setSelectedDeviceValue(event.target.value as string);
    };

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
                        <h2 id='contractform-modal-title'>Add contract</h2>
                        <div id='contractform-modal-description'>
                                <InputLabel id="deviceNameLabel">Device</InputLabel>
                                <Select
                                    className={classes.formRoot} 
                                    required
                                    id='inputDevice'
                                    labelId="deviceNameLabel"
                                    name="device" inputRef={register}
                                    value={selectedDeviceValue}
                                    onChange={HandleChangeDevice}
                                >
                                    {stateDevice !== undefined &&
                                        stateDevice.listOfDevices !== undefined
                                        ? stateDevice.listOfDevices.map((device) => (
                                            <MenuItem
                                                key={device.objId!.toString()}
                                                value={device.objId!.toString()}
                                            >
                                                {device.device}
                                            </MenuItem>
                                        ))
                                        : null}
                                </Select>
                                <Tooltip title="If the owner you need is not in the list, 
                                you will have to add it since the 'Handle owners view'">
                                    <div>
                                    <InputLabel id="ownerNameLabel">Owner</InputLabel>
                                    <Select
                                        className={classes.formRoot} 
                                        required
                                        id='inputName'
                                        labelId="ownerNameLabel"
                                        name="owner" inputRef={register}
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
