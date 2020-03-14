import React, { Component } from 'react';
import { LocationsTree } from './components/LocationsTree.jsx';
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';

export default class App extends Component {

    render() {
        return (
            <LocationsTree />
        );
    }
}
