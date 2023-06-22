<template>
  <b-container class="text-left mt-5">
      <b-list-group>
        <b-list-group-item style="background-color: rgb(240,240,240)"><h1 style="font-size: 24px" class="m-0">Super user</h1></b-list-group-item>
        <b-list-group-item to="/users" class="super-user-hover">Users</b-list-group-item>
        <b-list-group-item to="/import" class="super-user-hover">Import</b-list-group-item>
      </b-list-group>
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

<style>
.super-user-hover:hover {
  background-color: rgb(240,240,240);
}
</style>