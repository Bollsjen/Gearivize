<template>
  <div :class="isParent ? 'responsive' : ''">
      <div v-for="directory in directories"
           :key="directory.directoryName"
           style="display: flex; flex-wrap: wrap">
        <button v-b-toggle="(directory.directoryName).replace(' ', '')"
                style="width: 100%; white-space: nowrap; flex-grow: 1; flex: 100%"
                class="btn btn-block text-left custom-directory-button-styling"
                :style="doInTapping ? 'padding-left:' + (Number(10) + Number((25 * inTabbing))).toString() + 'px' : ''"
                @click="selectDirectory(directory)">
          <i class="fa-solid fa-folder mr-2"></i>{{ directory.directoryName }}
        </button>
        <template v-if="directory.directories.length > 0">
          <b-collapse :id="!isParent ? (directory.directoryName).replace(' ', '') : ''" :ref="(directory.directoryName).replace(' ', '')" :visible="!isParent ? getVisiblility(directory.directoryName) : isParent">
            <file-explorer-file-tree
                :directories="directory.directories"
                :show-files="showFiles"
                :is-parent="false"
                :in-tabbing="(inTabbing + 1)"
                @select-directory="selectDirectory"
                :do-in-tapping="doInTapping"
                :path-prop="path">
            </file-explorer-file-tree>
          </b-collapse>
        </template>
      </div>
  </div>
</template>

<script>
export default {
  name: 'FileExplorerFileTree',
  props: {
    directories: {
      type: Array,
      required: true
    },
    showFiles: {
      type: Boolean,
      default: true
    },
    isParent: {
      type: Boolean,
      default: true,
    },
    inTabbing: {
      type: Number,
      default: 0
    },
    doInTapping: {
      type: Boolean,
      default: false
    },
    selectedDirectory: {
      type: Object,
      default: null
    },
    pathProp: [],
  },
  data(){
    return {
      path: [],
      collapseVisible: {},
      displayCollapse: false,
    }
  },
  methods: {
    selectDirectory(directory){
      this.$emit('select-directory', directory)

    },

    findInstrumentDirectory(searchDir){

      const matchingDirectories = []

      function traverseDirectories(directories, path){
        for (const directory of directories) {
          // Construct the current directory's path
          const currentPath = path.concat(directory.directoryName);

          if (directory.directoryName === searchDir) {
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

      traverseDirectories(this.directories, []);
      return matchingDirectories;
    },

    isCollapseVisible(directoryName) {
      return this.collapseVisibility[directoryName] || false;
    },
  },
  mounted() {
    if(this.selectedDirectory !== null) {
      this.path = this.findInstrumentDirectory(this.selectedDirectory.directoryName)[0].path
    }
  },
  computed: {
    getVisiblility(){
      return (name) => {
        if (this.pathProp.includes(name)) {
          return true
        }
        return false
      }
    }
  },
  watch: {
    selectedDirectory(){
      this.path = this.findInstrumentDirectory(this.selectedDirectory.directoryName)[0].path
    }
  }
}
</script>

<style scoped>
.responsive {
  overflow-x: auto;
}

.custom-directory-button-styling:focus {
  outline: none;
  box-shadow: none;
}

.custom-directory-button-styling:hover {
  background-color: rgb(240,240,240);
}
</style>