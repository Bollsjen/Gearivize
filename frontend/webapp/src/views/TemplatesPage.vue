<template>
    <b-container fluid class="w-75 text-left" style="margin-bottom: 100px">
        <h1>Templates</h1>
        <common-table :items="templates"
                      :fields="fields"
                      striped
                      hover
                      pagination
                      :filter-properties="filterProperties"
                      borderless
                      :default-filter="['get_active']" />
        <template-modal ref="TemplateModal" />
    </b-container>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
import {templateService} from "@/services/templateService";
import TemplateModal from "@/components/templates/TemplatesModal.vue";
export default {
  components: {
    CommonTable,
    TemplateModal
  },
  props: {},
  data() {
    return {
      templates: [],
    }
  },
  methods: {
    getTemplates() {
      templateService.getAll()
          .then(result => this.templates = result.data)
          .catch(error => {
              console.log("fisk")
              console.log(error)
          })
    },
  },
  computed: {
    fields() {
      return [
        {
          key: 'id',
          sortable: true,
        },
        {
          key: 'name',
          sortable: true,
        },
        {
          key: 'value',
          sortable: true,
        },
        {
          key: 'actions',
          label: '',
          template: {
            head: [
              {
                icon: 'fa-plus',
                text: 'New template',
                placement: 'right',
                section: 'table',
                visible: this.$store.state.isAuthenticated.responsible,
                variant: 'success',
                size: 'sm',
                action: () => this.$refs['TemplateModal'].show('add', null)
              }
            ],
            cell: [
              {
                icon: 'fa-eye',
                placement: 'right',
                variant: 'primary',
                visible: true,
                size: 'sm',
                action: (data) => this.$refs['TemplateModal'].show('watch', data)
              },
              {
                icon: 'fa-pen-to-square',
                placement: 'right',
                variant: 'success',
                visible: this.$store.state.isAuthenticated.responsible,
                size: 'sm',
                action: (data) => this.$refs['TemplateModal'].show('edit', data)
              }
            ]
          }
        }
      ]
    }
  },
  mounted() {
    this.getTemplates()
  }
}
</script>