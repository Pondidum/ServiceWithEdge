app.set('views', 'views');
app.set('view engine', 'jade');

var staticSharp = function () {

    return function staticMiddleware(req, res, next) {

        if ('GET' != req.method && 'HEAD' != req.method) return next();

        //if you pass req or res into a .net callback, edge throws stackoverflows
        communicator.getStatic(req.originalUrl, function (error, result) {

            if (error) throw error;

            //how do i put a .net stream or buffer into response?
            console.log(result);

        });
    };
};

app.use(staticSharp());
