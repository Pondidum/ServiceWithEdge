var express = require('express');
var path = require('path');
var logger = require('morgan');

var communicator = require('../webui/communicator');

var root = path.join(__dirname, '../webui');

var app = express();

// view engine setup
app.set('views', path.join(root, 'views'));
app.set('view engine', 'jade');

app.use(logger('dev'));
app.use(express.static(path.join(root, 'public')));

var routes = express.Router();

routes.get('/', function(req, res) {
    res.render('index');
});

app.use('/', routes);

//{Models}

/// catch 404 and forward to error handler
app.use(function(req, res, next) {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

/// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use(function(err, req, res, next) {
        res.status(err.status || 500);
        res.render('error', {
            message: err.message,
            error: err
        });
    });
}

// production error handler
// no stacktraces leaked to user
app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
        message: err.message,
        error: {}
    });
});


return function(options, callback) {
    communicator.set(options);
    
    app.set('port', options.port);
    app.listen(app.get('port'));
};
