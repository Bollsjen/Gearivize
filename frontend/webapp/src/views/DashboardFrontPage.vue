<template>
  <b-container fluid class="w-75 text-start">
    <b-row class="mb-5">
      <b-col sm="12" class="pl-0 pr-0 mt-5">
        <b-card class="shadow rounded col-sm-12">
          <b-row>
            <b-col sm="12">
              <h2>Overdue</h2>
            </b-col>
            <b-col sm="4" class="w-100 d-flex justify-content-center">
              <gauge-chart background-color="#57E216" foreground-color="#E51616" label="Active" :percentage="activePercentage"  style="width: 75%"/>
            </b-col>
            <b-col sm="4" class="w-100 d-flex justify-content-center">
              <gauge-chart background-color="#57E216" foreground-color="#E51616" label="External calibration" :percentage="externalPercentage" style="width: 75%" />
            </b-col>
            <b-col sm="4" class="w-100 d-flex justify-content-center">
              <gauge-chart background-color="#57E216" foreground-color="#E51616" label="Internal calibration" :percentage="internalPercentage" style="width: 75%;" />
            </b-col>
          </b-row>
        </b-card>
      </b-col>

      <b-col sm="6" class="mt-5 pl-0">
        <b-row>
          <b-col sm="12" class="pr-0">
            <b-card class="shadow-lg">
              <h3>Next calibration - overview</h3>
              <column-chart :columns="columnChartColumns" :data="columnChartData" distributed :colors="['#e84118', '#e84118', '#fbc531', '#00a8ff', '#00a8ff', '#4cd137', '#4cd137']" />
            </b-card>
          </b-col>

          <b-col sm="12" class="mt-3 pr-0">
            <b-card class="shadow-lg">
              <h4>Instrument total value</h4>
              <common-table :fields="instrumentsTotalValueFields" :items="instrumentsTotalValue" responsive />
            </b-card>
          </b-col>
        </b-row>
      </b-col>

      <b-col sm="6" class="mt-5 pr-3">
        <b-row>
          <b-col sm="12" class="mt-0 pr-0">
            <b-card class="shadow-lg">
              <h5>External clibration - within 3 months</h5>
              <common-table :fields="fields" :items="externalCalibrationOverdue" pagination :page-sizes="[5]" responsive />
            </b-card>
          </b-col>

          <b-col sm="12" class="mt-3 pr-0">
            <b-card class="shadow-lg">
              <h5>Internal clibration - within 1 month</h5>
              <common-table :fields="fields" :items="internalCalibrationOverdue" pagination :page-sizes="[5]" responsive />
            </b-card>
          </b-col>
        </b-row>
      </b-col>

      <b-col sm="6" class="mt-5 pr-0 pb-0 pl-0">
        <b-card class="shadow-lg">
          <h3>External calibration - overdue</h3>
          <common-table :fields="fields" :items="externalCalibrationWithin3Months" pagination :page-sizes="[5]" responsive />
        </b-card>
      </b-col>

      <b-col sm="6" class="mt-5 pr-0 mb-5">
        <b-card class="shadow-lg">
          <h3>Internal calibration - overdue</h3>
          <common-table :fields="fields" :items="internalCalibrationWithin1Month" pagination :page-sizes="[5]" responsive />
        </b-card>
      </b-col>


    </b-row>
  </b-container>
</template>

