<template>
    <div v-if="OpenClose" class="modal fade show"
    tabindex="-1" aria-labelledby="exampleModalLabel" aria-modal="true" role="dialog" style="display:block">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{ movie.id ? 'Edit Movie' : 'Add Movie' }}</h5>
                    <button type="button" class="btn-close" @click="OpenCloseFun()" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                    <label for="movieTitle" class="form-label" >Title</label>
                    <input type="text" class="form-control" id="movieTitle" v-model="movie.title">
                    </div>
                    <div class="mb-3">
                    <label for="movieDirector" class="form-label">Director</label>
                    <input type="text" class="form-control" id="movieDirector" v-model="movie.director">
                    </div>
                    <div class="mb-3">
                    <label for="movieYear" class="form-label">Year</label>
                    <input type="text" class="form-control" id="movieYear" v-model="movie.year">
                    </div>
                    <div class="mb-3">
                    <label for="movieRate" class="form-label">Rate</label>
                    <input type="text" class="form-control" id="movieRate" v-model="movie.rate">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" @click="OpenCloseFun()" >Close</button>
                    <button type="button" class="btn btn-success" @click="Save()" >{{ movie.id ? 'Update' : 'Add' }}</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'MovieModal',
    props: {
        visible: {
            type: Boolean,
            default: false
        },
        movie: {
            type: Object,
            default: null
        }
    },
    data() {
        return {
            OpenClose: this.visible,
            selectedMovie: this.movie
        }
    },
    methods: {
        OpenCloseFun() {
            this.OpenClose = !this.OpenClose;
        },
        Save() {
            this.$emit('saveMovie', this.selectedMovie);
            this.OpenClose = false;
        }
    },
    watch: {
        visible: function (newVal) {
            this.OpenClose = newVal;
        },
        movie: function(newVal) {
            this.selectedMovie = newVal;
        }
    }
}

</script>