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
          <b-collapse :id="(directory.directoryName).replace(' ', '')" :visible="false">
            <file-explorer-file-tree
                :directories="directory.directories"
                :show-files="showFiles"
                :is-parent="false"
                :in-tabbing="(inTabbing + 1)"
                @select-directory="selectDirectory"
                :do-in-tapping="doInTapping">
            </file-explorer-file-tree>
          </b-collapse>
        </template>
      </div>
    </div>


  <!--
      <div v-if="displayType === 'list'" v-for="directory in directories" :key="directory.directoryName">
        <button v-b-toggle="directory.directoryName + '' + directory.size"
                style="width: 100%"
                class="btn btn-block text-left custom-directory-button-styling"
                :style="doInTapping ? 'padding-left:' + (Number(10) + Number((25 * inTabbing))).toString() + 'px' : ''"
                @click="selectDirectory(directory)">
          <i class="fa-solid fa-folder mr-2"></i>{{ directory.directoryName }}
        </button>
        <template v-if="directory.directories.length > 0">
          <b-collapse :id="directory.directoryName + '' + directory.size" :visible="false">
            <file-explorer-file-tree :directories="directory.directories" :show-files="showFiles" :is-parent="false" :in-tabbing="(inTabbing + 1)" @select-directory="selectDirectory" :do-in-tapping="doInTapping"></file-explorer-file-tree>
          </b-collapse>
        </template>
        <template v-if="directory.files.length > 0">
            <div v-if="showFiles === true"
                 v-for="file in directory.files"
                 :key="file.fileName"
                 :style="doInTapping ? 'padding-left:' + (Number(10) + Number((25 * inTabbing))).toString() + 'px' : ''">
              {{ file.fileName }}
            </div>
        </template>
      </div>-->
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
    }
  },
  methods: {
    selectDirectory(directory){
      this.$emit('select-directory', directory)
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