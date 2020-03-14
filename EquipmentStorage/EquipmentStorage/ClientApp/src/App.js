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
            parentId: null
        }
    }

    updateGrid(parentId) {
        this.setState({ parentId: parentId });
    }

    render() {
        return (
            <div className="main-class" >
                <LocationsTree updateGrid={this.updateGrid} />
                <EquipGrid parentId={this.state.parentId} />
            </div>
        );
    }
}
