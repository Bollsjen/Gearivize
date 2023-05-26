<template>
  <b-modal v-model="active" centered size="xl">
    <template #modal-header>
      <h3>Gearivize File Explorer</h3>
    </template>
      
      <b-row>
        <b-col sm="12" style="border-bottom: 1px solid gray; padding-bottom: 10px">
          <b-button>
            <i class="fa-solid fa-folder-plus toolbar-icons"></i>
          </b-button>
        </b-col>
          <b-col sm="4" style="overflow-x: visible; display: flex; border-right: 1px solid gray; padding: 0">
              <file-explorer-file-tree :directories="getFiles" :show-files="false" @select-directory="selectDirectory" do-in-tapping :selected-directory="selectedFolder" />
          </b-col>
          
          <b-col sm="8" ref="fileDrop" @dragover="fileDragOver" @dragleave="fileDragLeave" @drop="fileDrop">
            <fil-explorer-browse v-if="!isDragingFile" ref="fileBrowse" @select-directory="selectDirectory" :files="files" :directory="getDirectory" @reload="reload" @get-directorypath="findInstrumentDirectory" />
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
      selectedFolder: null,
      directoryTreeToSelectedFolder: null,
      isDragingFile: false
    }
  },
  methods: {
    fileDragOver(event){
      event.preventDefault()
      if(event.dataTransfer.files) {
        this.isDragingFile = true
        this.$refs.fileDrop.classList.add('file-drop-zone')
      }
    },

    fileDragLeave(event){
      event.preventDefault()
      if(event.dataTransfer.files) {
        this.isDragingFile = false
        this.$refs.fileDrop.classList.remove('file-drop-zone')
      }
    },

    fileDrop(event){
      event.preventDefault()
      this.isDragingFile = false
      this.$refs.fileDrop.classList.remove('file-drop-zone')
      if(!this.$store.state.isAuthenticated.responsible){
        this.$bvModal.msgBoxOk('You are not allowed to perform this action!', {
          title: 'Unauthorized use',
          size: 'sm',
          buttonSize: 'sm',
          okVariant: 'danger',
          centered: true
        })

        return
      }

      if(event.dataTransfer.files) {

        let formData = new FormData()

        formData.append('file',  event.dataTransfer.files[0])
        formData.append('path', this.findInstrumentDirectory(this.selectedFolder.directoryName)[0].path)

        fileExplorerService.uploadFile(formData)
            .then(result => this.reload())
            .catch(error => this.$bvModal.msgBoxOk('You are not allowed to perform this action!', {
              title: 'Unauthorized use',
              size: 'sm',
              buttonSize: 'sm',
              okVariant: 'danger',
              centered: true
            }))

      }
    },

    show(aNumber){
      this.aNumber = aNumber
      this.active = true
      this.directoryTreeToSelectedFolder = this.findInstrumentDirectory(aNumber)
      this.selectedFolder = this.directoryTreeToSelectedFolder[0].directory
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

    reload(){
      fileExplorerService.getAllFiles()
          .then(result => {
            this.files = result.data
            this.selectedFolder = this.findInstrumentDirectory(this.selectedFolder.directoryName)[0].directory
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
      this.findInstrumentDirectory(directory.directoryName)
    },

    findInstrumentDirectory(aNumber){
      const matchingDirectories = []

      function traverseDirectories(directories, path){
        for (const directory of directories) {
          // Construct the current directory's path
          const currentPath = path.concat(directory.directoryName);

          if (directory.directoryName === aNumber) {
            matchingDirectories.push({
              directory: directory,
              path: currentPath
            });
          }

          if (directory.directories.length > 0) {
            traverseDirectories(directory.directories, currentPath);
          }
        }
      }

      traverseDirectories(this.files, []);
      return matchingDirectories;
    },
  },
  computed: {
    getPath(){
      return this.directoryTreeToSelectedFolder.path
    },

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

<style scoped>
.toolbar-icons {
  font-size: 24px;
}

.file-drop-zone {
  border: 8px dashed gray;
}
</style>