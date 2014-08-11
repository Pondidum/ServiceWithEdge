var express = require('express');
var app = express();

var communicator;

var getModel = function (modelName, action) {
    communicator.getModel(modelName, function (error, result) {

        if (error) throw error;

        action(result);
    });
};

app.set('views', 'views');
app.set('view engine', 'jade');
app.use(express.static('public'));

app.get('/', function(req, res) {
    res.render('index', { title: "test", result: "amaze"});
});
