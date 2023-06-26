<template>
  <b-modal v-model="active" size="lg">
    <div v-if="errors.length > 0">
      <div style="background-color: rgb(240,240,240); border: 1px solid rgb(200,200,200); border-radius: 5px" class="p-2 m-0">
        <p v-for="error in errors" style="font-size: 12px; color: red; margin: 0">{{error}}</p>
      </div>
      <p style="font-size: 12px; color: red" class="mt-2">No instruments was imported</p>
    </div>
    <p v-else style="font-size: 12px">Everthing imported successfully</p>
  </b-modal>
</template>

<script>
export default {
  components: {

  },
  props: {

  },
  data(){
    return {
      active: false,
      errors: []
    }
  },
  methods: {
    show(errors){
      this.active = true
      this.errors = errors
    },

    close(){
      this.active = false
    }
  },
  computed: {

  },
  mounted() {

  }
}
</script>