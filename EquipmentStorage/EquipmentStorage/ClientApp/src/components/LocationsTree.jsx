import React, { Component } from 'react';
import { TreeView } from 'devextreme-react';
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import './LocationsTree.css';

export class LocationsTree extends Component {
    constructor(props) {
        super(props);

        fetch("api/Storage/GetLocationsAsync")
            .then(res => res.json())
            .then(data => {
                this.setState({ locations: data, isLoaded: true })
            });

        this.state = {
            locations: [],
            isLoaded: false
        };
    }

    render() {
        if (!this.state.isLoaded) {
            return (
                <h5 className="locations-tree">Загрузка дерева местоположений</h5>
            );
        }
        else {
            return (
                <TreeView id="locationsTree"
                    className="locations-tree"
                    dataSource={this.state.locations}
                    dataStructure="plain"
                    displayExpr="name"
                    keyExpr="locationId"
                    parentIdExpr="parentId"
                />
            );
        }
    }
}
