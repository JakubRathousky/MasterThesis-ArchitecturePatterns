import server from './server';

class ReservationRepository
{
    GetAmountOfReservedBook = async (bookId, date) =>
    {
        return server.get(`/reservation/book/${bookId}/${date}`);
    }
    GetAmountOfReservedFilm = async (filmId, date) =>
    {
        return server.get(`/reservation/film/${filmId}/${date}`);
    }
    SaveReservation = async (films, books, date) =>
    {
        films = films.map(film => { return { filmId: film.id, amount: film.toReserve } });
        books = books.map(film => { return { bookId: film.id, amount: film.toReserve } });
        console.log(films, books);
        return server.post(`/reservation`, { date, films, books });
    }
}

export default new ReservationRepository();