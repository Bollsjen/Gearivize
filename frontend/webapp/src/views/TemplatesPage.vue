<template>
  <b-container fluid class="w-75 text-start">
    <h1>Templates</h1>

    <common-table :fields="fields1" :items="instruments" selectable :row-selected="rowSelected"/>

    <common-table :fields="fields2" :items="selectedInstruments" />
  </b-container>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
import {instrumentsService} from "@/services/instrumentsService";

export default {
  components: {
    CommonTable
  },
  props: {

  },
  data(){
    return {
      instruments: [],
      selectedInstruments: []
    }
  },
  methods: {
    load(){
      instrumentsService.getAll()
          .then(result => this.instruments = result.data)
          .catch(error => console.error(error))
    },

    rowSelected(data){
      this.selectedInstruments = data
    }
  },
  computed: {
    fields1(){
      return [
        {
          key: 'selected',
          label: ''
        },
        {
          key: 'aNumber'
        },
        {
          key: 'instrumentName',
        },
        {
          key: 'serialNumber'
        }
      ]
    },

    fields2(){
      return [
        {
          key: 'aNumber'
        },
        {
          key: 'instrumentName',
        },
        {
          key: 'serialNumber'
        }
      ]
    }
  },
  mounted() {
    this.load()
  }
}
</script>