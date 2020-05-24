import React, { Component } from 'react';

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

    renderForecastsTable(forecasts: any[]) {

        const items = forecasts.map(forecast => {
            return <p>{forecast.date}</p>;
        });

        return items;
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

        const addWeather = async() => {
            await fetch('/WeatherForecast/AddRandomWeather');
            await this.populateWeatherData();
        };

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}

                <button onClick={addWeather}>Add Random Weather</button>
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
