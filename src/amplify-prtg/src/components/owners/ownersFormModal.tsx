import React, { ChangeEvent, useEffect } from "react";
import { makeStyles, Theme, createStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import { Button} from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import axios from "axios";
import {
  ToastsStore,
  ToastsContainer,
  ToastsContainerPosition,
} from "react-toasts";
import { myConfig } from "../../configurations";
import { Owner } from "./owner";

export interface OwnerFormModalProps {
  show: boolean;
  hideModal: Function;
  getAllOwners: Function;
  isEdit: boolean;
  owner?: Owner | undefined;
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
  })
);

const OwnerFormModal = ({
  show,
  hideModal,
  getAllOwners,
  isEdit,
  owner
}: OwnerFormModalProps) => {
  const classes = useStyles();
  const [modalStyle] = React.useState(getModalStyle);

  const [inputOwnerName, setInputOwnerName] = React.useState("");

  useEffect(() => {
    fillList();
  });

  const fillList = () => {
    if (isEdit) {
      setInputOwnerName(owner!.name!)
    }
  };

  const AddOwner = () => {
    let ownerData: Owner = {
        name: inputOwnerName!
    };
    axios.post(myConfig.backUrl + 'owner', ownerData).then(() => {
      ToastsStore.success('The owner was saved');
      getAllOwners();
      handleClose();
    }).catch(() => {
        ToastsStore.error('The owner was not saved');
    })
  };

  const UpdateOwner = async () => {
    let ownerData: Owner = {
      id : owner!.id!,
      name : inputOwnerName!
    };
    await axios.put(myConfig.backUrl + 'owner/', ownerData).then(() => {
      ToastsStore.success('The owner was saved');
      getAllOwners();
      handleClose();
    }).catch(() => {
        ToastsStore.error('The Owner was not saved');
    });
  };

  const handleInputOwnerChange = (e: ChangeEvent<HTMLInputElement>) => {
    setInputOwnerName(e.target.value);
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
            {!isEdit ? (
              <h2 id="ownerform-modal-title">Add owner</h2>
            )
            :
              <h2 id="ownerform-modal-title">Update owner</h2>
            }
            <div id="ownerform-modal-description">
              <TextField
                label="owner name"
                id="inputOwner"
                name="inputOwner"
                placeholder="input the owner name"
                value={inputOwnerName}
                onChange={handleInputOwnerChange}
              />
              <br />
              <br />
              <br />
              <br />
              {!isEdit ? (
                <Button
                  variant="contained"
                  color="default"
                  onClick={() => AddOwner()}
                >
                  Save new
                </Button>
              ) : (
                <Button
                  variant="contained"
                  color="default"
                  onClick={() => UpdateOwner()}
                >
                  Save update
                </Button>
              )}
              <Button variant="contained" color="default" onClick={handleClose}>
                Cancel
              </Button>
            </div>
          </div>
        </div>
      </Modal>
    </div>
  );
};

export default OwnerFormModal;
