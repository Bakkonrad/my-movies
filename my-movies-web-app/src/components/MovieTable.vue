<template>
    <div class="container">
        <div class="card">
            <div class="card-header">
                <h4>
                    My Movies
                    <button class="btn btn-primary float-end " @click="openAddModal">Add movie</button>
                    <span class="float-end">&nbsp;</span>
                    <button class="btn btn-primary float-end " @click="syncExternalMovies">Download external movies</button>
                </h4>
            </div>
            <div class="error center-text" v-if="err">{{ err }}</div>
            <div class="success center-text" v-if="successMessage">{{ successMessage }}</div>
            <div class="card-body">
                <table class="table table-light table-striped">
                    <thead class="table-dark">
                        <tr>
                            <th>Id</th>
                            <th>Title</th>
                            <th>Year</th>
                            <th>Director</th>
                            <th>Rate</th>
                            <th class="text-end">Actions</th>
                        </tr>
                    </thead>
                    <tbody v-if="this.movies.length > 0 && this.loaded == true">
                        <tr v-for="movie in movies" :key="movie.id">
                            <td>{{ movie.id }}</td>
                            <td>{{ movie.title }}</td>
                            <td>{{ movie.year }}</td>
                            <td>{{ movie.director }}</td>
                            <td>{{ movie.rate }}</td>
                            <td class="text-end">
                                <button class="btn btn-success" @click="openEditModal(movie)">
                                    Edit
                                </button>
                                <span>&nbsp;</span>
                                <button type="button" class="btn btn-danger" @click="confirmDelete(movie.id)">
                                    Delete
                                </button>
                            </td>
                        </tr>
                    </tbody>
                    <tbody v-else-if="this.movies.length == 0 && this.loaded == true">
                        <tr>
                            <td colspan="6" class="text-center">No movies found.</td>
                        </tr>
                    </tbody>
                    <tbody v-else-if="this.loaded == false">
                        <tr>
                            <td colspan="6" class="text-center">Loading...</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <MovieModal ref="movieModal" :movie="selectedMovie" @saveMovie="saveMovie" />
</template>

<script>
import axios from 'axios'
import MovieModal from './MovieModal.vue';

export default {
    name: 'movies',
    data() {
        return {
            movies: [],
            loaded: false,
            err: null,
            selectedMovie: null,
            responseCode: null,
            successMessage: null
        }
    },
    mounted() {
        this.getMovies();
    },
    methods: {
        getMovies() {
            axios.get('https://mymoviesapi2024.azurewebsites.net/movies').then(res => {
                this.movies = res.data
                this.responseCode = res.status
                this.loaded = true
                this.err = null
            })
                .catch(this.handleError);
        },
        openAddModal() {
            this.selectedMovie = { title: '', director: '', year: '', rate: '', id: null };
            this.$refs.movieModal.OpenCloseFun();
        },
        openEditModal(movie) {
            this.selectedMovie = Object.assign({}, movie);
            this.$refs.movieModal.OpenCloseFun();
        },
        saveMovie(movie) {
            console.log(movie);
            if (movie.id) {
                this.updateMovie(movie);
            } else {
                this.addMovie(movie);
            }
        },
        confirmDelete(id) {
            if (confirm('Are you sure you want to delete this movie?')) {
                this.deleteMovie(id);
            }
        },
        deleteMovie(id) {
            axios.delete('https://mymoviesapi2024.azurewebsites.net/movies/' + id).then(res => {
                this.successMessage = 'Movie successfully deleted!';
                this.getMovies();
            })
                .catch(this.handleError);
        },
        async syncExternalMovies() {
            axios.get('https://mymoviesapi2024.azurewebsites.net/movies/external').then(res => {
                this.successMessage = 'External movies successfully added!';
                this.getMovies();
            })
                .catch(error => {
                    this.handleError(error);
                });
        },
        addMovie(movie) {
            movie.id = 0;
            axios.post('https://mymoviesapi2024.azurewebsites.net/movies/', movie).then(res => {
                this.successMessage = 'Movie successfully added!';
                this.getMovies();
            })
                .catch(this.handleError);
        },
        updateMovie(movie) {
            axios.put('https://mymoviesapi2024.azurewebsites.net/movies/' + movie.id, movie).then(res => {
                this.successMessage = 'Movie successfully updated!';
                this.getMovies();
            })
                .catch(this.handleError);
        },
        handleError(err) {
            if (err.response) {
                // The request was made and the server responded with a status code
                this.responseCode = err.response.status;
                this.err = err.message;
            } else if (err.request) {
                // The request was made but no response was received
                this.err = 'No response received from the server.';
            } else {
                // Something happened in setting up the request that triggered an Error
                this.err = 'An error occurred while setting up the request.';
            }
            this.loaded = true;
        },
    },
    components: {
        MovieModal
    }
}
</script>


<style scoped>
.table-dark th {
    background-color: rgb(185, 185, 185);
    color: #ffffff;
    font-weight: bold;
}

.center-text {
    text-align: center;
}

.error {
    color: red;
}

.success {
    color: green;
}
</style>