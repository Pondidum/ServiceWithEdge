var express = require('express');
var router = express.Router();

var com = require('../communicator');

/* GET home page. */
router.get('/', function (req, res) {

    com.getModel("index", function(value) {
        
        res.render('index', {
            title: 'Express',
            result: value.Iterations
        });

    });
    
});

module.exports = router;
