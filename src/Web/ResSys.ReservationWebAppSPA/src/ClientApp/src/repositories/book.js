import server from './server';

class BookRepository
{
    getBooks = async () =>
    {
        return server.get('/books');
    }
    getBookById = async (bookId) =>
    {
        return server.get(`/supply/${bookId}`);
    }
}

export default new BookRepository();