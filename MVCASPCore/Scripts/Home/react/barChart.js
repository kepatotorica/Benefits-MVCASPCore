//data is in Users/Details.cshtml
//this gets put into bundle.js then it is included in the shared layout file
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
