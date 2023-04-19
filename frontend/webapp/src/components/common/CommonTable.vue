<template>
  <b-table :fields="fields" :items="items">
    <template #head(actions)="data">
      {{data.label}}
    </template>

    <template #cell(actions)="data">
      <div :class="getPlacement(data.field.template.cell[0].placement)">
        <b-button v-for="(cell, index) in data.field.template.cell"
                  :key="index"
                  :variant="cell.variant"
                  :size="cell.size"
                  style="margin-left: 5px"
                  :class="getPlacement(cell.placement)"
                  @click="cell.action(data.item)">
          <i class="fa-solid" :class="cell.icon"></i>
        </b-button>
      </div>
    </template>

  </b-table>
</template>

<script>
export default {
  components: {

  },
  props: {
    fields: Array,
    items: Array,
  },
  data(){
    return {
    }
  },
  methods: {
    getPlacement(placement){
      if(placement === 'left'){
        console.log('start')
        return 'text-start'
      }else if (placement === 'right'){
        console.log('end')
        return 'text-end'
      }
      console.log('center | ' + placement)
      return 'text-center'
    }
  },
  computed: {

  },
  mounted() {
  }
}
</script>