import React, { Component } from 'react';
import Moment from 'react-moment';
import moment from 'moment';

interface IState {
    isError: boolean;
    forecasts: any[],
    isLoading: boolean;
}

export class FetchData extends Component<{}, IState> {
    static displayName = FetchData.name;

    constructor(props: any) {
        super(props);
        this.state = { forecasts: [], isLoading: true, isError: false };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderForecastsTable(forecasts: any) {
        const dateFormatter = (colum: any, data: any) => {
            return moment(data.date).format('M/D/YYYY h A');
        };

        return (<p></p>
        );
    }

    render() {

        let contents = null;
        if (this.state.isError) {
            contents = (<strong>ERROR!</strong>);
        } else {
            contents = this.state.isLoading
                ? <p><em>Loading...</em></p>
                : FetchData.renderForecastsTable(this.state.forecasts);
        }

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        let data = Object.assign({}, this.state) as any;

        try {
            const response = await fetch('/WeatherForecast');
            data.forecasts = await response.json();
            data.isError = false;
        } catch (e) {
            console.error("Server Error", e);
            data.isError = true;
        } finally {
            data.isLoading = false;
            this.setState(data);
        }
    }
}
