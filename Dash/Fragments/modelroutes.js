models.get('/{modelName}', function (req, res) {
    communicator.getModel('{modelName}', function (model) {
        res.json({ model: model });
    });
});
