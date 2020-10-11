import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import { createStyles, Theme, makeStyles } from '@material-ui/core/styles';
import { Button } from '@material-ui/core';
import Grid from '@material-ui/core/Grid';
import { purple } from '@material-ui/core/colors';
import { Link } from 'react-router-dom';
import { useAuth0 } from './auth/react-auth0-spa';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            flexGrow: 1
        },
        menuButton: {
            marginRight: theme.spacing(2)
        },
        title: {
            flexGrow: 1,
            display: 'none',
            [theme.breakpoints.up('sm')]: {
                display: 'block'
            },
            fontFamily : 'Impact'
        },
        colorButton: {
            color: theme.palette.getContrastText(purple[500]),
            backgroundColor: purple[700],
            '&:hover': {
                backgroundColor: purple[500],
            },
            boxShadow: 'none',
            textTransform: 'none',
            fontSize: 16,
            padding: '6px 12px',
            border: '1px solid',
            lineHeight: 1.5,
            borderColor: '#0063cc',
            fontFamily: [
                '-apple-system',
                'BlinkMacSystemFont',
                '"Segoe UI"',
                'Roboto',
                '"Helvetica Neue"',
                'Arial',
                'sans-serif',
                '"Apple Color Emoji"',
                '"Segoe UI Emoji"',
                '"Segoe UI Symbol"',
            ].join(','),
            '&:active': {
                boxShadow: 'none',
                backgroundColor: '#0062cc',
                borderColor: '#005cbf',
            },
            '&:focus': {
                boxShadow: '0 0 0 0.2rem rgba(0,123,255,.5)',
            }
        },
    })
);

export default function SearchAppBar() {
    const classes = useStyles();
    const { isAuthenticated, loginWithRedirect, logoutWithRedirect } = useAuth0();

    return (
        <div className={classes.root}>
            <AppBar position='static'>
                <Toolbar>
                    <Grid container xs={4} spacing={3} item>
                        <Grid item xs={12}>
                            <Typography className={classes.title} variant='h5' noWrap>
                                PRTG customized App
                            </Typography>
                        </Grid>
                    </Grid>
                    <Grid container xs={8} spacing={1} item>
                        <Grid item xs={3}>
                            <Link to="/sensors" >
                                <Button className={classes.colorButton}>
                                    Sensors
                                </Button>
                            </Link>
                        </Grid>
                        <Grid item xs={3}>
                            <Link to="/contracts" >
                                <Button className={classes.colorButton}>
                                    Contracts
                                </Button>
                            </Link>
                        </Grid>
                        <Grid item xs={3}>
                            <Link to="/owners" >
                                <Button className={classes.colorButton}>
                                    Owners
                                </Button>
                            </Link>
                        </Grid>
                        <Grid item xs={1}>
                            {!isAuthenticated && (
                                <button onClick={() => loginWithRedirect({})} type="button">
                                Log in
                                </button>
                            )}
                            {isAuthenticated && (
                                <button onClick={() => logoutWithRedirect({})} type="button">
                                Log out
                                </button>
                            )}
                        </Grid>
                        <Grid item xs={1}>
                        </Grid>
                    </Grid>
                </Toolbar>
            </AppBar>
        </div>
    );
}
