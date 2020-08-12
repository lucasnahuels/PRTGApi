import React, { useEffect } from "react";
import { makeStyles, Theme, createStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody } from "@material-ui/core";
import {
  ToastsStore,
  ToastsContainer,
  ToastsContainerPosition,
} from "react-toasts";
import { DeviceDataViewModel } from "./device";

export interface PreviousMonthModalProps {
  show: boolean;
  hideModal: Function;
  info: DeviceDataViewModel;
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
      position: "absolute",
      backgroundColor: theme.palette.background.paper,
      border: "1px solid #000",
      boxShadow: theme.shadows[5],
      padding: theme.spacing(2, 4, 3),
    },
    root: {
      display: "flex",
      flexWrap: "wrap",
    },
    formControl: {
      margin: theme.spacing(1),
      minWidth: 120,
    },
    selectEmpty: {
      marginTop: theme.spacing(2),
    },
    titlesRow: {
      fontWeight: "bold",
      textAlign: "center",
    },
    dataRow: {
      textAlign: "center",
    },
  })
);

const PreviousMonthModal = ({show, hideModal, info }: PreviousMonthModalProps) => {
  const classes = useStyles();
  const [modalStyle] = React.useState(getModalStyle);

  useEffect(() => {
    fillList();
  });

  const fillList = () => {
  };

  const handleClose = () => {
    hideModal();
  };

  return (
    <div>
      <ToastsContainer
        position={ToastsContainerPosition.TOP_RIGHT}
        store={ToastsStore}
      />
      <Modal
        aria-labelledby="ownerform-modal-title"
        aria-describedby="ownerform-modal-description"
        open={show}
        onClose={handleClose}
      >
        <div style={modalStyle} className={classes.paper}>
          <div style={{ textAlign: "center" }}>
            <h5 id="ownerform-modal-title">Device info for previous month</h5>
            <div id="ownerform-modal-description">
               <TableContainer component={Paper}>
                    <Table size='medium'>
                        <TableHead aria-label="simple table">
                          <TableRow>
                            <TableCell className={classes.titlesRow} size="medium">
                                Color sheets
                            </TableCell>
                            <TableCell className={classes.titlesRow} size="medium">
                                B&W sheets
                            </TableCell>
                            <TableCell className={classes.titlesRow} size="medium">
                                Total
                            </TableCell>
                          </TableRow>
                        </TableHead>
                        <TableBody>
                            <TableRow >
                                <TableCell className={classes.dataRow}></TableCell>
                                <TableCell className={classes.dataRow}></TableCell>
                                <TableCell className={classes.dataRow}></TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </div>
          </div>
        </div>
      </Modal>
    </div>
  );
};

export default PreviousMonthModal;
