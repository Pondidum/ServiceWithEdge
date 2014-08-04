var options;

exports.set = function (m) {
    options = m;
};


exports.getModel = function (modelName, action) {

    options.getModel(modelName, function (error, result) {

        if (error) throw error;

        action(result);
    });
    
};
