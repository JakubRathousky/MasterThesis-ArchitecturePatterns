import axios from 'axios';

// 5023 is port for logistic gateway

export default axios.create({
    baseURL: 'http://localhost:5023/api'
})