<script>
import GaugeChart from "@/components/charts/GaugeChart.vue";
import commonTable from "@/components/common/CommonTable.vue";
import {instrumentsService} from "@/services/instrumentsService";
import {testTypes} from "@/types/TestTypes";
import ColumnChart from "@/components/charts/ColumnChart.vue";
export default {
    components: {
      GaugeChart,
      ColumnChart,
      commonTable
    },
  props: {

  },
  data(){
      return {
        instruments: [],
        externalCalibration: [],
        internalCalibration: [],
        testTypes: testTypes
      }
  },
  methods: {
      load(){
        instrumentsService.getAll()
            .then(result => {
              this.instruments = result.data
              //this.instrumentsTotalValue()
            })
            .catch(error => this.$bvModal.msgBoxOk(error.status, {
              title: 'Error',
              size: 'sm',
              buttonSize: 'sm',
              okVariant: 'success',
              centered: true
            }))
      },
  },
  computed: {
    columnChartColumns(){
      return ['< -7','-7 - 0', '1 - 7', '8 - 14', '15 - 30', '31 - 45', '46 - 90']
    },

    instrumentsTotalValue(){
      let initialVal = 0
      let active = this.instruments.filter(i => !i.inactive).reduce((acc, currentValue) => acc + currentValue.value, initialVal)
      initialVal = 0
      let inactive = this.instruments.filter(i => i.inactive).reduce((acc, currentValue) => acc + currentValue.value, initialVal)

      initialVal = 0
      let everything = this.instruments.reduce((acc, currentValue) => acc + currentValue.value, initialVal)

      return [
        {
          name: 'Active',
          value: active
        },
        {
          name: 'Inactive',
          value: inactive
        },
        {
          name: 'All',
          value: everything
        }
      ]
    },

    instrumentsTotalValueFields(){
      return [
        {
          key: 'name',
          label: 'Category'
        },
        {
          key: 'value',
          label: 'Total'
        },
      ]
    },

    columnChartData(){
      if(this.instruments.length <= 0) return null
        let today = new  Date()
      let todayMinus7 = new Date().setDate(new Date().getDate() - 7)
      console.log(new Date(today).toLocaleString('en-GB', {timeZone: 'Europe/Copenhagen'}))
      console.log(new Date().toLocaleString('en-GB', {timeZone: 'Europe/Copenhagen'}))
        let overdue = this.instruments.filter(i =>
            new Date(i.nextCalibrationDate) < todayMinus7 &&
            i.needsCalibration &&
            !i.inactive
        ).length

      let justOverdue = this.instruments.filter(i =>
          i.needsCalibration &&
          !i.inactive &&
          new Date(i.nextCalibrationDate) >= todayMinus7 &&
          new Date(i.nextCalibrationDate) <= today
      ).length

      let todayPlus1 = new Date().setDate(new Date().getDate() + 1)
      let todayPlus7 = new Date().setDate(new Date().getDate() + 7)
      let oneTo7Days = this.instruments.filter(i =>
          i.needsCalibration &&
          !i.inactive &&
          new Date(i.nextCalibrationDate) >= todayPlus1 &&
          new Date(i.nextCalibrationDate) <= todayPlus7
      ).length

      let todayPlus8 = new Date().setDate(new Date().getDate() + 8)
      let todayPlus14 = new Date().setDate(new Date().getDate() + 14)
      let eightTo14Days = this.instruments.filter(i =>
          i.needsCalibration &&
          !i.inactive &&
          new Date(i.nextCalibrationDate) >= todayPlus8 &&
          new Date(i.nextCalibrationDate) <= todayPlus14
      ).length

      let todayPlus15 = new Date().setDate(new Date().getDate() + 15)
      let todayPlus30 = new Date().setDate(new Date().getDate() + 30)
      let fifeteenTo30Days = this.instruments.filter(i =>
          i.needsCalibration &&
          !i.inactive &&
          new Date(i.nextCalibrationDate) >= todayPlus15 &&
          new Date(i.nextCalibrationDate) <= todayPlus30
      ).length

      let todayPlus31 = new Date().setDate(new Date().getDate() + 31)
      let todayPlus45 = new Date().setDate(new Date().getDate() + 45)
      let thirtyOneTo45Days = this.instruments.filter(i =>
          i.needsCalibration &&
          !i.inactive &&
          i.externalCalibration &&
          new Date(i.nextCalibrationDate) >= todayPlus31 &&
          new Date(i.nextCalibrationDate) <= todayPlus45
      ).length

      let todayPlus46 = new Date().setDate(new Date().getDate() + 46)
      let todayPlus90 = new Date().setDate(new Date().getDate() + 90)
      let fourtySixTo90Days = this.instruments.filter(i =>
          i.needsCalibration &&
          !i.inactive &&
          i.externalCalibration &&
          new Date(i.nextCalibrationDate) >= todayPlus46 &&
          new Date(i.nextCalibrationDate) <= todayPlus90
      ).length

      let result = [overdue, justOverdue, oneTo7Days, eightTo14Days, fifeteenTo30Days, thirtyOneTo45Days, fourtySixTo90Days]

      console.log(result)

      return result
    },

      fields(){
        return[
          {
            key: 'aNumber',
            label: 'A-Number',
            sortable: true,
          },
          {
            key: 'instrumentName',
            label: 'Instrument',
            sortable: true,
          },
          {
            key: 'manufacturer',
            sortable: true,
          },
          {
            key: 'test',
            formatter: (data) => {return testTypes[data.test]},
            sortable: true
          },
          {
            key: 'type',
            sortable: true,
          }
        ]
      },

    externalCalibrationWithin3Months(){
      let today = new Date()
      let in3Months = new Date().setDate(today.getDate() + 97)
      return this.instruments.filter(i =>
          new Date(i.nextCalibrationDate) > today &&
          new Date(i.nextCalibrationDate) <= in3Months &&
          i.externalCalibration &&
          i.needsCalibration &&
          !i.inactive
      )
    },

    internalCalibrationWithin1Month(){
      let today = new Date()
      let in1Months = new Date().setDate(today.getDate() + 37)
      return this.instruments.filter(i =>
          new Date(i.nextCalibrationDate) > today &&
          new Date(i.nextCalibrationDate) <= in1Months &&
          !i.externalCalibration &&
          i.needsCalibration &&
          !i.inactive
      )
    },

    externalCalibrationOverdue(){
      let today = new Date()
      return this.instruments.filter(i =>
          new Date(i.nextCalibrationDate) <= today &&
          i.externalCalibration &&
          i.needsCalibration &&
          !i.inactive
      )
    },

    internalCalibrationOverdue(){
      let today = new Date()
      return this.instruments.filter(i =>
          new Date(i.nextCalibrationDate) <= today &&
          !i.externalCalibration &&
          i.needsCalibration &&
          !i.inactive
      )
    },

    activePercentage(){
      let today = new Date()
      let Overdue = this.instruments.filter(instrument =>
          today >= new Date(instrument.nextCalibrationDate) &&
          !instrument.inactive &&
          instrument.needsCalibration
      ).length
      return Overdue > 0 ? Number(((Overdue + 0.0001) / (this.instruments.filter(i => i.needsCalibration && !i.inactive).length + 0.0001) * 100).toFixed(0)) : 0
    },

    externalPercentage(){
      let today = new Date()
      let Overdue = this.instruments.filter(instrument =>
          today >= new Date(instrument.nextCalibrationDate) &&
          instrument.externalCalibration &&
          !instrument.inactive &&
          instrument.needsCalibration
      ).length
      return Overdue > 0 ? Number((((Overdue + 0.00001) / (this.instruments.filter(i => i.externalCalibration && i.needsCalibration && !i.inactive).length + 0.00001)) * 100).toFixed(0)) : 0
    },

    internalPercentage(){
      let today = new Date()
      let noneOverdue = this.instruments.filter(instrument =>
          today >= new Date(instrument.nextCalibrationDate) &&
          !instrument.externalCalibration &&
          !instrument.inactive &&
          instrument.needsCalibration
      ).length
      return noneOverdue > 0 ? Number((((noneOverdue + 0.00001) / (this.instruments.filter(i => !i.externalCalibration && i.needsCalibration && !i.inactive).length) + 0.00001) * 100).toFixed(0)) : 0
    }
  },
  mounted(){
      this.load()
  }
}
</script>