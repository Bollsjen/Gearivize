<template>
  <b-modal v-model="active" centered size="xl">
    <template #modal-header>
      <h3>Gearivize File Explorer</h3>
    </template>
      
      <b-row>
          <b-col sm="4" style="overflow-x: visible; display: flex; border-right: 1px solid gray; padding: 0">
              <file-explorer-file-tree :directories="getFiles" :show-files="false" @select-directory="selectDirectory" do-in-tapping />
          </b-col>
          
          <b-col sm="8">
            <fil-explorer-browse ref="fileBrowse" @select-directory="selectDirectory" :directory="getDirectory" :path="getPath" />
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
    }
  },
  methods: {
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

    selectDirectory(directory){
      this.selectedFolder = directory
      console.log(this.$refs.fileBrowse.$refs.A1)
      this.findInstrumentDirectory(directory.directoryName)
    },

    openDirectoryCollapseables(path){
      this.$nextTick(() => {
            this.$nextTick(() => {
              this.$nextTick(() => {
                this.$nextTick(() => {
              if (path !== undefined && path !== null)
                path.forEach((path, index) => {
                  if(index > 0) {
                    console.log(this.$refs.fileBrowse.$refs)
                    this.$refs.fileBrowse.$refs[path].style.display = 'block'
                  }
                })
              })
            })
          })
      })
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

      this.openDirectoryCollapseables(matchingDirectories[0].path)
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