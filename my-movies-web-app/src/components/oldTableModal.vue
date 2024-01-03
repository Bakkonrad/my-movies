<template>
    <div>
        <h2>Lista Filmów</h2>
        <!-- Bootstrap Table for displaying movies -->
        <table class="table">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Tytuł</th>
                    <th>Rok</th>
                    <th>Akcje</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(movie, index) in movies" :key="movie.id">
                    <td>{{ index + 1 }}</td>
                    <td>{{ movie.title }}</td>
                    <td>{{ movie.year }}</td>
                    <td>
                        <!-- Bootstrap Button to open modal for editing -->
                        <button class="btn btn-primary" @click="openEditModal(movie)">Edytuj</button>
                    </td>
                </tr>
            </tbody>
        </table>

        <!-- Bootstrap Button to open modal for adding a new movie -->
        <button class="btn btn-success" @click="openAddModal">Dodaj Film</button>

        <!-- MovieForm as a modal for editing/adding movies -->
        <MovieForm :movie="selectedMovie" @save="saveMovie" ref="movieForm" />
    </div>
</template>

<script>
import MovieForm from './MovieForm.vue';

export default {
    data() {
        return {
            movies: [], // Array to store movies fetched from API
            selectedMovie: null, // Store the movie selected for editing
        };
    },
    methods: {
        openEditModal(movie) {
            this.selectedMovie = { ...movie };
            this.$refs.movieForm.openModal();
        },
        openAddModal() {
            this.selectedMovie = null;
            this.$refs.movieForm.openModal();
        },
        saveMovie(movie) {
            // Handle saving/updating movie (communicate with API)
            // Refresh movie list
            this.fetchMovies();
        },
        fetchMovies() {
            // Fetch movies from API and update the 'movies' data property
        },
    },
    mounted() {
        // Fetch movies when the component is mounted
        this.fetchMovies();
    },
    components: {
        MovieForm,
    },
};
</script>
