<template>
  <span v-if="vModel.$error" class="error-text">{{vModel.$errors[0].$message === '' ? message : vModel.$errors[0].$message}}</span>
</template>

<script>
export default {
  props:{
    vModel: null,
    message: {
      type: String,
      default: () => 'Required'
    }
  },
  methods: {
    getComponents(){
      const components = this.$parent.$children
      console.log(components)

      components.forEach(component => {
        if(component.$attrs.vuelidate !== null){
          component.$el.className = 'form-control error-field'
        }
      })
    }
  },
  watch: {

  },
  mounted(){

  }
}
</script>

<style scoped>
.error-text {
  color: red;
}

.error-field {
  border: 1px solid red !important;
}
</style>