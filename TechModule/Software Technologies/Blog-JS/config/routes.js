const userController = require('./../controllers/user');
const homeController = require('./../controllers/home');
const articleController = require('./../controllers/article');

module.exports = (app) => {
    app.get('/', homeController.index);

    //Register
    app.get('/user/register', userController.registerGet);
    app.post('/user/register', userController.registerPost);

    //Login
    app.get('/user/login', userController.loginGet);
    app.post('/user/login', userController.loginPost);

    //Logout
    app.get('/user/logout', userController.logout);

    //Articles
    app.get('/article/create', articleController.createGet);
    app.post('/article/create', articleController.createPost);

    app.get('/article/details/:id', articleController.details);

    app.get('/user/details/:id', userController.details);
};

