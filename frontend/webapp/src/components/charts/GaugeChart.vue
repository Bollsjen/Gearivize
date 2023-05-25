<template>
  <div>
    <apexchart type="radialBar" :options="chartOptions" :series="series"></apexchart>
    <h3>{{label}}</h3>
  </div>
</template>

<script>
import VueApexCharts from "vue-apexcharts";
export default {
  components: {
    VueApexCharts
  },
  props: {
    backgroundColor: '',
    foregroundColor: '',
    dash: 0,
    label: '',
    percentage: {
      type: Number,
      default: 0,
    }
  },
  data(){
    return {

    }
  },
  methods: {

  },
  computed: {
    series(){
      return[
          this.percentage
      ]
    },

    chartOptions(){
      return {
          chart: {
            type: 'radialBar',
            offsetY: -20,
            sparkline: {
              enabled: true
            }
          },
          plotOptions: {
            radialBar: {
              startAngle: -120,
              endAngle: 120,
              track: {
                background: this.backgroundColor,
                strokeWidth: '100%',
                margin: 5, // margin is in pixels
              },
              dataLabels: {
                name: {
                  show: false
                },
                value: {
                  offsetY: -2,
                  fontSize: '22px'
                }
              }
            }
          },
        stroke: {
            dashArray: this.dash
        },
        colors:[this.foregroundColor],
          label: this.label,
      }
    }
  },
  mounted(){

  }
}
</script>