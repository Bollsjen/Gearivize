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
      console.log(this.$store.state.isChecking)
      return this.$store.state.isChecking
    }
  },
  watch: {
    store() {
      if(!this.store){
        console.log("snask")
        if(this.$store.state.isAuthenticated !== {} || this.$store.state.isAuthenticated !== null){
          if(this.minimumRequirement === '') this.$router.push('/')
          if(!this.$store.state.isAuthenticated[this.minimumRequirement]) this.$router.push('/')
          if(!this.$store.state.isAuthenticated) this.$router.push('/')
          console.log("fisk")
          console.log(this.$store.state.isAuthenticated)
        }else{
          this.$router.push('/')
        }
      }
    },
  }
}
</script>