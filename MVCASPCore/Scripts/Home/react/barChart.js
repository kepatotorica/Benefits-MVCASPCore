//data is in Users/Details.cshtml
const { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend } = Recharts;

var React = require('react');

const StackedBarChart = () =>
(
    <BarChart width={600} height={400} data={data}
        margin={{ top: 0, right: 0, left: 0, bottom: 0 }}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="name" />
        <YAxis />
        <Tooltip />
        <Legend />
            <Bar dataKey="Paycheck" stackId="a" fill="#1d993e" />
            <Bar dataKey="Deductions" stackId="a" fill="#991d1d" />
            <Bar dataKey="Benefits" fill="#1d6d99" />
    </BarChart>
)

ReactDOM.render(
    <StackedBarChart />,
    document.getElementById('container')
);










//const { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } = Recharts;
//var React = require('react');
//var SimpleLineChart = require('create-react-class');
//import { render } from 'react-dom';

//const SimpleLineChart = () => (
//    <LineChart width={600} height={400} data={data}
//        margin={{ top: 0, right: 0, left: 0, bottom: 0 }}>
//        <XAxis dataKey="name" />
//        <YAxis />
//        <CartesianGrid strokeDasharray="3 3" />
//        <Tooltip />
//        <Legend />
//        <Line type="monotone" dataKey="paycheck" stroke="#8884d8" activeDot={{ r: 8 }} />
//        <Line type="monotone" dataKey="benefits" stroke="#82ca9d" />
//    </LineChart>
//);

//render(
//    <SimpleLineChart />,
//    document.getElementById('container')
//);



//const { PieChart, Pie, Sector, Cell } = Recharts;
////const data = [{ name: 'Group A', value: 400 }, { name: 'Group B', value: 300 },
////{ name: 'Group C', value: 300 }, { name: 'Group D', value: 200 }];
//const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];
//const RADIAN = Math.PI / 180;

//var React = require('react');
//var SimpleLineChart = require('create-react-class');
//import { render } from 'react-dom';

//const SimplePieChart = () => (
//            <PieChart width={400} height={200} /*onMouseEnter={this.onPieEnter}*/>
//                <Pie
//                    data={data}
//                    cx={60}
//                    cy={100}
//                    innerRadius={30}
//                    outerRadius={40}
//                    fill="#8884d8"
//                    paddingAngle={5}
//                >
//                    {
//                        data.map((entry, index) => <Cell fill={COLORS[index % COLORS.length]} />)
//                    }
//                </Pie>
//            </PieChart>
//);

//ReactDOM.render(
//    <SimplePieChart />,
//    document.getElementById('container')
//);


