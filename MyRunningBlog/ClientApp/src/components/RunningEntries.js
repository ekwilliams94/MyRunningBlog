import React, { Component } from 'react';

export class RunningEntries extends Component {
    constructor(props) {
        super(props);
        this.convertRunningEntryDateTimeToDate = this.convertRunningEntryDateTimeToDate.bind(this);
        this.convertDistanceIntoMiles = this.convertDistanceIntoMiles.bind(this);
        this.convertPaceIntoMinutesPerMile = this.convertPaceIntoMinutesPerMile.bind(this);
        this.convert = this.convert.bind(this);

        this.state = {
            runningEntries: [], loading: true
        };

        fetch('api/RunningEntry/GetRuningEntries')
            .then(response => response.json())
            .then(data => {
                this.setState({ runningEntries: data, loading: false });
            });
    }

    convertRunningEntryDateTimeToDate(runningEntries) {
        runningEntries.map(runningEntry => {
            runningEntry.date = new Date(runningEntry.date).toDateString();
        })
    }

    convertRunningTime(runningEntries) {
        runningEntries.map(runningEntry => {
            var hours = Math.floor(runningEntry.runningTime / 3600);
            (hours >= 1) ? runningEntry.runningTime = runningEntry.runningTime - (hours * 3600) : hours = '00';
            var min = Math.floor(runningEntry.runningTime / 60);
            (min >= 1) ? runningEntry.runningTime = runningEntry.runningTime - (min * 60) : min = '00';
            (runningEntry.runningTime < 1) ? runningEntry.runningTime = '00' : void 0;

            (min.toString().length == 1) ? min = '0' + min : void 0;
            (runningEntry.runningTime.toString().length == 1) ? runningEntry.runningTime = '0' + runningEntry.runningTime : void 0;

            runningEntry.runningTime = hours + ':' + min + ':' + runningEntry.runningTime;
        })
    }

    convert(runningPace) {
        var hours = Math.floor(runningPace / 3600);
        (hours >= 1) ? runningPace = runningPace - (hours * 3600) : hours = '00';
        var min = Math.floor(runningPace / 60);
        (min >= 1) ? runningPace = runningPace - (min * 60) : min = '00';
        (runningPace < 1) ? runningPace = '00' : void 0;

        (min.toString().length == 1) ? min = '0' + min : void 0;
        (runningPace.toString().length == 1) ? runningPace = '0' + runningPace : void 0;

        return hours + ':' + min + ':' + runningPace;
    }

    convertDistanceIntoMiles(runningEntries) {
        runningEntries.map(runningEntry => {
            runningEntry.runningDistance = runningEntry.runningDistance * 0.000621371;
        })
    }

    convertPaceIntoMinutesPerMile(runningEntries) {
        runningEntries.map(runningEntry => {
            runningEntry.runningPace = 26.8224 / runningEntry.runningPace;
        })
    }

    createRunningEntriesTable = (runningEntries) => {
        this.convertRunningEntryDateTimeToDate(runningEntries);
        this.convertRunningTime(runningEntries);
        this.convertDistanceIntoMiles(runningEntries);
        this.convertPaceIntoMinutesPerMile(runningEntries);
        //this.convertPaceIntoMinutesPerMile(runningEntries);
        return (
            <table className='table table-striped' >
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Time</th>
                        <th>Distance</th>
                        <th>Pace</th>
                    </tr>
                </thead>
                <tbody>
                    {runningEntries.map(runningEntry =>
                        <tr>
                            <td>{runningEntry.date.toString()}</td>
                            <td>{runningEntry.runningTime}</td>
                            <td>{runningEntry.runningDistance}</td>
                            <td>{runningEntry.runningPace}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading ... </em></p>
            : this.createRunningEntriesTable(this.state.runningEntries);

        return (
            <div>
                {contents}
            </div>
        )
    }


}
