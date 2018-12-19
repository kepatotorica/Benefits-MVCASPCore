const { PieChart, Pie, Sector, Cell } = Recharts;
const data = [{ name: 'Group A', value: 400 }, { name: 'Group B', value: 300 },
{ name: 'Group C', value: 300 }, { name: 'Group D', value: 200 }];
const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];
const RADIAN = Math.PI / 180;

var React = require('react');
var SimpleLineChart = require('create-react-class');
import { render } from 'react-dom';

const SimplePieChart = () => (
            <PieChart width={400} height={200} /*onMouseEnter={this.onPieEnter}*/>
                <Pie
                    data={data}
                    cx={60}
                    cy={100}
                    innerRadius={30}
                    outerRadius={40}
                    fill="#8884d8"
                    paddingAngle={5}
                >
                    {
                        data.map((entry, index) => <Cell fill={COLORS[index % COLORS.length]} />)
                    }
                </Pie>
            </PieChart>
);

ReactDOM.render(
    <SimplePieChart />,
    document.getElementById('container')
);



//data is in Users/Details.cshtml

//const { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } = Recharts;
//var React = require('react');
//var SimpleLineChart = require('create-react-class');
//import { render } from 'react-dom';

//const SimpleLineChart = () => (
//    <LineChart width={600} height={300} data={data}
//        margin={{ top: 5, right: 30, left: 20, bottom: 5 }}>
//        <XAxis dataKey="name" />
//        <YAxis />
//        <CartesianGrid strokeDasharray="3 3" />
//        <Tooltip />
//        <Legend />
//        <Line type="monotone" dataKey="pv" stroke="#8884d8" activeDot={{ r: 8 }} />
//        <Line type="monotone" dataKey="uv" stroke="#82ca9d" />
//    </LineChart>
//);

//render(
//    <SimpleLineChart />,
//    document.getElementById('container')
//);
