import React, { Component } from 'react';
import { Container, Grid, Divider, Icon, Button, Input } from 'semantic-ui-react'
import LogisticApi from '../repositories/logistic';

export class NewBook extends Component
{
    static displayName = NewBook.name;

    render()
    {
        return (
            <Grid.Row key={this.props?.book.id} border={1}>
                <Grid.Column width={2}>
                    <Input
                        focus placeholder='Amount...'
                        value={this.props?.book.amount}
                        type="number"
                        style={{ width: "5em" }}
                        onChange={(event) => this.props.updateBook({ ...this.props.book, amount: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={3}>
                    <Input
                        focus placeholder='Name...'
                        value={this.props?.book.name}
                        onChange={(event) => this.props.updateBook({ ...this.props.book, name: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={3}>
                    <Input
                        focus placeholder='IBAN...'
                        value={this.props?.book.IBAN}
                        onChange={(event) => this.props.updateBook({ ...this.props.book, IBAN: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={2}>
                    <Input
                        focus placeholder='Page count...'
                        value={this.props?.book.numberOfPages}
                        type="number"
                        style={{ width: "5em" }}
                        onChange={(event) => this.props.updateBook({ ...this.props.book, numberOfPages: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={1}>
                </Grid.Column>
                <Grid.Column width={2}>
                    <Input
                        focus placeholder='Author...'
                        value={this.props?.book.authorRegNum}
                        type="number"
                        style={{ width: "5em" }}
                        onChange={(event) => this.props.updateBook({ ...this.props.book, authorRegNum: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={3}>
                    <Input
                        focus placeholder='Published...'
                        value={this.props?.book.publishDate}
                        type="date"
                        style={{ width: "11em" }}
                        onChange={(event) => this.props.updateBook({ ...this.props.book, publishDate: event.target.value })} />
                </Grid.Column>
                <Grid.Column width={16}>
                    <Divider />
                </Grid.Column>
            </Grid.Row>
        );
    }
}
