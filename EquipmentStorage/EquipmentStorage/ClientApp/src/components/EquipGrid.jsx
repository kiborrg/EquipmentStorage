import React, { Component } from "react";
import DataGrid, { Column } from 'devextreme-react/data-grid';

import CustomStore from 'devextreme/data/custom_store';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import './EquipGrid.css';

export class EquipGrid extends Component {
    constructor(props) {
        super(props);

        this.getEquipment = this.getEquipment.bind(this);

        this.state = {
            parent: null
        }
    }

    componentDidUpdate() {
        if (this.state.parent !== this.props.parent) {
            this.setState({ parent: this.props.parent });
        }
    }

    get grid() {
        return this.gridRef.current.instance;
    }

    getEquipment() {
        return new CustomStore({
            key: "equipmentId",
            method: "GET",
            load: () => {
                if (this.state.parent !== null)
                    return fetch("api/Storage/GetEquipmentAsync?parentId=" + this.state.parent.locationId) 
                        .then(res => res.json());
            }
        });
    }

    render() {
        let type = this.state.parent === null ? null : this.state.parent.locationType.id;

        return (
            <DataGrid id="equipGrid"
                className="equip-grid"
                dataSource={this.state.parentId !== null ? this.getEquipment() : null}
            >
                <Column dataField="name" caption="Название" alignment="center" />
                <Column dataField="type.name" caption="Тип" alignment="center" />
                <Column dataField="count" caption="Количество" alignment="center" />
                {
                    type === null || type === 3 ? null : <Column dataField="room.name" caption="Местоположение" alignment="center" />
                }
            </DataGrid>
        );
    }
}