<template>
  <apexchart type="bar" :options="options" :series="series" style="height: 100%"></apexchart>
</template>

<script>
import VueApexCharts from "vue-apexcharts";
export default {
  components: {
    VueApexCharts
  },
  props: {
    distributed: {
      type: Boolean,
      default: () => false
    },
    colors: {
      type: Array,
      default: () => ['#00a8ff']
    },
    columns: {
      type: Array,
      default: () => []
    },
    data: {
      type: Array,
      default: () => [],
    }
  },
  data() {
    return {

    }
  },
  methods: {},
  computed: {
    series() {
      return [
        {
          data: this.data
        }
      ]
    },

    options() {
      return {
        chart: {
          height: 550,
          type: 'bar',
          toolbar: {
            show: false
          }
        },
        colors:this.colors,
        plotOptions: {
          bar: {
            columnWidth: '45%',
            distributed: this.distributed,
          }
        },
        dataLabels: {
          enabled: false
        },
        legend: {
          show: false
        },
        xaxis: {
          categories: this.columns,
          labels: {
            style: {
              fontSize: '18px'
            }
          }
        }
      }
    },
  },
  mounted() {

  }
}

</script>