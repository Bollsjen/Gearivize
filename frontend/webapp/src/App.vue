<template>
  <div id="app">
    <nav>
      <b-navbar toggleable="lg" type="dark" variant="secondary">
        <b-container fluid class="w-75">
          <b-navbar-brand href="#">
            <img src="/img/logos/Bolls.png" class="bolls-logo">
          </b-navbar-brand>

          <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

          <b-collapse id="nav-collapse" is-nav>
            <b-navbar-nav>
              <b-nav-item><router-link to="/">Dashboard</router-link></b-nav-item>
              <b-nav-item><router-link to="/instruments">Instruments</router-link></b-nav-item>
              <b-nav-item><router-link to="/templates">Templates</router-link></b-nav-item>
            </b-navbar-nav>

            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
              <b-nav-item class="notification-box">
                <router-link to="/notifications">
                  <i class="fa-solid fa-bell notification-bell"></i>
                  <span class="badge-buble" v-if="false">9</span>
                </router-link>
              </b-nav-item>

              <b-nav-item-dropdown right v-if="true">
                <!-- Using 'button-content' slot -->
                <template #button-content>
                  <em>User</em>
                </template>
                <b-dropdown-item href="/settings">Settings</b-dropdown-item>
                <b-dropdown-item href="#">Sign Out</b-dropdown-item>
              </b-nav-item-dropdown>
              <b-nav-item href="/signin" v-else>Signin</b-nav-item>
            </b-navbar-nav>
          </b-collapse>
        </b-container>
      </b-navbar>
    </nav>
    <router-view/>
  </div>
</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {

  a, .nav-link {
    font-weight: 600;
    color: white;
    text-decoration: none;
    font-size: 18px;

    &.router-link-exact-active {
      color: #68DBFF;
    }
  }

  a:hover, .nav-link:hover {
    color: #BFC1C1;
    text-decoration: none;
  }

  .bolls-logo {
    height: 50px;
    width: 50px;
  }

  .notification-box {
    position: relative;
    padding: 0px 10px;
  }

  .notification-bell {
    font-size: 22px;
    color: white;
  }

  .badge-buble {
    position: absolute;
    top: -6px;
    right: 0px;
    padding: 1px 8px;
    border-radius: 50%;
    background-color: red;
    color: white;
    font-size: 14px;
  }
}
</style>
