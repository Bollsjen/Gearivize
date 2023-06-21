<template>
    <b-container fluid class="w-75 text-left" style="margin-bottom: 100px">
        <h1>Templates</h1>
        <common-table :items="templates"
                      :fields="fields"
                      striped
                      hover
                      pagination
                      :filter-properties="filterProperties"
                      borderless />
        <templates-modal ref="TemplatesModal" />
        </b-container>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
import {templateService} from "@/services/templateService";
import TemplatesModal from "@/components/templates/TemplatesModal.vue";
export default {
  components: {
    CommonTable,
    TemplatesModal
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
                action: () => this.$refs['TemplatesModal'].show('add', null)
              }
            ],
            cell: [
              {
                icon: 'fa-eye',
                placement: 'right',
                variant: 'primary',
                visible: true,
                size: 'sm',
                action: (data) => this.$refs['TemplatesModal'].show('watch', data)
              },
              {
                icon: 'fa-pen-to-square',
                placement: 'right',
                variant: 'success',
                visible: this.$store.state.isAuthenticated.responsible,
                size: 'sm',
                action: (data) => this.$refs['TemplatesModal'].show('edit', data)
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