<template></template>

<script>
export default {
  props: {
    minimumRequirement: {
      type: String,
      default: ''
    },
  },
  computed: {
    store(){
      return this.$store.state.isChecking
    }
  },
  watch: {
    '$store.state.isChecking': function (newVal,oldVal) {
      if(!this.$store.state.isChecking){
        if(this.$store.state.isAuthenticated !== {} || this.$store.state.isAuthenticated !== null){
          if(this.minimumRequirement === '') this.$router.push('/')
          if(!this.$store.state.isAuthenticated[this.minimumRequirement]) this.$router.push('/')
          if(!this.$store.state.isAuthenticated) this.$router.push('/')
        }else{
          this.$router.push('/')
        }
      }
    },
  }
}
</script>