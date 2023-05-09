<template>
  <b-container fluid class="w-75 text-left">
    <authenticator minimum-requirement="superUser" />
    <b-row>
      <b-col sm="12">
        <h1 style="margin: 25px 0px;">Super user</h1>
        <hr/>
      </b-col>
      <b-col sm="12">
        <h3>Users</h3>
      </b-col>
      <b-col sm="12">
        <common-table :items="users" :fields="fields" />
      </b-col>
    </b-row>
    <user-modal ref="UserModal" />
  </b-container>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
import {userService} from "@/services/userService";
import UserModal from "@/components/users/UserModal.vue";
import Authenticator from "@/components/authentication/Authenticator.vue";
export default {
  components: {
    UserModal,
    CommonTable,
    Authenticator
  },
  props: {

  },
  data(){
    return {
      users: {
        type: Array,
        default: () => []
      }
    }
  },
  methods: {
    load(){
      userService.getAll()
          .then(result => this.users = result.data)
          .catch(error => this.$bvModal.msgBoxOk(error.status, {
            title: 'Error',
            size: 'sm',
            buttonSize: 'sm',
            okVariant: 'success',
            centered: true
          }))
    }
  },
  computed: {
    fields(){
      return [
        {
          key: 'name',
        },
        {
          key: 'email'
        },
        {
          key: 'responsible'
        },
        {
          key: 'superUser'
        },
        {
          key: 'actions',
          label: '',
          template: {
            head: [
              {
                icon: 'fa-plus',
                text: 'New instrument',
                placement: 'right',
                section: 'table',
                visible: true,
                variant: 'success',
                size: 'sm',
                action: () => this.$refs['UserModal'].show('add',null)
              }
            ],
            cell: [
              {
                icon: 'fa-eye',
                placement: 'right',
                variant: 'primary',
                visible: true,
                size: 'sm',
                action: (data) => this.$refs['UserModal'].show('watch',data)
              },
              {
                icon: 'fa-pen-to-square',
                placement: 'right',
                variant: 'success',
                visible: true,
                size: 'sm',
                action: (data) => this.$refs['UserModal'].show('edit',data)
              },
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