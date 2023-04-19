<template>
  <b-container fluid class="w-75 text-start">
    <h1>Instruments</h1>
    <common-table :items="instruments" :fields="fields" />
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
      instruments: Array,
    }
  },
  methods: {
    getInstruments(){
      instrumentsService.getAll()
          .then(result => this.instruments = result.data)
          .catch(error => console.log(error))
    }
  },
  computed: {
    fields(){
      return [
        {
          key: 'aNumber'
        },
        {
          key: 'instrumentName'
        },
        {
          key: 'value'
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