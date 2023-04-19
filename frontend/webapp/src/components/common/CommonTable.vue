<template>
  <div>
    <b-row v-if="filterProperties.length > 0">
      <b-col lg="6" class="my-1 mb-0">
        <b-form-group
            description="Leave all checked to filter on all data"
            label-cols-sm="3"
            label-align-sm="right"
            label-size="sm"
            class="mb-0"
            v-slot="{ ariaDescribedby }"
        >
          <b-form-checkbox-group
              v-model="filterOn"
              :aria-describedby="ariaDescribedby"
              class="mt-1 d-flex flex-row"
          >
            <b-form-checkbox v-for="(filter, index) in filterProperties" :key="index" :value="index" style="margin-right: 5px"><span style="margin-left: 4px">{{filter.label}}</span></b-form-checkbox>
          </b-form-checkbox-group>
        </b-form-group>
      </b-col>
    </b-row>
    <b-table
        :striped="striped"
        :bordered="bordered"
        :borderless="borderless"
        :outlined="outlined"
        :small="small"
        :hover="hover"
        :dark="dark"
        :fixed="fixed"
        :responsive="resposive"
        :sticky-header="stickyHeader"
        :stacked="stacked"
        :caption-top="captionTop"
        :table-variant="tableVariant"
        :head-variant="headVariant"
        :foot-variant="footVariant"
        :foot-clone="footClone"
        :no-footer-sorting="noFooterSorting"
        :no-border-collapse="noBorderCollapse"

        :per-page="perPage"
        :current-page="currentPage"

        :fields="fields"
        :items="filterItems"
        :sort-by.sync="sortBy"
        :sort-desc.sync="sortDesc">
      <template #head(actions)="data">
        {{data.label}}
      </template>

      <template #cell(actions)="data">
        <div :class="getPlacement(data.field.template.cell[0].placement)">
          <b-button v-for="(cell, index) in data.field.template.cell"
                    :striped="striped"
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
    <b-row>
      <div v-if="pagination" class="d-flex justify-content-between align-items-center">
        <div class="d-flex align-items-center">
          <b-button-group size="sm">
            <b-button key="10" variant="outline-secondary" :class="perPage === 10 ? 'active' : ''" @click="changePageLimit(10)">10</b-button>
            <b-button key="25" variant="outline-secondary" :class="perPage === 25 ? 'active' : ''" @click="changePageLimit(25)">25</b-button>
            <b-button key="50" variant="outline-secondary" :class="perPage === 50 ? 'active' : ''" @click="changePageLimit(50)">50</b-button>
          </b-button-group>
        </div>
        <b-pagination
            v-model="currentPage"
            :total-rows="totalRows"
            :per-page="perPage"
            align="fill"
            size="sm"
            class="my-0"
        ></b-pagination>
      </div>
    </b-row>
  </div>
</template>

<script>
export default {
  components: {

  },
  props: {
    fields: Array,
    items: Array,
    filterProperties: {
      type: Array,
      default: [],
    },

    striped: {
      type: Boolean,
      default: false
    },

    bordered: {
      type: Boolean,
      default: false,
    },

    borderless: {
      type: Boolean,
      default: false,
    },

    outlined: {
      type: Boolean,
      default: false,
    },

    small: {
      type: Boolean,
      default: false,
    },

    hover: {
      type: Boolean,
      default: false,
    },

    dark: {
      type: Boolean,
      default: false
    },

    fixed: {
      type: Boolean,
      default: false
    },

    captionTop: {
      type: Boolean,
      default: false,
    },

    footClone: {
      type: Boolean,
      default: false,
    },

    noFooterSorting: {
      type: Boolean,
      default: false,
    },

    noBorderCollapse: {
      type: Boolean,
      default: false,
    },

    resposive:{
      type: [Boolean, String],
      default: false
    },

    stickyHeader: {
      type: [Boolean, String],
      default: false,
    },

    stacked: {
      type: [Boolean, String],
      default: false,
    },

    tableVariant: {
      type: String,
      default: '',
    },

    headVariant: {
      type: String,
      default: '',
    },

    footVariant: {
      type: String,
      default: '',
    },

    pagination: {
      type: Boolean,
      default: false
    }
  },
  data(){
    return {
      sortBy: '',
      sortDesc: false,
      currentPage: 0,
      perPage: 10,
      filterOn: [],
      filterItems: []
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
    },
    changePageLimit(limit){
      this.perPage = limit
    },

    filterItemsList(){
      this.filterItems = this.items.filter(item => {
        return this.filterOn.some(index => {
          const prop = this.filterProperties[index]
          if (prop && prop.key in item) {
            return prop.value === item[prop.key]
          }
          return false
        })
      })
    },
  },
  computed: {
    checkedFilters() {
      return this.filterProperties.filter((filter, index) => this.filterOn[index]);
    },

    totalRows(){
      return this.filterItems.length
    },
  },
  mounted() {
    this.pagination === true ? this.perPage = 10 : this.perPage = 900719925474099
    this.filterProperties ? this.filterProperties.forEach(filter => filter.default > 0 ?this.filterOn.push(filter.default) : null) : null
  },
  watch: {
    filterOn(){
      this.filterItemsList()
    },
    items(){
      this.filterProperties.length > 0 ? this.filterItemsList() : this.filterItems = this.items
    }
  }
}
</script>

<style>
.active {
  background-color: #0d6efd;
}
</style>