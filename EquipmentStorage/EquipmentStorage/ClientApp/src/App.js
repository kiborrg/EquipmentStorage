import React, { Component } from 'react';
import { LocationsTree } from './components/LocationsTree.jsx';
import { EquipGrid } from './components/EquipGrid.jsx';
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';

export default class App extends Component {
    constructor(props) {
        super(props);
        
        this.updateGrid = this.updateGrid.bind(this);

        this.state = {
            parent: null,
            refresh: false
        }
    }

    updateGrid(item) {
        this.setState({ parent: item });
    }

    testRefresh() {
        this.setState({ refresh: true });
        this.setState({ refresh: false });
    }

    render() {
        return (
            <div className="main-class" >
                <LocationsTree updateGrid={this.updateGrid} refresh={this.state.refresh} />
                <EquipGrid onRefresh={() => { this.testRefresh() }} parent={this.state.parent} />
            </div>
        );
    }
}
