const mongoose = require('mongoose');

let filmSchema = mongoose.Schema({
    title: {type: 'string', required: 'true'},
    comments: {type: 'string', required: 'true'},
});

let Film = mongoose.model('Film', filmSchema);

module.exports = Film;