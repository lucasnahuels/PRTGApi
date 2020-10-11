import React from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableContainer from '@material-ui/core/TableContainer';
import { Grid, Paper, TableFooter, Select } from '@material-ui/core';
import register from '../../../registerServiceWorker';
import { Link } from 'react-router-dom';
import MenuItem from '@material-ui/core/MenuItem';

const PricesList = (deviceObjId: number) => {
    const useStyles = makeStyles((theme: Theme) =>
      createStyles({
        margins: {
          paddingTop: "70px",
          paddingBottom: "120px",
        },
        titlesRow: {
          fontWeight: "bold",
          textAlign: "center",
        },
        dataRow: {
          textAlign: "center",
        },
        formRoot: {
          margin: theme.spacing(1),
          width: "10ch",
        },
        buttonBack: {
          position: "absolute",
          left: "45%",
          right: "45%",
          backgroundColor: "blue",
          borderRadius: "18px",
          color: "white",
          fontSize: "10px",
          fontWeight: "bold",
          "&:hover": {
            backgroundColor: "lightblue",
          },
        },
      })
    );
    const classes = useStyles();
    const [selectedMonthValue, setSelectedMonthValue] = React.useState((new Date().getMonth() + 1).toString());
    const [selectedYearValue, setSelectedYearValue] = React.useState((new Date().getFullYear()).toString());
    const MonthList = () : string[] => {
      let monthList: string[] = [];
      let i: number;
      for (i = 1; i <= 12; i++) {
        monthList.push(i.toString());
      }
      return monthList;
    }; 
    const YearList = () : string[] => {
      let yearList: string[] = [];
      let i: number;
      for (i = 2020; i <= 2050; i++) {
        yearList.push(i.toString());
      }
      return yearList;
    }; 

    const HandleChangeMonth = (event: React.ChangeEvent<{ value: unknown }>) => {
        setSelectedMonthValue(event.target.value as string);
    };
    const HandleChangeYear = (event: React.ChangeEvent<{ value: unknown }>) => {
      setSelectedYearValue(event.target.value as string);
    };

  return (
    <div className={classes.margins}>
      <Grid container xs={12} item>
        <Grid item xs={3}></Grid>
        <Grid item xs={6}>
          <h5 className={classes.titlesRow}>Calculated prices for this device in the actual month with the values of this contract</h5>
          <br/>
          <TableContainer component={Paper}>
            <Table size="medium">
              <TableHead aria-label="simple table">
                <TableRow>
                  <TableCell className={classes.titlesRow} size="medium">
                    Device
                  </TableCell>
                  {/* <TableCell className={classes.titlesRow} size="medium">
                    Year
                  </TableCell>
                  <TableCell className={classes.titlesRow} size="medium">
                    Month
                  </TableCell> */}
                  <TableCell className={classes.titlesRow} size="medium">
                    Spent in black and white sheets
                  </TableCell>
                  <TableCell className={classes.titlesRow} size="medium">
                    Spent in color sheets
                  </TableCell>
                  <TableCell className={classes.titlesRow} size="medium">
                    Total spent
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                <TableRow>
                  {" "}
                  {/*do not forget key */}
                  <TableCell className={classes.dataRow}></TableCell>
                  {/* <TableCell className={classes.dataRow}>
                    <Select
                      className={classes.formRoot}
                      id="inputYear"
                      name="year"
                      inputRef={register}
                      value={selectedYearValue}
                      onChange={HandleChangeYear}
                    >
                      {
                        YearList().map(year =>
                          <MenuItem key={year} value={year}>{year}</MenuItem>
                        )
                      }
                    </Select>
                  </TableCell>
                  <TableCell className={classes.dataRow}>
                    <Select
                      className={classes.formRoot}
                      id="inputMonth"
                      name="month"
                      inputRef={register}
                      value={selectedMonthValue}
                      onChange={HandleChangeMonth}
                    >
                        {
                        MonthList().map(month =>
                            <MenuItem key={month} value={month}>{month}</MenuItem>
                        ) 
                        }
                    </Select>
                  </TableCell> */}
                  <TableCell className={classes.dataRow}></TableCell>
                  <TableCell className={classes.dataRow}></TableCell>
                  <TableCell className={classes.dataRow}></TableCell>
                </TableRow>
              </TableBody>
              <TableFooter>
                <TableRow></TableRow>
              </TableFooter>
            </Table>
          </TableContainer>
          <br />
          <br />
          <Link to="/devices">
            <Button className={classes.buttonBack}>Back</Button>
          </Link>
        </Grid>
        <Grid item xs={3}></Grid>
      </Grid>
    </div>
  );
}

export default PricesList;