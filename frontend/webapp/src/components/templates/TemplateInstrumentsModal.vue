<template>
    <common-table :fields="fields1" :items="instruments" selectable :row-selected="rowSelected" />
</template>

<script lang="ts">
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
            selectedInstruments: []
        },
    };
</script>

<style>
</style>