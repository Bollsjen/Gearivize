<template>
  <b-modal v-model="visible" centered size="lg">
    <template #modal-header>
      <div class="text-right w-100 d-flex flex-row justify-content-between">
        <h4>User</h4>
        <b-button v-if="purpose !== 'add' && purpose !== 'watch'" @click="deleteUser" variant="danger"><span style="margin-right: 7px">Delete</span><i class="fa-solid fa-trash"></i></b-button>
      </div>
    </template>

    <b-row>
      <b-col sm="6">
        <b-form-group label="Name">
          <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                        v-model="user.name"
                        :class="{error: $v.user.name.$error}">
          </b-form-input>

          <b-form-input v-else-if="purpose === 'watch'"
                        v-model="user.name"
                        :class="{error: $v.user.name.$error}"
                        disabled>
          </b-form-input>
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="E-mail">
          <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                        v-model="user.email"
                        :class="{error: $v.user.email.$error}">
          </b-form-input>

          <b-form-input v-else-if="purpose === 'watch'"
                        v-model="user.email"
                        disabled>
          </b-form-input>
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="Password">
          <b-form-input v-if="purpose === 'add'"
                        v-model="user.password"
                        :class="{error: $v.password.$error}">
          </b-form-input>
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="Password">
          <b-form-input v-if="purpose === 'add'"
                        v-model="user.password"
                        :class="{error: $v.password.$error}">
          </b-form-input>
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="Responsible">
          <b-checkbox v-if="purpose === 'add' || purpose === 'edit'"
                        v-model="user.responsible"
                        switch>
          </b-checkbox>

          <b-checkbox v-else-if="purpose === 'watch'"
                        v-model="user.responsible"
                        switch
                        disabled>
          </b-checkbox>
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="Super user">
          <b-checkbox v-if="purpose === 'add' || purpose === 'edit'"
                      v-model="user.superUser"
                      switch>
          </b-checkbox>

          <b-checkbox v-else-if="purpose === 'watch'"
                      v-model="user.superUser"
                      switch
                      disabled>
          </b-checkbox>
        </b-form-group>
      </b-col>
    </b-row>

    <template #modal-footer>
      <div v-if="purpose === 'add'">
        <b-button variant="secondary" @click="close" style="margin-right: 10px">Cancel</b-button>
        <b-button variant="success" type="submit" @click="addInstrument">Add</b-button>
      </div>
      <div v-else-if="purpose === 'edit'">
        <b-button variant="secondary" @click="close" style="margin-right: 10px">Cancel</b-button>
        <b-button variant="success" type="submit" @click="updateInstrument">Update</b-button>
      </div>
      <div v-else-if="purpose === 'watch'">
        <b-button variant="secondary" @click="close" style="margin-right: 10px">Close</b-button>
      </div>
    </template>
  </b-modal>
</template>

<script>
import {useVuelidate} from "@vuelidate/core";
import {required} from "vuelidate/lib/validators";

export default {
  setup(){
    return {v$: useVuelidate()}
  },
  components: {

  },
  props: {

  },
  data(){
    return {
      visible: false,
      purpose: 'watch',
      user:{
        type: Object,
        default: () => {}
      },
      defaultUser: {
        name: '',
        email:'',
        password: '',
        reponsible: false,
        superUser: false
      }
    }
  },
  validations(){
    return {
      user: {
        name: {
          required
        },
        email: {
          required
        },
      },
      password: {
        required
      }
    }
  },
  methods: {
    show(purpose, user){
      this.purpose = purpose
      this.visible = true
      this.user = user != null ? user : this.defaultUser
    },

    close(){
      this.visible = false
    },

    deleteUser(){

    },

    submit(){

    }
  },
  computed: {
    repeatPassword(){

    }
  },
  mounted(){

  }
}
</script>