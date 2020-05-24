import React, { Component } from 'react';

interface IState {
    isError: boolean;
    forecasts: any[],
    isLoading: boolean;
    isDataModifying: boolean;
}

export class FetchData extends Component<{}, IState> {
    static displayName = FetchData.name;

    constructor(props: any) {
        super(props);
        this.state = { forecasts: [], isLoading: true, isError: false, isDataModifying: false };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    renderForecastsTable(forecasts: any[]) {

        let item = null;
        if (forecasts.length > 0) {
            item = forecasts.map(forecast => {
                return <p>{forecast.date}</p>;
            });
        } else {
            item = (<strong>No items found</strong>);
        }

        return item;
    }

    render() {

        let contents = null;
        if (this.state.isError) {
            contents = (<strong>ERROR!</strong>);
        } else {
            contents = this.state.isLoading
                ? <p><em>Loading...</em></p>
                : this.renderForecastsTable(this.state.forecasts);
        }

        const addWeatherHandle = async () => {
            let data = Object.assign({}, this.state) as any;
            try {
                data.isDataModifying = true;
                this.setState(data);

                await fetch('/WeatherForecast/AddRandomWeather');
            } finally {
                data.isDataModifying = false;
                this.setState(data);
            }
            await this.populateWeatherData();
        };

        const deleteAllWeatherHandle = async () => {
            let data = Object.assign({}, this.state) as any;
            try {
                data.isDataModifying = true;
                this.setState(data);

                await fetch('/WeatherForecast/DeleteAllWeather');
            } finally {
                data.isDataModifying = false;
                this.setState(data);
            }
            await this.populateWeatherData();
        };

        let buttons = (<div>
            <button onClick={addWeatherHandle}>Add Random Weather</button>
            <button onClick={deleteAllWeatherHandle}>Delete All Weather Data</button>
        </div>);

        if (this.state.isDataModifying) {
            buttons = (<div>
                <button onClick={addWeatherHandle} disabled>Add Random Weather</button>
                <button onClick={deleteAllWeatherHandle} disabled>Delete All Weather Data</button>
            </div>);
        }

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}

                {buttons}
            </div>
        );
    }

    async populateWeatherData() {
        let data = Object.assign({}, this.state) as any;

        // Clear current stuff
        data.isError = false;
        data.isLoading = true;
        data.forecasts = [];
        this.setState(data);

        // Get data from server
        try {
            const response = await fetch('/WeatherForecast/GetWeatherForecast');
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
