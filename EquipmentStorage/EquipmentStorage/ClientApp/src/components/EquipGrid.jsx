import React, { Component } from "react";
import DataGrid, { Column, Editing, Lookup, HeaderFilter } from 'devextreme-react/data-grid';

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
            },
            update: (key, values) => {
                console.log(values.type);
                return fetch("api/Storage/UpdateEquip", {
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(Object.assign({
                        equipmentId: key,
                        roomId: this.state.parent.locationId,
                        type: values.type === undefined ? null : {
                            id: values.type.id,
                            name: values.type.name
                        },
                        count: values.count === undefined ? -1 : values.count,
                        name: values.name === undefined ? null : values.name
                    }))
                });
            },
            remove: (key) => {
                return fetch("api/Storage/DeleteEquip?equipId=" + key, {
                    method: "DELETE"
                });
            },
            insert: (values) => {
                return fetch("api/Storage/InsertEquip", {
                    method: "PUT",
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(Object.assign({
                        equipmentId: -1,
                        roomId: this.state.parent.locationId,
                        type: {
                            id: values.type.id,
                            name: values.type.name
                        },
                        count: values.count,
                        name: values.name
                    }))
                });
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