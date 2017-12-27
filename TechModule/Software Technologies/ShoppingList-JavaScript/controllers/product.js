const Product = require('../models/Product');

module.exports = {
    index: (req, res) => {
        Product.find().sort({"priority": 1}).then(products => {
            res.render('product/index', {'entries': products});
        });
    },

    createGet: (req, res) => {
        res.render('product/create');
    },

    createPost: (req, res) => {
        let product = req.body;
        Product.create(product).then(product => {
            res.redirect("/");
        }).catch(err => {
            product.error = 'Invalid input!';
            res.render('product/create', product);
        });
    },

    editGet: (req, res) => {
        let productId = req.params.id;
        Product.findById(productId).then(product => {
            res.render('product/edit', product);
        }).catch(err => res.redirect('/'));
    },

    editPost: (req, res) => {
        let productId = req.params.id;
        let product = req.body;

        Product.findByIdAndUpdate(productId, product, {runValidators: true}).then(products => {
            res.redirect("/");
        }).catch(err => {
            product.id = productId;
            product.error = "Invalid input!";
            return res.render("product/edit", product);
        });
    }
};