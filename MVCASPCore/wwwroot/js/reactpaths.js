//npx babel --watch wwwroot/js --out-dir wwwroot/dist --presets react-app/prod 
//from root
//import React from "react";
import Pie from 'react-pathjs-chart';
//var Pie = require('react-pathjs-chart.Pie');

var c = React.createElement;

class PieChart extends React.Component {
    render() {
        return c(
            <Pie data={data} options={options} accessorKey="population" />
        );
    }
}

const ce = document.getElementById('content');
ReactDOM.render(c(PieChart), ce);