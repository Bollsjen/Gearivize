<template>
  <div style="display: flex; flex-wrap: wrap">
    <common-table :items="items" :fields="fields" :filter-properties="[]" style="flex: 1" />
  </div>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
export default {
  components: {
    CommonTable
  },
  props: {
    directory: {
      type: Object,
      required: true
    },
  },
  computed: {
    items(){
      let array = []

      if(this.directory === null && this.directory !== {}){
        this.directory.directories.forEach(dir => {
          array.push({name: dir.directoryName, size: dir.size, lastModified: dir.lastModified})
        })

        this.directory.files.forEach(file => {
          array.push({name: file.fileName, size: file.size, lastModified: file.lastModified})
        })
      }

      return array
    },

    fields() {
      return [
        {
          key: 'name'
        },
        {
          key: 'lastModified'
        },
        {
          key: 'size'
        }
      ]
    }
  }
}
</script>