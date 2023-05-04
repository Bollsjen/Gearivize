<style>
    .container {
        max-width: 300px;
        margin: 0 auto;
    }

    .form-group {
        margin-bottom: 20px;
    }

    label {
        display: block;
        margin-bottom: 5px;
    }

    input {
        width: 100%;
    }
</style>

<template>
    <div class="container">
        <form>
            <div class="form-group">
                <label for="username">Username</label>
                <input type="text" class="form-control" id="username" v-model="username">
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" class="form-control" id="password" v-model="password">
            </div>
            <button class="btn btn-primary" @click.prevent="login">Login</button>
        </form>
    </div>
</template>

<script>
    import { authService } from "../services/authService"
    export default {
        components: {

        },
        props: {

        },
        data() {
            return {
                username: "",
                password: ""
            }
        },
        methods: {
            login() {
                authService.login(this.username, this.password)
                    .then(result => {
                        console.log(result)
                        this.$router.push({ name: 'Dashboard' })
                    })
                    .catch(error => console.log(error))
            }
        },
        computed: {

        },
        mounted() {

        }
    }
</script>