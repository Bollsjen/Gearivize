<template>
  <div style="display: flex; flex-wrap: wrap; width: 100%">
    <b-row>
      <b-col sm="12" class="d-flex">
        <div v-for="(dir, index) in path">
          <b-link>
            {{dir}}
          </b-link>
          <span v-if="index !== path.length-1">/</span>
        </div>
      </b-col>

      <b-col sm="12">
        <b-table
            :items="items"
            :fields="fields"
            borderless
            hover
            @row-clicked="onRowSelected"
            ref="ExplorerTable">

          <template #cell(name)="data">
            <div v-if="data.item.type === 'folder'">
              <i class="fa-solid fa-folder mr-2"></i>{{data.item.name}}
            </div>
            <div v-else-if="data.item.type === 'file'">
              <i v-if="data.item.name.includes('.pdf')" class="fa-solid fa-file-pdf"></i>
              <i v-else-if="checkIfImage(data.item.name)" class="fa-solid fa-file-image"></i>
              <i v-else-if="data.item.name.includes('.txt')" class="fa-solid fa-file-lines"></i>
              <i v-else-if="data.item.name.includes('.docx')" class="fa-solid fa-file-word"></i>
              <i v-else-if="data.item.name.includes('.ods')" class="fa-solid fa-file-excel"></i>
              <i v-else class="fa-solid fa-file"></i>
              {{data.item.name}}
            </div>
          </template>

        </b-table>
      </b-col>
    </b-row>
  </div>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
export default {
  components: {
    CommonTable
  },
  props: {
    directory: null,
    selectedDirectory: null,
    path: []
  },
  data(){
    return {
    }
  },
  methods: {
    checkIfImage(name){
      return (name.includes('jpg') || name.includes('jpeg') || name.includes('png'))
    },

    onRowSelected(items){
      if(items.type === 'folder')
        this.$emit('select-directory', items.actualObject)
    },
  },
  computed: {
    items(){
      let array = []

      if(this.directory !== null){
        this.directory.directories.forEach(dir => {
          array.push({name: dir.directoryName, size: dir.size, lastModified: dir.lastModified, type: 'folder', actualObject: dir})
        })

        this.directory.files.forEach(file => {
          array.push({name: file.fileName, size: file.size, lastModified: file.lastModified, type: 'file', actualObject: file})
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
</style>