<template>
    <div class="w-100" style="z-index: 1; display: flex; flex-wrap: wrap; align-items: flex-start; width: 100%; position: relative; height: 100%" @click="isContextMenuVisible ? disableContextMenu() : null" @dragover="dragThingOver($event,null)" @drop="dropThing($event)">
      <b-row class="w-100">
        <b-col sm="12" class="d-flex w-100">
          <div style="flex: 1">
            Name
          </div>

          <div style="flex: 1">
            Last modified
          </div>

          <div style="flex: 1">
            Size
          </div>
        </b-col>

        <button class="col-12 btn btn-block" v-for="item in items" @click="!isContextMenuVisible ? onRowSelected(item) : disableContextMenu()" @contextmenu="onRightClick($event, item)" @drag="dragThing($event,item)" @dragover.stop="dragThingOver($event,item)" @dragleave="dragThingLeave($event)" @drop.stop="dropThing($event)" draggable="true">
          <div class="d-flex">
            <div style="flex: 1; white-space: nowrap;" class="text-left">
              <div v-if="item.type === 'folder'">
                <i class="fa-solid fa-folder mr-2"></i>{{item.name}}
              </div>
              <div v-else-if="item.type === 'file'">
                <i v-if="item.name.includes('.pdf')" class="fa-solid fa-file-pdf mr-2"></i>
                <i v-else-if="checkIfImage(item.name)" class="fa-solid fa-file-image mr-2"></i>
                <i v-else-if="item.name.includes('.txt')" class="fa-solid fa-file-lines mr-2"></i>
                <i v-else-if="item.name.includes('.docx')" class="fa-solid fa-file-word mr-2"></i>
                <i v-else-if="item.name.includes('.ods')" class="fa-solid fa-file-excel mr-2"></i>
                <i v-else class="fa-solid fa-file mr-2"></i>
                {{item.name}}
              </div>
            </div>

            <div style="white-space: nowrap; flex: 1" class="text-left">
              {{item.lastModified}}
            </div>

            <div style="flex: 1; white-space: nowrap;" class="text-left">
              {{item.size}}
            </div>
          </div>
        </button>
      </b-row>

      <div v-if="isContextMenuVisible" style="z-index: 10; position: fixed; background-color: white; border: 1px solid rgb(230,230,230); min-width: 150px" :style="{top: contextMenuTop + 'px', left: contextMenuLeft + 'px'}">
        <button class="btn btn-block text-left context-menu-button" @click.stop="deleteFile">
          <i class="fa-solid fa-trash-can"></i>
          Delete
        </button>
      </div>
    </div>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
