import React, { Component } from "react";
import DataGrid, { Column } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import './EquipGrid.css';

export class EquipGrid extends Component {
    constructor(props) {
        super(props);

        this.getEquipment = this.getEquipment.bind(this);

        this.state = {
            parentId: null
        }
    }

    componentDidMount() {
        this.setState({ parentId: this.props.parentId });
        console.log(this.state.parentId);
    }

    get grid() {
        return this.gridRef.current.instance;
    }

    getEquipment() {
        return Promise.resolve(fetch("api/Storage/GetLocationsAsync?parentId=" + this.state.parentId)
            .then(res => res.json()));
    }

    render() {
        return (
            <DataGrid id="equipGrid"
                className="equip-grid"
                dataSource={this.state.parentId !== null ? this.getEquipment() : null}
            >
                <Column dataField="name" caption="Название" />
                <Column dataField="type.name" caption="Тип" />
                <Column dataField="count" caption="Количество" />
            </DataGrid>
        );
    }
}