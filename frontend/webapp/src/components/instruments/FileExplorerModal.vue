<template>
  <b-modal v-model="active" centered size="xl">
    <template #modal-header>
      <h3>Gearivize File Explorer</h3>
    </template>
      
      <b-row>
          <b-col sm="4" style="overflow-x: visible; display: flex">
              <file-explorer-file-tree :directories="getFiles" :show-files="false" @select-directory="selectDirectory" do-in-tapping />
          </b-col>
          
          <b-col sm="8">
            <fil-explorer-browse :directory="getDirectory" />
          </b-col>
      </b-row>
      
      <template #modal-footer>
          <b-button @click="close">Close</b-button>
      </template>
  </b-modal>
</template>

<script>
import {fileExplorerService} from "@/services/fileExplorerService";
import FileExplorerFileTree from "@/components/FileExplorerFileTree.vue";
import FilExplorerBrowse from "@/components/FilExplorerBrowse.vue";
export default {
  components: {
    FileExplorerFileTree,
    FilExplorerBrowse
  },
  props: {

  },
  data(){
    return {
      active: false,
      aNumber: '',
        files: {},
      selectedFolder: null
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
            .then(result => {
              this.files = result.data
              this.selectedFolder = this.files[0]
            })
            .catch(error => this.$bvModal.msgBoxOk(error.status, {
                title: 'Error',
                size: 'sm',
                buttonSize: 'sm',
                okVariant: 'success',
                centered: true
            }))
      },

    selectDirectory(directory){
      this.selectedFolder = directory
    }
  },
  computed: {
    getFiles(){
      return this.files
    },
    getDirectory(){
      return this.selectedFolder
    }
  },
  mounted(){
    this.load()
  }
}
</script>