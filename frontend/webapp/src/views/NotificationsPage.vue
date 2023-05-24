<template>
  <b-container fluid class="w-75">
    <h1>Notifications</h1>
    <common-table :items="items" :fields="fields" />
  </b-container>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
import {notificationService} from "@/services/notificationService";
import {instrumentsService} from "@/services/instrumentsService";
export default {
  components: {
    CommonTable
  },
  props: {

  },
  data(){
    return {
      items: [],
      instrumenter: []
    }
  },
  methods: {
    load(){
      notificationService.getAll()
          .then(result => {
            this.items = result.data
            console.log(result.data)
          })
          .catch(error => console.error(error))

      instrumentsService.getAll()
          .then(result => {
            this.instrumenter = result.data
          })
          .catch(error => console.error(error))
    },

    react(item){
      item.hasReacted = true
      notificationService.updateNotification(item)
          .then(result => console.log("sucess"))
          .catch(error => console.error(error))
    }
  },
  computed: {
    fields(){
      return [
        {
          key: 'aNumber'
        },
        {
          key: 'hasReacted'
        },
        {
          key: 'creationDate'
        },
        {
          key: 'nextCalibrationDate',
          formatter: (data) => {
            if(data !== null) {
              let instrument = this.instrumenter.find(instrument => instrument.aNumber === data.aNumber)
              let date = new Date(instrument.nextCalibrationDate)

              let displayDate = `${date.getDate()}/${date.getMonth()+1}/${date.getFullYear()}`

              return displayDate
            }
            return ''
          }
        },
        {
          key: 'actions',
          label: 'React',
          template: {
            cell: [
              {
                icon: 'fa-check',
                size: 'sm',
                placement: 'right',
                visible: this.$store.state.isAuthenticated.responsible,
                variant: 'success',
                action: (data) => this.react(data)
              }
            ]
          }
        }
      ]
    }
  },
  mounted(){
    this.load()
  }
}
</script>