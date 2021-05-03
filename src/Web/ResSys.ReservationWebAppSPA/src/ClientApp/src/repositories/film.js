import server from './server';

class FilmRepository
{
    getFilms = async () =>
    {
        return server.get('/films');
    }
    getFilmById = async (filmId) =>
    {
        return server.get(`/supply/${filmId}`);
    }
}

export default new FilmRepository();