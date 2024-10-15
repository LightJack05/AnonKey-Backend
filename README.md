# AnonKey-Backend
AnonKey is a zero-knowledge password manager, designed for self hosting.

Installation instructions, Concepts and migration steps are detailed in the Wiki: 
https://github.com/LightJack05/AnonKey-Backend/wiki

(The project is currently Work-In-Progress, please don't expect everything to work as intended immediately.)
# Documentation

If you would like to build documentation for code and classes as an HTML website, install doxygen and run this in the repo folder:

```bash
make docs
```

The Swagger API docs can be accessed by running the AnonKey Backend in debug mode, at the `/swagger` path.
Run:
```bash
make run
```

then visit http://localhost:5000/swagger in your browser.
