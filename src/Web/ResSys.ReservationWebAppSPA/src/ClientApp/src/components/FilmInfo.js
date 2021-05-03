import React, { Component } from 'react';
import { Grid, Divider, Input } from 'semantic-ui-react'
import ReservationAPi from '../repositories/reservation';

export class FilmInfo extends Component
{
    static displayName = FilmInfo.name;


    render()
    {
        return (
            <Grid.Row key={this.props?.film.id} border={1}>
                <Grid.Column width={2}>
                    <Input
                        focus placeholder='Amount...'
                        value={this.props?.film.toReserve}
                        style={{ width: "5em" }}
                        onChange={(event) => this.props.updateFilm({ ...this.props.film, toReserve: event.target.value })}
                    />
                </Grid.Column>
                <Grid.Column width={2}>
                    <p>{this.props?.film.availableAmount === undefined ? this.props?.film.amount : this.props?.film.availableAmount}</p>
                </Grid.Column>
                <Grid.Column width={3}>
                    <p>{this.props?.film.name}</p>
                </Grid.Column>
                <Grid.Column width={3}>
                    <p>{this.props?.film.eidr}</p>
                </Grid.Column>
                <Grid.Column width={2}>
                    <p>{this.props?.film.rating}</p>
                </Grid.Column>
                <Grid.Column width={1}>
                </Grid.Column>
                <Grid.Column width={3}>
                    <p>{this.props?.film.publishDate}</p>
                </Grid.Column>
                <Grid.Column width={16}>
                    <Divider />
                </Grid.Column>
            </Grid.Row>
        );
    }
}
