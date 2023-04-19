<template>
  <b-container fluid class="w-75 text-start">
    <h1>Instruments</h1>
    <common-table :items="instruments" :fields="fields" striped hover pagination :filter-properties="filterProperties" />
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
      filterProperties: [
        {
          key: 'inactive',
          value: true,
          label: 'Inactive',
          default: 0,
        },
        {
          key: 'inactive',
          value: false,
          label: 'Active',
          default: 1,
        },
      ]
    }
  },
  methods: {
    getInstruments(){
      instrumentsService.getAll()
          .then(result => this.instruments = this.sortInstruments(result.data))
          .catch(error => console.log(error))
    },
    sortInstruments(array){
      return array.sort(function(a,b){
        const aNumber = parseInt(a.aNumber.match(/\d+/)[0]);
        const bNumber = parseInt(b.aNumber.match(/\d+/)[0]);
        if (aNumber === bNumber) {
          return a.aNumber.localeCompare(b.aNumber, undefined, { numeric: true, sensitivity: 'base' });
        } else {
          return aNumber - bNumber;
        }
      })
    }
  },
  computed: {
    fields(){
      return [
        {
          key: 'aNumber',
          sortable: true,
        },
        {
          key: 'instrumentName',
          sortable: true,
        },
        {
          key: 'value',
          sortable: true,
        },
        {
          key: 'actions',
          label:'',
          template: {
            head: [
              {
                label:'ACTIONS'
              }
            ],
            cell: [
              {
                icon: 'fa-pen-to-square',
                placement: 'right',
                size: 'sm',
                action: (data) => alert(data.aNumber)
              },
              {
                icon: 'fa-pen-to-square',
                placement: 'right',
                size: 'sm',
                variant: 'danger',
                action: (data) => alert(data.instrumentName)
              }
            ]
          }
        }
      ]
    }
  },
  mounted() {
    this.getInstruments()
  }
}
</script>