import server from './server';

class LogisticRepository
{
    synchronize = async () =>
    {
        return server.get(`/synchronize`);
    }
    saveSupply = async (films, books) =>
    {
        return server.post('/supply', { films, books });
    }
    getSupplyById = async (supplyId) =>
    {
        return server.get(`/supply/${supplyId}`);
    }
    getSupplies = async () =>
    {
        return server.get(`/supply`);
    }
}

export default new LogisticRepository();