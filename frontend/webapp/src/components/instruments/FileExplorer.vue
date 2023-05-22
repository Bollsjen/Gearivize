<template>
  <b-modal v-model="active" centered size="xl">
    <template #modal-header>
      <h3>Gearivize File Explorer</h3>
    </template>
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
      aNumber: ''
    }
  },
  methods: {
    show(aNumber){
      this.aNumber = aNumber
      this.active = true
    },

    close(){
      this.active = false
    }
  },
  computed: {

  },
  mounted(){

  }
}
</script>