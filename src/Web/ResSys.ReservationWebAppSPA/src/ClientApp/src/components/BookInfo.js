import React, { Component } from 'react';
import { Grid, Divider, Input } from 'semantic-ui-react'
import ReservationAPi from '../repositories/reservation';

export class BookInfo extends Component
{
    static displayName = BookInfo.name;


    render()
    {
        return (
            <Grid.Row key={this.props?.book.id} border={1}>
                <Grid.Column width={2}>
                    <Input
                        focus placeholder='Amount...'
                        value={this.props?.book.toReserve}
                        style={{ width: "5em" }}
                        onChange={(event) => this.props.updateBook({ ...this.props.book, toReserve: event.target.value })}
                    />
                </Grid.Column>
                <Grid.Column width={2}>
                    <p>{this.props.book?.availableAmount === undefined ? this.props.book?.amount : this.props.book?.availableAmount}</p>
                </Grid.Column>
                <Grid.Column width={3}>
                    <p>{this.props.book?.name}</p>
                </Grid.Column>
                <Grid.Column width={3}>
                    <p>{this.props.book?.iban}</p>
                </Grid.Column>
                <Grid.Column width={2}>
                    <p>{this.props.book?.numberOfPages}</p>
                </Grid.Column>
                <Grid.Column width={1}>
                </Grid.Column>
                <Grid.Column width={3}>
                    <p>{this.props.book.publishDate}</p>
                </Grid.Column>
                <Grid.Column width={16}>
                    <Divider />
                </Grid.Column>
            </Grid.Row>
        );
    }
}
