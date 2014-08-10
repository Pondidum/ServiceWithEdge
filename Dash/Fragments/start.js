return function(options, callback) {
    communicator = options;
    app.listen(options.port);
};
