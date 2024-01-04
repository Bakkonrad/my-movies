<template>
    <div v-if="OpenClose" class="modal fade show"
    tabindex="-1" aria-labelledby="exampleModalLabel" aria-modal="true" role="dialog" style="display:block">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">{{ selectedMovie.id ? 'Edit Movie' : 'Add Movie' }}</h5>
                    <button type="button" class="btn-close" @click="OpenCloseFun()" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                    <label for="movieTitle" class="form-label" >Title</label>
                    <input type="text" class="form-control" id="movieTitle" v-model="selectedMovie.title">
                    <p class="error" v-for="error of v$.selectedMovie.title.$errors" :key="error.$uid">{{ error.$message }}</p>
                    </div>
                    <div class="mb-3">
                    <label for="movieDirector" class="form-label">Director</label>
                    <input type="text" class="form-control" id="movieDirector" v-model="selectedMovie.director">
                    <p class="error" v-for="error of v$.selectedMovie.director.$errors" :key="error.$uid">{{ error.$message }}</p>
                    </div>
                    <div class="mb-3">
                    <label for="movieYear" class="form-label">Year</label>
                    <input type="number" class="form-control" id="movieYear" v-model="selectedMovie.year">
                    <p class="error" v-for="error of v$.selectedMovie.year.$errors" :key="error.$uid">{{ error.$message }}</p>
                    </div>
                    <div class="mb-3">
                    <label for="movieRate" class="form-label">Rate</label>
                    <input type="number" class="form-control" id="movieRate" v-model="selectedMovie.rate">
                    <p class="error" v-for="error of v$.selectedMovie.rate.$errors" :key="error.$uid">{{ error.$message }}</p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" @click="OpenCloseFun()" >Close</button>
                    <button type="button" class="btn btn-success" @click="Save()" >{{ selectedMovie.id ? 'Update' : 'Add' }}</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import useVuelidate from '@vuelidate/core'
import { required, maxLength, between, numeric } from '@vuelidate/validators'

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
            v$: useVuelidate(),
            OpenClose: this.visible,
            selectedMovie: this.movie ? this.movie : {
                title: '',
                year: '',
                director: '',
                rate: ''
            }
        }
    },
    validations() {
        return {
        selectedMovie: {
            title: { required, maxLength: maxLength(200), $autoDirty: true },
            year: { required, numeric, between: between(1900, 2200), $autoDirty: true },
            director: { required, $autoDirty: true },
            rate: { required, numeric, $autoDirty: true}
            }
        }
    },
    methods: {
        OpenCloseFun() {
            this.OpenClose = !this.OpenClose;
        },
        Save() {
            console.log(this.v$)
            this.v$.$touch()
            if (this.v$.$error) {
                return;
            }
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

<style>
/* class error with red text */
.error {
    color: red;
}

</style>