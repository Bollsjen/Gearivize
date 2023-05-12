<template>
  <b-container fluid class="w-75 text-left" style="margin-bottom: 100px">
    <h1>Instruments</h1>
    <common-table :items="instruments" :fields="fields" striped hover pagination :filter-properties="filterProperties" borderless :default-filter="['get_active']" />
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
          condition: (item) => {return item.inactive === true},
          value: 'get_inactive',
          label: 'Inactive',
        },
        {
          condition: (item) => {return item.inactive === false && item.needsCalibration === true},
          value: 'get_active',
          label: 'Active',
        },
        {
          condition: (item) => {return item.needsCalibration === false},
          value: 'get_need_calibration',
          label: 'No calibration',
        }
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
            //
            //  For at tilføje et knapper til headeren af common table laves et array kaldet head
            //  I dette array laves objekter. Hvert objekt vil blive til en knap
            //  En knap kan opbygges af:
            //    label:  tekst en kolonne som andre kolonner
            //    text: knap tekst
            //    placement: left, center, right
            //    variant: baggrundsfarven for knappen
            //    size: bootstrap størrelser som sm, md osv.
            //    action: tager en metode
            //
            head: [
              {
                icon: 'fa-plus',
                text: 'New instrument',
                placement: 'right',
                section: 'table',
                visible: this.$store.state.isAuthenticated.responsible,
                variant: 'success',
                size: 'sm',
                action: () => this.$refs['InstrumentModal'].show('add',null)
              }
            ],

            //
            //  For at tilføje et knapper til cellerne af common table laves et array kaldet cell
            //  I dette array laves objekter. Hvert objekt vil blive til en knap
            //  En knap kan opbygges af:
            //    text: knap tekst
            //    placement: left, center, right
            //    variant: baggrundsfarven for knappen
            //    size: bootstrap størrelser som sm, md osv.
            //    action: tager en metode. En metode kan tage én parameter vil blive cellens celle data (data.field for field data og data.item for cellens element fra items arrayet)
            //
            cell: [
              {
                icon: 'fa-eye',
                placement: 'right',
                variant: 'primary',
                visible: true,
                size: 'sm',
                action: (data) => this.$refs['InstrumentModal'].show('watch',data)
              },
              {
                icon: 'fa-pen-to-square',
                placement: 'right',
                variant: 'success',
                visible: this.$store.state.isAuthenticated.responsible,
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
    //this.filterOn = this.filterProperties.map()
  }
}
</script>