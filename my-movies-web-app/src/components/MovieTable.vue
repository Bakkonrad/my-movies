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
                    <tbody v-if="this.movies.length > 0 && this.responseCode == 200">
                        <tr v-for="movie in movies" :key="movie.id">
                            <td>{{ movie.id }}</td>
                            <td>{{ movie.title }}</td>
                            <td>{{ movie.year }}</td>
                            <td>{{ movie.director }}</td>
                            <td>{{ movie.rate }}</td>
                            <td class="text-end">
                                <button class="btn btn-success" @click="openEditModal(movie)" >
                                    Edit
                                </button>
                                <span>&nbsp;</span>
                                <button type="button" class="btn btn-danger" @click="confirmDelete(movie.id)">
                                    Delete
                                </button>
                            </td>
                        </tr>
                    </tbody>
                    <tbody v-else-if="this.movies.length == 0 && this.responseCode == 200">
                        <tr>
                            <td colspan="6" class="text-center">No movies found.</td>
                        </tr>
                    </tbody>
                    <tbody v-else-if="this.responseCode != 200 && this.loaded == true">
                        <tr>
                            <td colspan="6" class="text-center">Error loading movies... {{ err }}</td>
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
    <MovieModal ref="movieModal" :movie="selectedMovie"/>
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
            err: "",
            selectedMovie: null,
            responseCode: null,
        }
    },
    mounted() {
        this.getMovies();
    },
    methods: {
        getMovies() {
            axios.get('http://localhost:5178/Movies').then(res => {
                this.movies = res.data
                this.responseCode = res.status
                this.loaded = true
            })
                .catch(err => {
                    this.responseCode = err.response ? err.response.status : null;
                    this.err = err.message
                    this.loaded = true
                });
        },
        openAddModal() {
        this.selectedMovie = { title: '', director: '', year: '', rate: '', id: null };
        this.$refs.movieModal.OpenCloseFun();
        },
        openEditModal(movie) {
        this.selectedMovie = movie;
        this.$refs.movieModal.OpenCloseFun();
        },
        saveMovie(movie) {
        if (this.selectedMovie) {
            // If a movie is selected, update it
            const index = this.movies.findIndex(m => m.id === this.selectedMovie.id);
            this.movies.splice(index, 1, movie);
        } else {
            // If no movie is selected, add a new movie
            this.movies.push(movie);
        }
        this.$refs.movieModal.closeModal();
        },
        confirmDelete(id) {
            if (confirm('Are you sure you want to delete this movie?')) {
                this.deleteMovie(id);
            }
        },
        deleteMovie(id) {
            axios.delete('http://localhost:5178/Movies/' + id).then(res => {
                this.getMovies();
            })
                .catch(err => {
                    this.responseCode = err.response.status
                    this.err = err.response.data
                    this.loaded = true
                });
        },
        async syncExternalMovies() {
                axios.get('http://localhost:5178/Movies/external').then(res => {
                    this.getMovies();
                })
                    .catch(err => {
                        this.responseCode = err.response.status
                        this.err = err.response.data
                        this.loaded = true
                    });
        }
    },
    components: {
    MovieModal,
    }
}
</script>


<style scoped>
.table-dark th {
    background-color: rgb(185, 185, 185);
    color: #ffffff;
    font-weight: bold;
}
</style>