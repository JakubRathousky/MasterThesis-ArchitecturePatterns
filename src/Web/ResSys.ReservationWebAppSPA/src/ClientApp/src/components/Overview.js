import React, { Component } from 'react';
import { Container, Grid, Divider, Input, Button } from 'semantic-ui-react'
import BookAPI from '../repositories/book';
import FilmAPI from '../repositories/film';
import ReservationAPi from '../repositories/reservation';
import { BookInfo } from './BookInfo';
import { FilmInfo } from './FilmInfo';

export class Overview extends Component
{
  static displayName = Overview.name;

  constructor(props)
  {
    super(props);

    this.state = {
      films: [],
      books: [],
      date: ""
    }
  }

  componentDidMount = async () =>
  {
    var films = (await FilmAPI.getFilms()).data;
    let books = (await BookAPI.getBooks()).data;
    films.forEach(film => film.toReserve = 0);
    books.forEach(book => book.toReserve = 0);
    this.setState({ films: films, books: books, date: "" });
  }

  getBookAvailableAmount = async (book, date) =>
  {
    if (date)
    {
      let result = await ReservationAPi.GetAmountOfReservedBook(book.id, this.state.date);
      book.availableAmount = book.amount - result.data;
      this.updateBook(book);
    }
  }
  getFilmAvailableAmount = async (film, date) =>
  {
    if (date)
    {
      let result = await ReservationAPi.GetAmountOfReservedFilm(film.id, this.state.date);
      film.availableAmount = film.amount - result.data;
      console.log(result);
      this.updateFilm(film);
    }
  }

  onDateChange = (event) =>
  {
    if (event.target.value)
    {
      this.setState({ date: event.target.value });
      if (event.target.value)
      {
        this.state.films.forEach(x => this.getFilmAvailableAmount(x, event.target.value));
        this.state.books.forEach(x => this.getBookAvailableAmount(x, event.target.value));
      }
    }
  }

  updateBook = (updatedBook) =>
  {
    let books = [...this.state.books]
    let book = books.find(x => x.id === updatedBook.id);
    let index = books.map(function (x) { return x.id; }).indexOf(book.id);
    if (book && index >= 0)
    {
      book.toReserve = updatedBook.toReserve;
      book.available = updatedBook.availableAmount;
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
      film.toReserve = updatedFilm.toReserve;
      film.available = updatedFilm.availableAmount;
      films[index] = film;
      this.setState({ films: films })
    }
  }

  renderBooks = () =>
  {
    return this.state?.books?.map(x => (
      <BookInfo key={x.id} book={x} updateBook={this.updateBook} />
    ))

  }
  renderFilms = () =>
  {
    return this.state?.films?.map(x => (
      <FilmInfo key={x.id} film={x} updateFilm={this.updateFilm} />
    ));
  }

  postReservation = () =>
  {
    let films = this.state.films.filter(x => x.toReserve);
    let books = this.state.books.filter(x => x.toReserve);

    ReservationAPi.SaveReservation(films, books, this.state.date);
  }

  render()
  {
    console.log(this.state?.date);
    return (
      <Container>
        <Grid>
          <Grid.Row key={"nadpis"}>
            <Grid.Column width={13}>
              <h1><strong>Supply overview</strong></h1>
            </Grid.Column>
            <Grid.Column width={3}>
              <Button content='Reserve' primary onClick={() => this.postReservation()} />
            </Grid.Column>
          </Grid.Row>
          <Grid.Row key={"datum"}>
            <Grid.Column width={4}>
              <p><strong>Reserve to date:</strong></p>
            </Grid.Column>
            <Grid.Column width={4}>
              <Input
                focus placeholder='Reserve to...'
                value={this.state?.date}
                style={{ width: "11em" }}
                type="date"
                onChange={this.onDateChange}
              />
            </Grid.Column>
            <Grid.Column width={6}>
            </Grid.Column>
          </Grid.Row>
          <Grid.Row key={"zahlaví knihy"} border={1}>
            <Grid.Column width={2}>
              <p><strong>Amount</strong></p>
            </Grid.Column>
            <Grid.Column width={2}>
              <p><strong>Available</strong></p>
            </Grid.Column>
            <Grid.Column width={3}>
              <p><strong>Book name</strong></p>
            </Grid.Column>
            <Grid.Column width={3}>
              <p><strong>IBAN</strong></p>
            </Grid.Column>
            <Grid.Column width={2}>
              <p><strong>Page count</strong></p>
            </Grid.Column>
            <Grid.Column width={1}>
            </Grid.Column>
            <Grid.Column width={3}>
              <p><strong>Publish date</strong></p>
            </Grid.Column>

            <Grid.Column width={16}>
              <Divider />
            </Grid.Column>
          </Grid.Row>
          {this.renderBooks()}

          <Grid.Row key={"zahlaví filmu"} border={1}>
            <Grid.Column width={2}>
              <p><strong>Amount</strong></p>
            </Grid.Column>
            <Grid.Column width={2}>
              <p><strong>Available</strong></p>
            </Grid.Column>
            <Grid.Column width={3}>
              <p><strong>Film name</strong></p>
            </Grid.Column>
            <Grid.Column width={3}>
              <p><strong>EDIR</strong></p>
            </Grid.Column>
            <Grid.Column width={2}>
              <p><strong>Rating</strong></p>
            </Grid.Column>
            <Grid.Column width={1}>
            </Grid.Column>
            <Grid.Column width={3}>
              <p><strong>Published</strong></p>
            </Grid.Column>

            <Grid.Column width={16}>
              <Divider />
            </Grid.Column>
          </Grid.Row>
          {this.renderFilms()}

        </Grid>
      </Container >
    );
  }
}
