import React, { Component } from 'react';
import { Container, Grid, Divider, Icon, Button, Input } from 'semantic-ui-react'
import LogisticApi from '../repositories/logistic';

export class NewFilm extends Component
{
    static displayName = NewFilm.name;

    render()
    {
        return (
            <Grid.Row key={this.props?.film.id} border={1}>
                <Grid.Column width={2}>
                    <Input
                        focus placeholder='Amount...'
                        value={this.props?.film.amount}
                        type="number"
                        style={{ width: "5em" }}
                        onChange={(event) => this.props.updateFilm({ ...this.props.film, amount: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={3}>
                    <Input
                        focus placeholder='Name...'
                        value={this.props?.film.name}
                        onChange={(event) => this.props.updateFilm({ ...this.props.film, name: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={3}>
                    <Input
                        focus placeholder='EIDR...'
                        value={this.props?.film.EIDR}
                        onChange={(event) => this.props.updateFilm({ ...this.props.film, EIDR: event.target.value })}
                    />
                </Grid.Column>
                <Grid.Column width={2}>
                    <Input
                        focus placeholder='Rating...'
                        value={this.props?.film.rating}
                        type="number"
                        style={{ width: "5em" }}
                        onChange={(event) => this.props.updateFilm({ ...this.props.film, rating: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={1}>
                </Grid.Column>
                <Grid.Column width={2}>
                    <Input focus
                        placeholder='Author...'
                        value={this.props?.film.authorRegNum}
                        type="number"
                        style={{ width: "5em" }}
                        onChange={(event) => this.props.updateFilm({ ...this.props.film, authorRegNum: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={3}>
                    <Input
                        focus placeholder='Published...'
                        value={this.props?.film.publishDate}
                        style={{ width: "11em" }}
                        type="date"
                        onChange={(event) => this.props.updateFilm({ ...this.props.film, publishDate: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={16}>
                    <Divider />
                </Grid.Column>
            </Grid.Row>
        );
    }
}
