import React, { Component } from 'react';

export default class Services extends Component {
    constructor(props) {
        super(props);

        this.baseUrl = "api/Storage/";
    }

    getLocations() {
        return fetch("api/Storage/GetLocationsAsync", {
            method: "GET"
        })
        .then(res => res.json())
    }

    getEquipment(parentId) {
        return fetch(`${this.baseUrl}GetEquipmentAsync?parentId=` + parentId, {
            method: "GET"
        })
        .then(res => res.json());
    }

    updateEquip(key, values, roomId) {
        return fetch(`${this.baseUrl}UpdateEquip`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(Object.assign({
                equipmentId: key,
                roomId: roomId,
                type: values.type === undefined ? null : {
                    id: values.type.id,
                    name: values.type.name
                },
                count: values.count === undefined ? -1 : values.count,
                name: values.name === undefined ? null : values.name
            }))
        });
    }

    deleteEquip(key) {
        return fetch(`${this.baseUrl}DeleteEquip?equipId=` + key, {
            method: "DELETE"
        });
    }

    insertEquip(values, roomId) {
        return fetch(`${this.baseUrl}InsertEquip`, {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(Object.assign({
                equipmentId: -1,
                roomId: roomId,
                type: {
                    id: values.type.id,
                    name: values.type.name
                },
                count: values.count,
                name: values.name
            }))
        });
    }
}