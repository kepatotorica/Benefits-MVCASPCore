//npx babel--watch. --out - dir wwwroot / dist--presets react - app / prod
//from root 

var e = React.createElement;

class HelloWorld extends React.Component {
    render() {
        return e(
            "div",
            null,
            "Hello World"
        );
    }
}

const containerElement = document.getElementById('chart');
ReactDOM.render(e(HelloWorld), containerElement);