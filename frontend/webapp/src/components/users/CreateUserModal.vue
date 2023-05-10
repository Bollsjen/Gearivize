<template>
  <b-modal v-model="visible" centered size="lg">
    <template #modal-header>
      <div class="text-right w-100 d-flex flex-row justify-content-between">
        <h4>User</h4>
      </div>
    </template>

    <b-row>
      <b-col sm="6">
        <b-form-group label="Name">
          <b-form-input
                        v-model="user.name"
                        vuelidate
                        :class="{error: v$.user.name.$error}">
          </b-form-input>

          <vuelidate :v-model="v$.user.name" />
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="E-mail">
          <b-form-input
                        v-model="user.email"
                        vuelidate
                        :class="{error: v$.user.email.$error}">
          </b-form-input>
          <vuelidate :v-model="v$.user.email" />
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="Password">
          <b-form-input
                        v-model="user.password"
                        vuelidate
                        type="password"
                        :class="{error: v$.user.password.$error}">
          </b-form-input>
          <vuelidate :v-model="v$.user.password" />
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="Repeat Password">
          <b-form-input
                        v-model="repeatPassword"
                        vuelidate
                        type="password"
                        :class="{error: v$.repeatPassword.$error}">
          </b-form-input>
          <vuelidate :v-model="v$.repeatPassword" message="Passwords not identical" />
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="Responsible">
          <b-checkbox
                      v-model="user.responsible"
                      switch>
          </b-checkbox>
        </b-form-group>
      </b-col>

      <b-col sm="6">
        <b-form-group label="Super user">
          <b-checkbox
                      v-model="user.superUser"
                      switch>
          </b-checkbox>
        </b-form-group>
      </b-col>
    </b-row>

    <template #modal-footer>
      <div>
        <b-button variant="secondary" @click="close" style="margin-right: 10px">Cancel</b-button>
        <b-button variant="success" @click="submit">Add</b-button>
      </div>
    </template>
  </b-modal>
</template>

<script>
import {useVuelidate} from "@vuelidate/core";
import {required, minLength, sameAs} from "vuelidate/lib/validators";
import {userService} from "@/services/userService";
import Vuelidate from "@/components/Vuelidate.vue";
import {email, requiredIf} from "@vuelidate/validators";
import {withParams} from "vuelidate";

export default {
  setup(){
    return {v$: useVuelidate()}
  },
  components: {
    Vuelidate
  },
  props: {
  },
  data(){
    return {
      visible: false,
      user: {
        type: Object,
        default: ()=> {}
      },
      defaultUser: {
        id: '2d210a74-419d-4705-a1ec-820274894c6b',
        name: '',
        email: '',
        password: '',
        responsible: false,
        superUser: false,
        createDate: new Date().toISOString()
      },
      repeatPassword: '',
    }
  },
  validations(){
    return {
      user: {
        name: {
          required
        },
        email: {
          required,
          email
        },
        password: {
          required
        },
      },
      repeatPassword: {
        required: requiredIf(function() {return false}),
        sameAsPassword: sameAs(function(){
          return this.user.password
        }),
      }
    }
  },
  methods: {
    show(){
      this.visible = true
    },

    close(){
      this.visible = false
      this.v$.$reset()
      console.log("Close")
    },

    submit(){
      this.v$.$touch()
      if(this.v$.$errors.length === 0){
          userService.createUser(this.user)
              .then(result => {
                this.$emit('add-user',result.data)
                this.close()
              })
              .catch(error => this.$bvModal.msgBoxOk(error.status, {
                title: 'Error',
                size: 'sm',
                buttonSize: 'sm',
                okVariant: 'success',
                centered: true
              }))
      }else{
        console.log(this.v$.$errors)
      }

    }
  },
  computed: {
  },
  watch: {
    'this.v$.user.name.$errors'(){
      console.log("ting")
    }
  },
  mounted(){

  }
}
</script>

<style>
.error {
  border: 1px solid red;
}
</style>