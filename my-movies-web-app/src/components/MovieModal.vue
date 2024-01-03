<template>
    <div v-if="OpenClose" class="modal fade show"
    tabindex="-1" aria-labelledby="exampleModalLabel" aria-modal="true" role="dialog" style="display:block">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modal title</h5>
                    <button type="button" class="btn-close" @click="OpenCloseFun()" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>Modal body text goes here.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" @click="OpenCloseFun()" >Close</button>
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
            OpenClose: this.visible
        }
    },
    methods: {
        OpenCloseFun() {
            this.OpenClose = !this.OpenClose;
        }
    },
    watch: {
        visible: function (newVal) {
            this.OpenClose = newVal;
        }
    }



}

</script>