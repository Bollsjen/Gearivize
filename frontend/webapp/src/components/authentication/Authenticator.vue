<template></template>

<script>
export default {
  props: {
    minimumRequirement: {
      type: String,
      default: ''
    }
  },
  computed: {
    store(){
      return this.$store.state.isChecking
    }
  },
  watch: {
    store() {
      if(!this.store){
        if(this.minimumRequirement === '') this.$router.push('/')
        if(!this.$store.state.isAuthenticated[this.minimumRequirement]) this.$router.push('/')
      }
    }
  }
}
</script>