var express = require('express');
var app = express();

var communicator;

var getModel = function (modelName, action) {
    communicator.getModel(modelName, function (error, result) {

        if (error) throw error;

        action(result);
    });
};
