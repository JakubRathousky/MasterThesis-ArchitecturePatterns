import React, { Component } from 'react';
import { Container, Grid, Divider, Icon, Button, Input } from 'semantic-ui-react'
import LogisticApi from '../repositories/logistic';
import { NewBook } from './NewBook'
import { NewFilm } from './NewFilm'

import { Redirect } from 'react-router';

export class NewSupply extends Component
{
    static displayName = NewSupply.name;

    componentDidMount = () =>
    {
        this.setState({ films: [], books: [] });
    }

    postSupply = () =>
    {
        LogisticApi.saveSupply(this.state.films, this.state.books).then(result =>
        {
            this.setState({ supplyUploaded: true });
        })
    }

    newFilm = () =>
    {
        return {
            id: "film - " + this.state.films.length,
            name: "",
            EIDR: "",
            rating: 0,
            publishDate: "",
            authorRegNum: 0,
            amount: 0
        }
    }
    createNewBook = () =>
    {
        return {
            id: "book - " + this.state.books.length,
            name: "",
            IBAN: "",
            numberOfPages: 0,
            publishDate: "",
            authorRegNum: 0,
            amount: 0
        }
    }
    updateBook = (updatedBook) =>
    {
        let books = [...this.state.books]
        let book = books.find(x => x.id === updatedBook.id);
        let index = books.map(function (x) { return x.id; }).indexOf(book.id);
        console.log(updatedBook, index);
        if (book && index >= 0)
        {
            book.name = updatedBook.name;
            book.IBAN = updatedBook.IBAN;
            book.numberOfPages = updatedBook.numberOfPages;
            book.publishDate = updatedBook.publishDate;
            book.authorRegNum = updatedBook.authorRegNum;
            book.amount = updatedBook.amount;
            books[index] = book;
            this.setState({ books: books })
        }
    }
    updateFilm = (updatedFilm) =>
    {
        let films = [...this.state.films]
        let film = films.find(x => x.id === updatedFilm.id);
        let index = films.map(function (x) { return x.id; }).indexOf(film.id);
        if (film && index >= 0)
        {
            film.name = updatedFilm.name;
            film.EIDR = updatedFilm.EIDR;
            film.rating = updatedFilm.rating;
            film.publishDate = updatedFilm.publishDate;
            film.authorRegNum = updatedFilm.authorRegNum;
            film.amount = updatedFilm.amount;
            films[index] = film;
            this.setState({ films: films })
        }
    }
    renderFilms = () =>
    {
        return (
            this.state?.films?.map(x =>
                <NewFilm key={x.id} film={x} updateFilm={this.updateFilm} />
            )
        )
    }
    renderBooks = () =>
    {
        return (
            this.state?.books?.map(x =>
                <NewBook key={x.id} book={x} updateBook={this.updateBook} />
            )
        )
    }


    render()
    {
        if (this.state?.supplyUploaded)
        {
            return <Redirect to={"/"} />;
        }

        return (
            <Container>
                <Grid>
                    <Grid.Row key={"nadpis"}>
                        <Grid.Column width={13}>
                            <h1><strong>New supply:</strong></h1>
                            <p>Authors with RegNum 1-9 seeded, any other will not work</p>
                        </Grid.Column>
                        <Grid.Column width={3}>
                            <Button content='Submit' primary onClick={() => this.postSupply()} />
                        </Grid.Column>
                    </Grid.Row>
                    <Grid.Row key={"zahlaví knihy"} border={1}>
                        <Grid.Column width={2}>
                            <p><strong>Amount</strong></p>
                        </Grid.Column>
                        <Grid.Column width={3}>
                            <p><strong>Book name</strong></p>
                        </Grid.Column>
                        <Grid.Column width={3}>
                            <p><strong>IBAN</strong></p>
                        </Grid.Column>
                        <Grid.Column width={2}>
                            <p><strong>Amount of pages</strong></p>
                        </Grid.Column>
                        <Grid.Column width={1}>
                        </Grid.Column>
                        <Grid.Column width={2}>
                            <p><strong>Author reg. num.</strong></p>
                        </Grid.Column>
                        <Grid.Column width={3}>
                            <p><strong>Publish date</strong></p>
                        </Grid.Column>

                        <Grid.Column width={16}>
                            <Divider />
                        </Grid.Column>
                    </Grid.Row>
                    {this.renderBooks()}
                    <Grid.Row key={"další kniha"}>
                        <Grid.Column width={14}>
                        </Grid.Column>
                        <Grid.Column width={1}>
                            <Button
                                icon="add circle"
                                secondary
                                onClick={() => this.setState({ books: [...this.state.books, this.createNewBook()] })}
                            />
                        </Grid.Column>
                        <Grid.Column width={1}>
                        </Grid.Column>
                        <Grid.Column width={16}>
                            <Divider />
                        </Grid.Column>
                    </Grid.Row>
                    <Grid.Row key={"zahlaví filmu"} border={1}>
                        <Grid.Column width={2}>
                            <p><strong>Amount</strong></p>
                        </Grid.Column>
                        <Grid.Column width={3}>
                            <p><strong>Film name</strong></p>
                        </Grid.Column>
                        <Grid.Column width={3}>
                            <p><strong>EIDR</strong></p>
                        </Grid.Column>
                        <Grid.Column width={2}>
                            <p><strong>Rating</strong></p>
                        </Grid.Column>
                        <Grid.Column width={1}>
                        </Grid.Column>
                        <Grid.Column width={2}>
                            <p><strong>Author reg. num.</strong></p>
                        </Grid.Column>
                        <Grid.Column width={3}>
                            <p><strong>Publish date</strong></p>
                        </Grid.Column>

                        <Grid.Column width={16}>
                            <Divider />
                        </Grid.Column>
                    </Grid.Row>
                    {this.renderFilms()}
                    <Grid.Row key={"další film"}>
                        <Grid.Column width={14}>
                        </Grid.Column>
                        <Grid.Column width={1}>
                            <Button
                                icon="add circle"
                                secondary
                                onClick={() => this.setState({ films: [...this.state.films, this.newFilm()] })}
                            />
                        </Grid.Column>
                        <Grid.Column width={1}>
                        </Grid.Column>
                    </Grid.Row>
                </Grid>
            </Container>
        );
    }
}
