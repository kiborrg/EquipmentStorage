import React, { Component } from "react";
import DataGrid, { Column, Editing, Lookup, HeaderFilter } from 'devextreme-react/data-grid';

import CustomStore from 'devextreme/data/custom_store';
import Services from './Services.js'

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import './EquipGrid.css';

export class EquipGrid extends Component {
    constructor(props) {
        super(props);

        console.log(props);

        this.getEquipment = this.getEquipment.bind(this);

        this.gridRef = React.createRef();

        this.service = new Services(props);

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

    refresh() {
        this.grid.refresh();
    }

    getEquipment() {
        return new CustomStore({
            key: "equipmentId",
            method: "GET",
            load: () => {
                if (this.state.parent !== null)
                    return this.service.getEquipment(this.state.parent.locationId);
            },
            update: (key, values) => {
                return this.service.updateEquip(key, values, this.state.parent.locationId)
                    .then(() => this.props.onRefresh());
            },
            remove: (key) => {
                return this.service.deleteEquip(key)
                    .then(() => this.props.onRefresh());
            },
            insert: (values) => {
                return this.service.insertEquip(values, this.state.parent.locationId)
                    .then(() => this.props.onRefresh());
            }
        });
    }

    getEquipTypes() {
        return new CustomStore({
            key: "id",
            loadMode: "raw",
            method: "GET",
            load: () => {
                return fetch("api/Storage/GetEquipTypes")
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
                ref={this.gridRef}
            >
                <HeaderFilter visible={true} />
                <Column dataField="name" caption="Название" alignment="center" allowFiltering={true} />
                <Column dataField="type.id" caption="Тип" alignment="center" allowFiltering={true} >
                    <Lookup dataSource={this.getEquipTypes} displayExpr="name" valueExpr="id" />
                </Column>
                <Column dataField="count" caption="Количество" alignment="center" allowHeaderFiltering={false} />
                {
                    type === null || type === 3 ? null :
                        <Column dataField="room.name" caption="Месторасположение" alignment="center" allowFiltering={true} />
                }
                <Editing
                    mode="row"
                    allowAdding={type === 3 ? true : false}
                    allowDeleting={type === 3 ? true : false}
                    allowUpdating={type === 3 ? true : false}
                />
            </DataGrid>
        );
    }
}