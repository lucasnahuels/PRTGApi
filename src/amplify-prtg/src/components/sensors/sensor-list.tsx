import React from 'react';
import { Grid, makeStyles, Theme, createStyles } from "@material-ui/core";

const SensorList = () => {
    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            margins: {
                paddingTop: '60px',
                paddingBottom: '60px',
            },
        })
    );
    const classes = useStyles();


    return (
        <div className={classes.margins}>
            <Grid container xs={12} item>
                <Grid item xs={1}></Grid>
                <Grid item xs={10}>
                </Grid>
                <Grid item xs={1}></Grid>
            </Grid>
        </div>
    )
}

export default SensorList;
