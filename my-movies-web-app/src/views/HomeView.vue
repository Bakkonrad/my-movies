<template>
  <div class="container">
    <div class="card">
      <div class="card-header">
        <h4>
          MyMovies
          <RouterLink class="btn btn-primary float-end " to="/add-movie">
            Add Movie
          </RouterLink>
        </h4>
        </div>
        <div class="card-body">
          <table class="table table-bordered">
            <thead>
              <tr>
                <th>Id</th>
                <th>Title</th>
                <th>Year</th>
                <th>Director</th>
                <th>Rate</th>
              </tr>
            </thead>
            <tbody v-if="this.movies.length > 0 && this.responseCode == 200">
              <tr v-for="movie in movies" :key="movie.id">
                <td>{{ movie.id }}</td>
                <td>{{ movie.title }}</td>
                <td>{{ movie.year }}</td>
                <td>{{ movie.director }}</td>
                <td>{{ movie.rate }}</td>
                <td>
                  <RouterLink class="btn btn-success" to="/edit-movie">
                    Edit
                  </RouterLink>
                  <button type="button" class="btn btn-danger" @click="deleteMovie(movie.id)">
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
          <tbody v-else>
            <tr>
              <td colspan="6" class="text-center">Loading...</td>
            </tr>
          </tbody>
          </table>
        </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default{
  name: 'movies',
  data(){
    return{
      movies: []
    }
  },
  mounted(){
    this.getMovies();
  },
  methods:{
    getMovies(){
      axios.get('http://localhost:5178/Movies').then(res => {
        this.movies = res.data
        this.responseCode = res.status
        // console.log(res)
      });
    }
  },
}
</script>