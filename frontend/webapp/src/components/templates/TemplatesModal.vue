<template>
    <b-modal v-model="active" centered size="xl">
        <template #modal-header>
            <div class="text-right w-100 d-flex flex-row justify-content-between">
                <b-button v-if="purpose !== 'add' && purpose !== 'watch'" @click="deleteTemplate" variant="danger"><span style="margin-right: 7px">Delete</span><i class="fa-solid fa-trash"></i></b-button>
            </div>
        </template>
        <b-row style="padding: 25px">
            <b-col sm="8">
                <b-row class="pl-5 w-100">
                    <b-form-group label="Id" class="col-sm-6">
                        <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                      :value="purpose === 'add' ? 'Auto generated' : template.id"
                                      disabled>
                        </b-form-input>

                        <b-form-input v-if="purpose === 'watch'"
                                      v-model="template.id"
                                      required
                                      disabled>
                        </b-form-input>
                    </b-form-group>


                    <b-form-group label="Name" class="col-sm-6">
                        <b-form-input v-if="purpose === 'add' || purpose === 'edit'"
                                      v-model="template.name"
                                      :class="{error: v$.template.name.$error}">
                        </b-form-input>

                        <b-form-input v-if="purpose === 'watch'"
                                      v-model="template.name"
                                      disabled>
                        </b-form-input>
                        <span class="text-danger" style="font-size: 14px" v-if="v$.template.name.$error">
                            {{v$.template.name.$errors[0].$message}}
                        </span>
                    </b-form-group>
                </b-row>
            </b-col>
        </b-row>

        <template #modal-footer>
            <div v-if="purpose === 'add'">
                <b-button variant="secondary" @click="hide" style="margin-right: 10px">Cancel</b-button>
                <b-button variant="success" type="submit" @click="addTemplate">Add</b-button>
            </div>
            <div v-else-if="purpose === 'edit'">
                <b-button variant="secondary" @click="hide" style="margin-right: 10px">Cancel</b-button>
                <b-button variant="success" type="submit" @click="updateTemplate">Update</b-button>
            </div>
            <div v-else-if="purpose === 'watch'">
                <b-button variant="secondary" @click="hide" style="margin-right: 10px">Close</b-button>
            </div>
        </template>
    </b-modal>

</template>

<script>
    import { templateService } from '@/services/templateService';
    import { userService } from '@/services/userService';
    import { useVuelidate } from '@vuelidate/core'
    import { required } from '@vuelidate/validators'
    

    export default {
        setup() {
            return { v$: useVuelidate() }
        },
        components: {
        },
        directives: {
        },
        filters: {
        },
        props: {
        },
        data() {
            return {
                purpose: '',
                active: false,
                users: [],
                template: {},
                templates: [],
                defaultTemplate: {
                    id: '2d210a74-419d-4705-a1ec-820274894c6b',
                    name: '',
                },

            }
        },
        computed: {
        },
        watch: {
        },
        beforeCreate() {
        },
        created() {
        },
        beforeMount() {
        },
        mounted() {
            this.load()
        },
        updated() {
        },
        activated() {
        },
        deactivated() {
        },
        beforeDestroy() {
        },
        destroyed() {
        },
        methods: {
            show(purpose, template) {
                this.active = true;
                this.purpose = purpose
                this.template = template != null ? template : this.defaultTemplate
                console.log(template)
            },
            load() {
                userService.getAll()
                    .then(result => this.users = result.data)
                    .catch(error => console.error(error))
            },
            hide() {
                this.v$.$reset()
                this.active = false
            },
            addTemplate() {
                this.v$.$touch()
                if (!this.v$.$error) {
                    templateService.createTemplate(this.template)
                        .then(result => {
                            this.template.push(result.data)
                        })
                        .catch(error => this.$bvModal.msgBoxOk(error.status, {
                            title: 'Error',
                            size: 'sm',
                            buttonSize: 'sm',
                            okVariant: 'success',
                            centered: true
                        }))
                } else {
                    this.v$.$errors.forEach(error => {
                        error.$message = "Required"
                    })
                }
            },
            updateTemplate() {
                this.v$.$touch()
                if (!this.v$.$error) {
                    templateService.updateTemplate(this.template)
                        .then(result => {
                            this.template.push(result.data)
                        })
                        .catch(error => this.$bvModal.msgBoxOk(error.status, {
                            title: 'Error',
                            size: 'sm',
                            buttonSize: 'sm',
                            okVariant: 'success',
                            centered: true
                        }))
                } else {
                    this.v$.$errors.forEach(error => {
                        error.$message = "Required"
                    })
                }
            },
            deleteTemplate() {
                templateService.deleteTemplate(this.template.id)
                    .then(result => {
                        this.template.push(result.data)
                    })
                    .catch(error => this.$bvModal.msgBoxOk(error.status, {
                        title: 'Error',
                        size: 'sm',
                        buttonSize: 'sm',
                        okVariant: 'success',
                        centered: true
                    }))
            }
        },
        validations() {
            return {
                template: {
                    name: {
                        required
                    }
                }
            }
        }
    }
</script>

<style lang="scss" scoped>

.error {
  border: solid 1px red;
}
</style>