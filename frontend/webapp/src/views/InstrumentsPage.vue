<template>
  <b-container fluid class="w-75 text-left" style="margin-bottom: 100px">
    <h1>Instruments</h1>
    <common-table :items="instruments" :fields="fields" striped hover pagination :filter-properties="filterProperties" borderless />
    <instrument-modal ref="InstrumentModal" />
  </b-container>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
import {instrumentsService} from "@/services/instrumentsService";
import InstrumentModal from "@/components/instruments/InstrumentModal.vue";
import {testTypes} from "@/types/TestTypes";
export default {
  components: {
    CommonTable,
    InstrumentModal
  },
  props: {

  },
  data(){
    return {
      instruments: [],
      testTypes: testTypes,
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
          key: 'manufacturer',
          sortable: true
        },
        {
          key: 'type',
          sortable: true
        },
        {
          key: 'test',
          formatter: (data) => {return testTypes[data.test]},
          sortable: true
        },
        {
          key: 'daysLeft',
          formatter: (data) => {
            if(data){
              const today = new Date()
              const diff = new Date(data.nextCalibrationDate).getTime() - today.getTime()
              const days = Math.ceil(diff / (1000 * 3600 * 24))
              return days
            }
            return 0
          },
          sortable: true
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
                icon: 'fa-plus',
                text: 'New instrument',
                placement: 'right',
                variant: 'success',
                size: 'sm',
                action: (data) => this.$refs['InstrumentModal'].show('add',null)
              }
            ],
            cell: [
              {
                icon: 'fa-eye',
                placement: 'right',
                variant: 'primary',
                size: 'sm',
                action: (data) => this.$refs['InstrumentModal'].show('watch',data)
              },
              {
                icon: 'fa-pen-to-square',
                placement: 'right',
                variant: 'success',
                size: 'sm',
                action: (data) => this.$refs['InstrumentModal'].show('edit',data)
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