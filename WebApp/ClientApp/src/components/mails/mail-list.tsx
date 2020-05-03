import React from 'react';
import axios from 'axios';
import { useEffect } from 'react';
import { Mail } from './mail';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableContainer from '@material-ui/core/TableContainer';
import Paper from '@material-ui/core/Paper';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';

export interface IMailList {
    listOfMail: Mail[]
}

export interface MailListProps{
    show: boolean
}

const MailList = ({show}: MailListProps) => {
    const useStyles = makeStyles((theme: Theme) =>
        createStyles({
            titlesRow: {
                fontWeight: 'bold'
            },
            margins: {
                marginTop: '120px',
                marginLeft: '400px',
                marginRight: '400px',
                marginBottom: '120px',
            },
            buttonAdd: {
                float: 'right',
                backgroundColor: '#c62d1f',
                borderRadius: '18px',
                color: 'white',
                fontSize: '10px',
                fontWeight: 'bold',
                '&:hover': {
                    backgroundColor: 'red',
                }
            },
        })
    );
    const classes = useStyles();

    const [stateMail, setMail] = React.useState<IMailList>();

    const GetMails = async () => {
        // const response = await axios.get(`https://localhost:44370/api/Mails`);
        // setMail({ ...stateMail, listOfMail: response.data });
        setMail(stateMail, )
    };

    useEffect(() => { GetMails() }, []);
   
    return (
        <div className={classes.margins}>
            <Button className={classes.buttonAdd}>
                Add new E-Mail
            </Button>
            <TableContainer component={Paper}>
                <Table size='medium'>
                    <TableHead aria-label="simple table">
                        <TableRow>
                            <TableCell className={classes.titlesRow} size='medium'>E-mail adress</TableCell>
                            <TableCell className={classes.titlesRow} size='medium'></TableCell>
                            <TableCell className={classes.titlesRow} size='medium'></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {stateMail !== undefined && stateMail.listOfMail !== undefined ? stateMail.listOfMail.map(mail => (
                            <TableRow key={`${mail.mailId}`}>
                                <TableCell>{mail.adress}</TableCell>
                                <TableCell></TableCell>
                                <TableCell>
                                    <Button variant='contained' color='default'> <EditIcon /> </Button>
                                </TableCell>
                                <TableCell>
                                    <Button variant='contained' color='secondary'><DeleteIcon /></Button>
                                </TableCell>
                            </TableRow>
                        )) : null}
                    </TableBody>
                </Table>
            </TableContainer>
        </div>
    )
}

export default MailList
