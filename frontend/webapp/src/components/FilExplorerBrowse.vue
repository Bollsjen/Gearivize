<template>
  <div style="display: flex; flex-wrap: wrap; width: 100%">
    <b-row>

      <b-col sm="12" class="d-flex">
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

      <button class="col-12 btn btn-block" v-for="item in items" @click="onRowSelected(item)">
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

      <!--<b-col sm="12">
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
      </b-col>-->
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
  },
  data(){
    return {
    }
  },
  methods: {
    checkIfImage(name){
      return (name.includes('jpg') || name.includes('jpeg') || name.includes('png'))
    },

    onRowSelected(item){
      if(item.type === 'folder')
        this.$emit('select-directory', item.actualObject)
    },
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
</style>