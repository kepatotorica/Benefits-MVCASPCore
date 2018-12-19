
//data is in Users/Details.cshtml

const { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } = Recharts;
var React = require('react');
var SimpleLineChart = require('create-react-class');
import { render } from 'react-dom';

const SimpleLineChart = () => (
    <LineChart width={600} height={300} data={data}
        margin={{ top: 5, right: 30, left: 20, bottom: 5 }}>
        <XAxis dataKey="name" />
        <YAxis />
        <CartesianGrid strokeDasharray="3 3" />
        <Tooltip />
        <Legend />
        <Line type="monotone" dataKey="pv" stroke="#8884d8" activeDot={{ r: 8 }} />
        <Line type="monotone" dataKey="uv" stroke="#82ca9d" />
    </LineChart>
);

render(
    <SimpleLineChart />,
    document.getElementById('container')
);

//import React, { Fragment } from 'react';
//import { render } from 'react-dom';

//const App = () => (
//    <Fragment>
//        <h1>React works in ASP.NET MVC! Yes!</h1>
//        <div>Hello React World!!xxx!</div>
//    </Fragment>
//);


//render(<App />, document.getElementById('app'));