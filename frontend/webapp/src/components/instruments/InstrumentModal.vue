<template>
  <b-modal v-model="active" centered size="xl">
    <template #modal-header>
      <div class="text-right w-100 d-flex flex-row justify-content-between">
        <b-button v-if="!instrument.inactive && purpose !== 'add' && purpose !== 'watch'" @click="actualDeleteInstrument" variant="danger"><span style="margin-right: 7px">Delete</span><i class="fa-solid fa-trash"></i></b-button>
        <b-button v-if="!instrument.inactive && purpose !== 'add' && purpose !== 'watch'" @click="deleteInstrument" variant="warning"><span style="margin-right: 7px">Make inactive</span><i class="fa-solid fa-eye-slash"></i></b-button>
        <b-button v-else-if="purpose !== 'add' && purpose !== 'watch'" @click="reactivateInstrument" variant="primary" class="ml-auto"><span style="margin-right: 7px">Make active</span><i class="fa-solid fa-circle-check"></i></b-button>
      </div>
    </template>

    <b-row style="padding: 25px">
      <b-col sm="4">
        <b-row>
          <b-link href="#" @click="browseFileClick" class="w-100">
            <div v-if="instrument.image !== ''" class="w-100">
              <img v-bind:src="imageURL" style="width: 100%; object-fit: cover" @load="imageLoaded" />
            </div>
            <div v-else-if="imageUpload">
              <img v-bind:src="imageUpload" style="width: 100%; object-fit: cover" />
            </div>
            <div v-else class="w-100 d-flex justify-content-center">
              <svg style="width: 90%" fill="#999999" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--! Font Awesome Pro 6.4.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M448 80c8.8 0 16 7.2 16 16V415.8l-5-6.5-136-176c-4.5-5.9-11.6-9.3-19-9.3s-14.4 3.4-19 9.3L202 340.7l-30.5-42.7C167 291.7 159.8 288 152 288s-15 3.7-19.5 10.1l-80 112L48 416.3l0-.3V96c0-8.8 7.2-16 16-16H448zM64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V96c0-35.3-28.7-64-64-64H64zm80 192a48 48 0 1 0 0-96 48 48 0 1 0 0 96z"/></svg>
            </div>
          </b-link>
          <input type="file" ref="instrument-image-upload" style="display: none" @change="imagePrivew"/>
        </b-row>

        <b-row>
          <b-col sm="12" class="text-center mt-3">
              <b-button class="w-50" variant="primary"><span style="margin-right: 7px">Certificates</span><i class="fa-solid fa-arrow-right"></i></b-button>
          </b-col>
          <b-col sm="12" class="text-center mt-3">
              <b-button class="w-50" variant="primary"><span style="margin-right: 7px">Manuals</span><i class="fa-solid fa-arrow-right"></i></b-button>
          </b-col>
        </b-row>

        <b-row class="mt-3">
          <div class="w-100 text-center">
            <h3 style="font-weight: bold; text-align: center">Comment:</h3>
          </div>
          <p v-if="purpose === 'watch'">{{instrument.comment}}</p>
          <b-textarea v-else v-model="instrument.comment" rows="5" placeholder="Write a comment..."></b-textarea>
        </b-row>
      </b-col>

      <b-col sm="8">
          <b-row class="pl-5 w-100">
              <b-form-group label="A-Number" class="col-sm-6">
                  <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                :value="purpose === 'add' ? 'Auto generated' : instrument.aNumber"
                                disabled>
                  </b-form-input>

                  <b-form-input v-if="purpose === 'watch'"
                                v-model="instrument.aNumber"
                                required
                                disabled>
                  </b-form-input>
              </b-form-group>


              <b-form-group label="Name" class="col-sm-6">
                  <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                v-model="instrument.instrumentName"
                                :class="{error: v$.instrument.instrumentName.$error}">
                  </b-form-input>

                  <b-form-input v-if="purpose === 'watch'"
                                v-model="instrument.instrumentName"
                                disabled>
                  </b-form-input>
                  <span class="text-danger" style="font-size: 14px" v-if="v$.instrument.instrumentName.$error">
                      {{v$.instrument.instrumentName.$errors[0].$message}}
                  </span>
              </b-form-group>


              <b-form-group label="Manufacturer" class="col-sm-6">
                  <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                v-model="instrument.manufacturer"
                                :class="{error: v$.instrument.manufacturer.$error}">
                  </b-form-input>

                  <b-form-input v-if="purpose === 'watch'"
                                v-model="instrument.manufacturer"
                                required
                                disabled>
                  </b-form-input>
                  <span class="text-danger" style="font-size: 14px" v-if="v$.instrument.manufacturer.$error">
                      {{v$.instrument.manufacturer.$errors[0].$message}}
                  </span>
              </b-form-group>


              <b-form-group label="Serial Number" class="col-sm-6">
                  <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                v-model="instrument.serialNumber"
                                required>
                  </b-form-input>

                  <b-form-input v-if="purpose === 'watch'"
                                v-model="instrument.serialNumber"
                                required
                                disabled>
                  </b-form-input>
              </b-form-group>


              <b-form-group label="Type" class="col-sm-6">
                  <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                v-model="instrument.type"
                                :class="{error: v$.instrument.type.$error}">
                  </b-form-input>

                  <b-form-input v-if="purpose === 'watch'"
                                v-model="instrument.type"
                                required
                                disabled>
                  </b-form-input>
                  <span class="text-danger" style="font-size: 14px" v-if="v$.instrument.type.$error">
                      {{v$.instrument.type.$errors[0].$message}}
                  </span>
              </b-form-group>


              <b-form-group label="Test" class="col-sm-6">
                  <b-select v-if="purpose === 'add' || purpose === 'edit'"
                            v-model="instrument.test"
                            :class="{error: v$.instrument.test.$error}">
                      <option v-for="(test, index) in testTypes" :key="index" :value="index">
                          {{test}}
                      </option>
                  </b-select>

                  <b-form-input v-if="purpose === 'watch'"
                                v-model="instrument.test"
                                required
                                disabled>
                  </b-form-input>
                  <span class="text-danger" style="font-size: 14px" v-if="v$.instrument.test.$error">
                      {{v$.instrument.test.$errors[0].$message}}
                  </span>
              </b-form-group>


              <b-form-group label="Last calibreated" class="col-sm-6">
                  <b-form-datepicker v-if="purpose === 'add' || purpose === 'edit'"
                                     v-model="instrument.lastCalibratedDate"
                                     :class="{error: v$.instrument.lastCalibratedDate.$error}"
                                     @input="onLastCalibrationDateChange">
                  </b-form-datepicker>

                  <b-form-datepicker v-if="purpose === 'watch'"
                                     v-model="instrument.lastCalibratedDate"
                                     required
                                     disabled>
                  </b-form-datepicker>
                  <span class="text-danger" style="font-size: 14px" v-if="v$.instrument.lastCalibratedDate.$error">
                      {{v$.instrument.lastCalibratedDate.$errors[0].$message}}
                  </span>
              </b-form-group>


              <b-form-group label="Next calibration" class="col-sm-6">
                  <b-form-datepicker v-if="purpose === 'add' || purpose === 'edit'"
                                     v-model="instrument.nextCalibrationDate"
                                     :class="{error: v$.instrument.nextCalibrationDate.$error}">
                  </b-form-datepicker>

                  <b-form-datepicker v-if="purpose === 'watch'"
                                     v-model="instrument.nextCalibrationDate"
                                     required
                                     disabled>
                  </b-form-datepicker>
                  <span class="text-danger" style="font-size: 14px" v-if="v$.instrument.nextCalibrationDate.$error">
                      {{v$.instrument.nextCalibrationDate.$errors[0].$message}}
                  </span>
              </b-form-group>



              <b-form-group label="Value" class="col-sm-6">
                  <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                v-model="instrument.value"
                                type="number">
                  </b-form-input>

                  <b-form-input v-if="purpose === 'watch'"
                                v-model="instrument.value"
                                required
                                disabled>
                  </b-form-input>
              </b-form-group>



              <b-form-group label="Primary Responsible" class="col-sm-6">
                  <b-form-select v-if="purpose === 'add' || purpose === 'edit'"
                                 v-model="instrument.userId"
                                 :class="{error: v$.instrument.userId.$error}"
                                 :options="users"
                                 :value-field="'id'"
                                 :text-field="'name'">
                      <template #first>
                          <option :value="null">Select primary responsible</option>
                      </template>
                  </b-form-select>

                  <b-select v-if="purpose === 'watch'"
                            v-model="instrument.test"
                            :class="{error: v$.instrument.test.$error}"
                            disabled>
                      <option v-for="(user, index) in users" :key="index" :value="user.id">
                          {{user.name}}
                      </option>
                  </b-select>
                  <span class="text-danger" style="font-size: 14px" v-if="v$.instrument.userId.$error">
                      {{v$.instrument.userId.$errors[0].$message}}
                  </span>
              </b-form-group>



              <b-form-group label="Internal calibration" class="col-sm-6">
                  <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                v-model="instrument.internalCalibration">
                  </b-form-input>

                  <b-form-input v-if="purpose === 'watch'"
                                v-model="instrument.internalCalibration"
                                required
                                disabled>
                  </b-form-input>
              </b-form-group>



              <b-form-group label="Secondary Responsible" class="col-sm-6">
                  <b-form-select v-if="purpose === 'add' || purpose === 'edit'"
                                 v-model="instrument.userId2"
                                 :options="users"
                                 :value-field="'id'"
                                 :text-field="'name'">
                      <template #first>
                          <option :value="null">Select secondary responsible</option>
                      </template>
                  </b-form-select>

                  <b-select v-if="purpose === 'watch'"
                            v-model="instrument.test"
                            disabled>
                      <option v-for="(user, index) in users" :key="index" :value="user.id">
                          {{user.name}}
                      </option>
                  </b-select>
              </b-form-group>



              <b-form-group label="External calibration" class="col-sm-6">
                  <b-checkbox v-if="purpose === 'add' || purpose === 'edit'"
                              v-model="instrument.externalCalibration"
                              type="number"
                              switch>
                  </b-checkbox>

                  <b-checkbox v-if="purpose === 'watch'"
                              v-model="instrument.externalCalibration"
                              disabled
                              switch>
                  </b-checkbox>
              </b-form-group>



              <b-form-group label="Needs calibration?" class="col-sm-6">
                  <b-checkbox v-if="purpose === 'add' || purpose === 'edit'"
                              v-model="instrument.needsCalibration"
                              type="number"
                              switch>
                  </b-checkbox>

                  <b-checkbox v-if="purpose === 'watch'"
                              v-model="instrument.needsCalibration"
                              disabled
                              switch>
                  </b-checkbox>
              </b-form-group>
          </b-row>
      </b-col>
    </b-row>

    <template #modal-footer>
      <div v-if="purpose === 'add'">
        <b-button variant="secondary" @click="hide" style="margin-right: 10px">Cancel</b-button>
        <b-button variant="success" type="submit" @click="addInstrument">Add</b-button>
      </div>
      <div v-else-if="purpose === 'edit'">
        <b-button variant="secondary" @click="hide" style="margin-right: 10px">Cancel</b-button>
        <b-button variant="success" type="submit" @click="updateInstrument">Update</b-button>
      </div>
    </template>
  </b-modal>
