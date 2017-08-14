import React from 'react';
import config from './appconfig.js';
import Griddle from 'griddle-react';
import ReactTable from 'react-table'
import 'react-table/react-table.css'
import '../styles/index.scss';

<script src="https://unpkg.com/react-table@latest/react-table.js"></script>

export default class App extends React.Component {
  state = {
    result: [],
    loaded: false
  };

  componentDidMount() {
    this.getData();
  }

  getData = () => {
    fetch(`${config.root}/employee`, {
      method: 'GET',
      headers: {
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': 'true',
      },
    })
      .then((response) => response.json())
      .then((responseJson) => {
        this.setState({ result: responseJson, loaded: true });
      })
      .catch((error) => {
        console.error(error);
      });
  }

  constructor() {
    super();
  }

  render() {

    const columns = [{
      Header: 'First Name',
      accessor: 'FirstName'
    }, {
      Header: 'Last Name',
      accessor: 'LastName',
    }, {
      Header: 'Title',
      accessor: 'Title'
    },{
      Header: 'Products Sold',
      accessor: 'Orders'
    },]

    if (this.state.loaded == false) {
      return <div>Loading...</div>
    }
    else {
      return (
        <div>
          <h1>Employees</h1>
          <ReactTable
            data={this.state.result}
            columns={columns}
            defaultPageSize={5} 
          />
        </div>
      )
    }
  }
}
