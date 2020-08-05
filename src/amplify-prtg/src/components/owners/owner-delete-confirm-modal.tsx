import React from "react";
import { makeStyles, Theme, createStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import { Button } from "@material-ui/core";
import axios from "axios";
import {
  ToastsStore,
  ToastsContainer,
  ToastsContainerPosition,
} from "react-toasts";
import { myConfig } from "../../configurations";
import { Owner } from "./owner";

export interface OwnerDeleteConfirmModalProps {
  show: boolean;
  hideModal: Function;
  getAllOwners: Function;
  owner : Owner | undefined;
}

function getModalStyle() {
  const top = 28;
  const left = 35;

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
      margin: 100,
      width: 400,
      backgroundColor: theme.palette.background.paper,
      border: "1px solid #000",
      boxShadow: theme.shadows[5],
      padding: theme.spacing(2, 4, 3),
    },
  })
);

const OwnerDeleteConfirmModal = ({
  show,
  hideModal,
  getAllOwners,
  owner
}: OwnerDeleteConfirmModalProps) => {
  const classes = useStyles();
  // getModalStyle is not a pure function, we roll the style only on the first render
  const [modalStyle] = React.useState(getModalStyle);

  const DeleteOwner = async () => {
    await axios
      .delete(myConfig.backUrl + "owner/" + owner!.id!.toString())
      .then(() => {
        handleClose();
        ToastsStore.success("The owner was deleted");
        getAllOwners();
      })
      .catch(() => {
        ToastsStore.error("The owner was not deleted");
      });
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
            <h3>Are you sure to delete the owner "{owner!.name!}"</h3>
            <br />
            <br />
            <Button variant="contained" color="primary" onClick={DeleteOwner}>
              Yes
            </Button>
            <Button variant="contained" color="secondary" onClick={handleClose}>
              No
            </Button>
          </div>
        </div>
      </Modal>
    </div>
  );
};

export default OwnerDeleteConfirmModal;