</template>

<script>
import {testTypes} from "@/types/TestTypes";
import {instrumentsService} from "@/services/instrumentsService";
import {userService} from "@/services/userService";
import {required} from "vuelidate/lib/validators";
import { useVuelidate } from '@vuelidate/core'
import {computed, reactive} from "vue";
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
      active: false,
      imageUpload: null,
      imageURL: null,
      imageFile: null,
      purpose: '',
      testTypes: testTypes,
      users: [],
      instruments: [],
      instrument: {},
      defaultInstrument: {
        aNumber: 'A0',
        calibrationReportNumber: '',
        comment: '',
        creationTime: new Date(),
        externalCalibration: false,
        image: '',
        inactive: false,
        instrumentName: '',
        internalCalibration: '',
        lastCalibratedDate: new Date(),
        manufacturer: '',
        nextCalibrationDate: new Date(new Date().setFullYear(new Date().getFullYear() + 2)).toISOString().slice(0, 10),
        note: '',
        referenceCalibrationInstruction: '',
        serialNumber: '',
        test: 0,
        testTemplateId: null,
        type: '',
        userId: null,
        userId2: null,
        value: 0,
        needsCalibration: true,
      },
    }
  },
  validations () {
    return {
      instrument: {
        instrumentName: {
          required: required
        },
        lastCalibratedDate: {
          required
        },
        manufacturer: {
          required
        },
        nextCalibrationDate: {
          required
        },
        test: {
          required
        },
        type: {
          required
        },
        userId: {
          required
        }
      }
    }
  },
  methods: {
    imageLoaded(){
      URL.revokeObjectURL(this.imageURL)
    },
    load(){
      userService.getAll()
          .then(result => this.users = result.data)
          .catch(error => console.error(error))
    },
    show(purpose, instrument){
      const date = new Date().setFullYear(new Date().getFullYear() + 2)
      const twoYearsFromNow = new Date(date)
      this.active = true
      this.purpose = purpose
      this.instrument = instrument != null ? instrument : this.defaultInstrument

      instrumentsService.getImage(this.instrument.image)
          .then(result => {
            this.imageURL = URL.createObjectURL(result.data)
          })
          .catch(error => console.log(error))
    },

    hide(){
      this.v$.$reset()
      this.active = false
    },

    addInstrument(){
      this.v$.$touch()
      if(!this.v$.$error){
        instrumentsService.createNewInstrument(this.instrument)
            .then(result => {
              this.instruments.push(result.data)
              if(this.imageFile !== null){
                let formData = new FormData()
                formData.append("file", this.imageFile)
                instrumentsService.uploadInstrumentImage(formData, result.data.aNumber)
                    .then(result => {
                      console.log("Upload success")
                      this.imageUpload = null
                      this.hide()
                    })
                    .catch(error => {
                        console.log(error)
                        this.$bvModal.msgBoxOk(error.status, {
                          title: 'Error',
                          size: 'sm',
                          buttonSize: 'sm',
                          okVariant: 'success',
                          centered: true
                        });
                    })
              }else{
                this.imageUpload = null
                this.hide()
              }
            })
            .catch(error => this.$bvModal.msgBoxOk(error.status, {
              title: 'Error',
              size: 'sm',
              buttonSize: 'sm',
              okVariant: 'success',
              centered: true
            }))
      }else {
        this.v$.$errors.forEach(error => {
          error.$message = "Required"
        })
      }
    },

    updateInstrument(){
      this.v$.$touch()
      if(!this.v$.$error){
        instrumentsService.updateInstrument(this.instrument)
            .then(result => {
              if(this.imageFile !== null){
                let formData = new FormData()
                formData.append("file", this.imageFile)
                instrumentsService.uploadInstrumentImage(formData, result.data.aNumber)
                    .then(result => {
                      console.log("Upload success")
                      this.imageUpload = null
                      this.hide()
                    })
                    .catch(error => {
                      console.log(error)
                      this.$bvModal.msgBoxOk(error.status, {
                        title: 'Error',
                        size: 'sm',
                        buttonSize: 'sm',
                        okVariant: 'success',
                        centered: true
                      });
                    })
              }else{
                this.imageUpload = null
                this.hide()
              }
            })
            .catch(error => this.$bvModal.msgBoxOk(error.status, {
              title: 'Error',
              size: 'sm',
              buttonSize: 'sm',
              okVariant: 'success',
              centered: true
            }))
      }else {
        this.v$.$errors.forEach(error => {
          error.$message = "Required"
        })
      }
    },

    reactivateInstrument(){
      this.instrument.inactive = false
      instrumentsService.activateInstrument(this.instrument)
          .then(result => this.hide())
          .catch(error => this.$bvModal.msgBoxOk(error.status, {
            title: 'Error',
            size: 'sm',
            buttonSize: 'sm',
            okVariant: 'success',
            centered: true
          }))
    },

    deleteInstrument(){
      this.instrument.inactive = true
      instrumentsService.deleteInstrument(this.instrument.aNumber)
          .then(result => this.hide())
          .catch(error => this.$bvModal.msgBoxOk(error.status, {
            title: 'Error',
            size: 'sm',
            buttonSize: 'sm',
            okVariant: 'success',
            centered: true
          }))
    },

    actualDeleteInstrument(){
      instrumentsService.actualDeleteInstrument(this.instrument.aNumber)
          .then(result => this.hide())
          .catch(error => this.$bvModal.msgBoxOk(error.status, {
            title: 'Error',
            size: 'sm',
            buttonSize: 'sm',
            okVariant: 'success',
            centered: true
          }))
    },

    onLastCalibrationDateChange(){
      this.instrument.nextCalibrationDate = new Date(new Date(this.instrument.lastCalibratedDate).setFullYear(new Date(this.instrument.lastCalibratedDate).getFullYear() + 2)).toISOString().slice(0, 10)
    },

    browseFileClick(){
      this.$refs['instrument-image-upload'].click()
    },

    imagePrivew(e){
      this.imageFile = e.target.files[0]
      const reader = new FileReader()
      reader.readAsDataURL(this.imageFile)
      reader.onload = e => {
        this.imageUpload = e.target.result
      }
    }
  },
  computed: {
  },
  mounted(){
    this.load()
  }
}
</script>

<style lang="scss" scoped>
.image-container {
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center;
}

.error {
  border: solid 1px red;
}
</style>