import {fileExplorerService} from "@/services/fileExplorerService";
export default {
  components: {
    CommonTable
  },
  props: {
    directory: null,
    selectedDirectory: null,
    files: []
  },
  data(){
    return {
      contextMenuTop: 0,
      contextMenuLeft: 0,
      isContextMenuVisible: false,
      rightClickedDirOrFile: null,
      dataToSend: {
        myName: '',
        myPath: [],
        myType: ''
      },

      isDragging: false,
      moveThing: {
        thingToMove: null,
        destinationForThing: null,
      }
    }
  },
  methods: {
    dragThing(event, item){
      event.preventDefault()
      this.moveThing.thingToMove = item
      this.isDragging = true
    },

    dragThingLeave(event){
      event.preventDefault()
      this.moveThing.thingToMove = null
    },

    dragThingOver(event, item){
      event.preventDefault()
      if(item !== this.moveThing.thingToMove && (item !== null && item.type === 'folder'))
        this.moveThing.destinationForThing = item
      else
        this.moveThing.destinationForThing = null
    },

    dropThing(event){
      event.preventDefault()
      if(this.moveThing.destinationForThing !== null) {
        let filePath = this.findFileDirectory(this.moveThing.thingToMove.name, this.directory.directoryName)[0].path
        let destinationPath = this.findDirectory(this.moveThing.destinationForThing.name)[0].path
        //console.log(this.moveThing)

        let move = {
          filePath: filePath,
          destinationPath: destinationPath,
          fileName: this.moveThing.thingToMove.name
        }

        fileExplorerService.moveFile(move)
            .then(result => this.$emit('reload'))
            .catch(error => console.error(error))
      }
      this.isDragging = false
    },

    checkIfImage(name){
      return (name.includes('jpg') || name.includes('jpeg') || name.includes('png'))
    },

    onRowSelected(item){
      if(item.type === 'folder')
        this.$emit('select-directory', item.actualObject)
    },
    onRightClick(event, item){
      event.preventDefault()
      if(item !== null){
        this.rightClickedDirOrFile = item
        this.contextMenuTop = event.clientY
        this.contextMenuLeft = event.clientX
        this.isContextMenuVisible = true
      }
    },
    disableContextMenu(){
      this.isContextMenuVisible = false
    },

    deleteFile(){
      if(this.rightClickedDirOrFile.type === 'file') {
        let formData = new FormData()

        formData.append('name', this.rightClickedDirOrFile.actualObject.fileName)
        formData.append('path', this.findFileDirectory(this.rightClickedDirOrFile.name, this.directory.directoryName)[0].path)
        formData.append('type', this.rightClickedDirOrFile.type)

        fileExplorerService.deleteDirOrFile(formData)
            .then(result => this.$emit('reload'))
            .catch(error => console.error(error))
      }

      this.disableContextMenu()
    },

    findFileDirectory(fileName, folder){
      const matchingDirectories = []

      function traverseDirectories(directories, path){
        for (const directory of directories) {
          // Construct the current directory's path
          const currentPath = path.concat(directory.directoryName);

          if (directory.files.find(file => file.fileName === fileName) !== null && directory.files.find(file => file.fileName === fileName) !== undefined && directory.directoryName === folder) {
            matchingDirectories.push({
              directory: directory,
              path: currentPath
            })
          }

          if (directory.directories.length > 0) {
            traverseDirectories(directory.directories, currentPath);
          }
        }
      }

      traverseDirectories(this.files, []);
      return matchingDirectories;
    },

    findDirectory(dir){
      const matchingDirectories = []

      function traverseDirectories(directories, path){
        for (const directory of directories) {
          // Construct the current directory's path
          const currentPath = path.concat(directory.directoryName);

          if (directory.directoryName === dir) {
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
    }
  },
  computed: {
    items(){
      let array = []

      if(this.directory !== null){
        this.directory.directories.forEach(dir => {
          let date = new Date(dir.lastModified)

          const day = String(date.getDate()).padStart(2,'0')
          const month = String(date.getMonth() + 1).padStart(2,'0')
          const year = String(date.getFullYear())

          const hours = String(date.getHours()).padStart(2, '0')
          const minutes = String(date.getMinutes()).padStart(2, '0')

          const formattedDate = `${day}/${month}/${year} ${hours}.${minutes}`

          array.push({name: dir.directoryName, size: dir.size, lastModified: formattedDate, type: 'folder', actualObject: dir})
        })

        this.directory.files.forEach(file => {
          let date = new Date(file.lastModified)

          const day = String(date.getDate()).padStart(2,'0')
          const month = String(date.getMonth() + 1).padStart(2,'0')
          const year = String(date.getFullYear())

          const hours = String(date.getHours()).padStart(2, '0')
          const minutes = String(date.getMinutes()).padStart(2, '0')

          const formattedDate = `${day}/${month}/${year} ${hours}.${minutes}`

          array.push({name: file.fileName, size: file.size, lastModified: formattedDate, type: 'file', actualObject: file})
        })
      }else{
        console.log(this.directory)
      }

      return array
    },

    fields() {
      return [
        {
          key: 'name'
        },
        {
          key: 'lastModified',
        },
        {
          key: 'size'
        }
      ]
    }
  }
}
</script>

<style scoped>
.hover-row:hover{
  cursor: pointer;
}

.context-menu-button:focus {
  border: none;
  background-color: rgb(250,250,250);
}

.context-menu-button:hover {
  background-color: rgb(245,245,245);
}
</style>