var express = require('express');

var communicator;

var getModel = function (modelName, action) {
    communicator.getModel(modelName, function (error, result) {

        if (error) throw error;

        action(result);
    });
};


var staticSharp = function (root) {

    root = root.toLowerCase();

    return function staticMiddleware(req, res, next) {

        if ('GET' != req.method && 'HEAD' != req.method) {
            return next();
        }

        var path = req.originalUrl.toLowerCase();

        //if you pass req or res into a .net callback, edge throws stackoverflows
        var result = communicator.getStatic(root + path, true);

        if (result && result.length > 0) {
            res.set('Content-Type', 'text/css');
            res.send(result);
        } else {
            return next();
        }
    };
};

var app = express();

app.set('views', 'views');
app.set('view engine', 'jade');

app.use(staticSharp('/public'));

app.get('/', function (req, res) {
    res.render('index');
});
