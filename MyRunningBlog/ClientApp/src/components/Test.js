import React, { Component } from 'react';

export class Test extends Component {
    static displayName = Test.name;

    constructor() {
        super();
        this.state = { testData: [], loading: false };

        fetch('api/Test/GetTestData')
            .then(response => response.json())
            .then(data => {
                this.setState({ testData: data, loading: false });
            });
    }

    static createTestDataTable(testData) {
        return (
            <table>
                <thead>
                    <tr>
                        <th> Test Number </th>
                        <th> Test Sentence </th>
                    </tr>
                </thead>
                <tbody>
                    {testData.map(test =>
                        <tr>
                            <td>{test.number}</td>
                            <td>{test.sentence}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = Test.createTestDataTable(this.state.testData);
        return (
            <div>
                {contents}
            </div>
        )
    }
}
