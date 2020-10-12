import React, { useEffect } from 'react';
import useApi from '../../../helpers/axios-wrapper'
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
import { myConfig } from '../../../configurations';
import { PricesData } from './price-view-model';

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
    // const [selectedMonthValue, setSelectedMonthValue] = React.useState((new Date().getMonth() + 1).toString());
    // const [selectedYearValue, setSelectedYearValue] = React.useState((new Date().getFullYear()).toString());
    // const MonthList = () : string[] => {
    //   let monthList: string[] = [];
    //   let i: number;
    //   for (i = 1; i <= 12; i++) {
    //     monthList.push(i.toString());
    //   }
    //   return monthList;
    // }; 
    // const YearList = () : string[] => {
    //   let yearList: string[] = [];
    //   let i: number;
    //   for (i = 2020; i <= 2050; i++) {
    //     yearList.push(i.toString());
    //   }
    //   return yearList;
    // }; 

    // const HandleChangeMonth = (event: React.ChangeEvent<{ value: unknown }>) => {
    //     setSelectedMonthValue(event.target.value as string);
    // };
    // const HandleChangeYear = (event: React.ChangeEvent<{ value: unknown }>) => {
    //   setSelectedYearValue(event.target.value as string);
    // };
  const [deviceId, setDeviceId] = React.useState("");
  const [contractId, setContractId] = React.useState("");
  const [pricesData, setPricesData] = React.useState<PricesData>({
    deviceId: 0,
    blackAndWhiteCopiesPrices: 0,
    colorCopiesPrices: 0,
    totalCopiesPrices: 0
  })

  const axios = useApi();

  function getQueryVariable(variable: string) {
    var query = window.location.search.substring(1);//"app=article&act=news_content&aid=160990"
    var vars = query.split("&");//[ 'app=article', 'act=news_content', 'aid=160990' ]
    for (var i = 0; i < vars.length; i++) {
      var pair = vars[i].split("=");//[ 'app', 'article' ][ 'act', 'news_content' ][ 'aid', '160990' ] 
      if (pair[0] === variable) { return pair[1]; }
    }
    return "";
  }

  useEffect(() => {
    setContractId(getQueryVariable("contractId"));
    setDeviceId(getQueryVariable("deviceObjId"));
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [contractId, deviceId]);

  useEffect(() => {
    CalculatePrices()
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  
  const CalculatePrices = async () => {
    let contractId: string = getQueryVariable("contractId");
    let deviceId: string = getQueryVariable("deviceObjId"); 
    await axios.get(`contract/CalculatePrices/` + contractId + "/" + deviceId).then((response) => {
      setPricesData( response.data );
    });
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
                <TableRow key={pricesData!.deviceId}>
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
                  <TableCell className={classes.dataRow}>{pricesData!.blackAndWhiteCopiesPrices}</TableCell>
                  <TableCell className={classes.dataRow}>{pricesData!.colorCopiesPrices}</TableCell>
                  <TableCell className={classes.dataRow}>{pricesData!.totalCopiesPrices}</TableCell>
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