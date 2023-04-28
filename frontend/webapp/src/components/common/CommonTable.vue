<template>
  <div>
    <div class="common-table-top-container w-100">
      <b-row v-if="filterProperties.length > 0" class="w-100">
        <b-col sm="3">
          <b-form-group
              label-cols-sm="0"
              label-align-sm="right"
              label-size="sm"
              class="mb-0 text-left w-100 mt-1"
              v-slot="{ ariaDescribedby }"
          >
            <b-form-checkbox-group
                v-model="filterOn"
                :aria-describedby="ariaDescribedby"
                class="mt-1 d-flex flex-row"
            >
              <b-form-checkbox v-for="(filter, index) in filterProperties" :key="index" :value="index" style="margin-right: 10px"><span>{{filter.label}}</span></b-form-checkbox>
            </b-form-checkbox-group>
          </b-form-group>
        </b-col>
        <b-col sm="6">
          <b-form-input v-model="search" placeholder="Search items..."></b-form-input>
        </b-col>
        <b-col sm="3" class="text-right" v-if="actionsField">
          <b-button variant="success" size="sm" class="mt-1" v-for="item in actionsField.template.head" @click="item.action()" v-if="item.section === 'filter' && item.visible">
            <span style="margin-right: 7px">{{item.text}}</span>
            <i class="fa-solid fa-plus"></i>
          </b-button>
        </b-col>
      </b-row>
    </div>
    <div class="common-table-container">
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

          thead-class="text-left"
          tbody-class="text-left"
          class="w-100"

          :per-page="perPage"
          :current-page="currentPage"

          :fields="fields"
          :items="filterItems"
          show-empty
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc">
        <template #empty="scope">
          <div class="my-3 text-center">
            <span>No items to display</span>
          </div>
        </template>

        <template #head(actions)="data">
          <div :class="getPlacement(data.field.template.head[0].placement)" class="text-left" v-if="data.field.template.head">
            <div v-for="(cell, index) in data.field.template.head" v-if="cell.section === 'table' && cell.visible" :key="index">
              <b-button v-if="!cell.label"
                        :key="index"
                        :variant="cell.variant"
                        :size="cell.size"
                        style="margin-left: 5px"
                        :class="getPlacement(cell.placement)"
                        @click="cell.action(data.item)">
                <span v-if="cell.text" style="margin-right: 7px">{{cell.text}}</span>
                <i class="fa-solid" :class="cell.icon"></i>
              </b-button>
              <span v-else>{{cell.label}}</span>
            </div>
          </div>
        </template>

        <template v-for="item in fields" v-slot:[`cell(${item.key})`]="data">
          <div v-if="data.item">
            <span v-if="item.formatter">{{item.formatter(data.item)}}</span>
            <span v-else>{{data.item[item.key]}}</span>
          </div>
        </template>

        <template #cell(actions)="data">
          <div :class="getPlacement(data.field.template.cell[0].placement)" class="text-left">
            <b-button v-for="(cell, index) in data.field.template.cell"
                      v-if="cell.visible"
                      :key="index"
                      :variant="cell.variant"
                      :size="cell.size"
                      style="margin-left: 5px"
                      :class="getPlacement(cell.placement)"
                      @click="cell.action(data.item)">
              <span v-if="cell.text" style="margin-right: 7px">{{cell.text}}</span>
              <i class="fa-solid" :class="cell.icon"></i>
            </b-button>
          </div>
        </template>

      </b-table>
      <b-row style="margin-bottom: 10px;" class="w-100">
        <div v-if="pagination" class="d-flex justify-content-between align-items-center w-100">
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
      filterItems: [],
      search: '',
    }
  },
  methods: {
    getPlacement(placement){
      if(placement === 'left'){
        return 'text-left'
      }else if (placement === 'right'){
        return 'text-right'
      }
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

    filterSearch(){
      this.filterProperties.length > 0 ? this.filterItemsList() : this.filterItems = this.items
      let itemProperties = Object.keys(this.filterItems[0])
      let regex = new RegExp(this.search, 'g')
      this.filterItems = this.filterItems.filter(item =>
          {
            for(let i = 0; i < itemProperties.length; i++){
              let prop = itemProperties[i]
              if(prop.toLowerCase().includes("id")) continue
              let value = item[prop]
              if(value != null && value.toString().toLocaleLowerCase().includes(this.search.toLowerCase()) ||
                  (typeof value == 'number' && value.toString().match(regex))){
                return true
              }
            }
            return false
          })
    },
  },
  computed: {
    mapFilterdItemsKeys(){
      return Object.keys(this.filterItems)
    },
    checkedFilters() {
      return this.filterProperties.filter((filter, index) => this.filterOn[index]);
    },

    totalRows(){
      return this.filterItems.length
    },

    actionsField(){
      let actions = this.fields.find(field => field.key === 'actions')
      return actions
    }
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
      console.log(this.filterItems)
    },

    search(){
      this.filterSearch()
    }
  }
}
</script>

<style>
.common-table-top-container {
  background-color: #F7F7F7;
  padding: 12px;
  border-radius: 10px;
  margin-bottom: 20px;
}

.common-table-container {
  background-color: #F7F7F7;
  padding: 12px;
  border-radius: 10px;
  margin-bottom: 20px;
}
</style>