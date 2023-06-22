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
    <user-modal ref="UserModal" @delete-user="deleteUser" />
    <create-user-modal ref="CreateUserModal" @add-user="addUser"/>
  </b-container>
</template>

<script>
import CommonTable from "@/components/common/CommonTable.vue";
import {userService} from "@/services/userService";
import UserModal from "@/components/users/UserModal.vue";
import CreateUserModal from "@/components/users/CreateUserModal.vue";
import Authenticator from "@/components/authentication/Authenticator.vue";
export default {
  components: {
    UserModal,
    CommonTable,
    CreateUserModal,
    Authenticator
  },
  props: {
  },
  data(){
    return {
      users: []
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
    },

    deleteUser(userToDelete){
      let theUser = null
      this.users.forEach(user => {
        if(user.id === userToDelete.id){
          theUser = user
        }
      })

      this.users = this.users.filter(user => user !== theUser)
    },

    addUser(user){
      this.users.push(user)
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
                action: () => this.$refs['CreateUserModal'].show()
              }
            ],
            cell: [
              {
                icon: 'fa-eye',
                placement: 'right',
                variant: 'primary',
                visible: true,
                size: 'sm',
                action: (data) => this.$refs['UserModal'].show('watch',data, null)
              },
              {
                icon: 'fa-pen-to-square',
                placement: 'right',
                variant: 'success',
                visible: true,
                size: 'sm',
                action: (data) => this.$refs['UserModal'].show('edit',data,null)
              },
            ]
          }
        }
      ]
    }
  },
  mounted(){
    this.load()
  },
  watch: {
    users () {
      console.log(this.users)
    }
  }
}
</script>