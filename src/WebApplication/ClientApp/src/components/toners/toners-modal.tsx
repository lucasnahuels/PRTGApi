import React from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { Paper } from '@material-ui/core';
import {Chart, BarSeries, Title, ArgumentAxis, ValueAxis} from '@devexpress/dx-react-chart-material-ui';
import { Animation } from '@devexpress/dx-react-chart';
import { Toner } from './toner';

export interface TonersModalProps {
    show: boolean,
    hideModal: Function,
    info? : Toner
}

function getModalStyle() {
    const top = 28;
    const left = 35;

    return {
        top: `${18}%`,
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

const TonersModal = ({ show, hideModal, info}: TonersModalProps) => {
    const [modalStyle] = React.useState(getModalStyle);

    const classes = useStyles();

    const handleClose = () => {
        hideModal();
    };

    const data = [
        { color: 'Black', black: info!.blackToner },
        { color: 'Cyan', cyan: info!.cyanToner },
        { color: 'Yellow', yellow: info!.yellowToner },
        { color: 'Magenta', magenta: info!.magentaToner },
        { color: '', total :100 },
    ];

    return (
        <div>
            <ToastsContainer position={ToastsContainerPosition.TOP_RIGHT} store={ToastsStore} />
            <Modal
                aria-labelledby='simple-modal-title'
                aria-describedby='simple-modal-description'
                open={show}
                onClose={handleClose}
            >
                <div style={modalStyle} className={classes.paper}>
                    <Paper>
                        <Chart data={data}>

                            <ArgumentAxis/>
                            <ValueAxis />

                            <BarSeries 
                                color = "black"
                                valueField="black"
                                argumentField="color"
                            />
                            <BarSeries
                                color="cyan"
                                valueField="cyan"
                                argumentField="color"
                            />
                            <BarSeries
                                color="yellow"
                                valueField="yellow"
                                argumentField="color"
                            />
                            <BarSeries
                                color="magenta"
                                valueField="magenta"
                                argumentField="color"
                            />
                            <BarSeries
                                color="white"
                                valueField="total"
                                argumentField="color"
                            />
                            <Title text="Toners" />
                            <Animation />
                        </Chart>
                    </Paper>
                </div>
            </Modal>
        </div>
    );
}

export default TonersModal
