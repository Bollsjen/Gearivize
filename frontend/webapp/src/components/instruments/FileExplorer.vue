<template>
  <b-modal v-model="active" centered size="xl">
    <template #modal-header>
      <h3>Gearivize File Explorer</h3>
    </template>
      
      <b-row>
          <b-col sm="3">
              <div v-for="directory in files.directories">
                  {{directory.directoryName}}
              </div>
          </b-col>
          
          <b-col sm="6">
              
          </b-col>
          
          <b-col sm="3">
              
          </b-col>
      </b-row>
      
      <template #modal-footer>
          <b-button @click="close">Close</b-button>
      </template>
  </b-modal>
</template>

<script>
import {fileExplorerService} from "@/services/fileExplorerService";
export default {
  components: {

  },
  props: {

  },
  data(){
    return {
      active: false,
      aNumber: '',
        files: {},
    }
  },
  methods: {
    show(aNumber){
      this.aNumber = aNumber
      this.active = true
    },

    close(){
      this.active = false
    },
      
      load(){
        fileExplorerService.getAllFiles()
            .then(result => this.files = result.data)
            .catch(error => this.$bvModal.msgBoxOk(error.status, {
                title: 'Error',
                size: 'sm',
                buttonSize: 'sm',
                okVariant: 'success',
                centered: true
            }))
      }
  },
  computed: {

  },
  mounted(){

  }
}
</script>