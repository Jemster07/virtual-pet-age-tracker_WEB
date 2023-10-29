<template>
  <div id="vpat-app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>
      &nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link v-bind:to="{ name: 'login' }" v-if="$store.state.token == '' && $store.state.displayLogin == true">Login</router-link>
      &nbsp;
      <router-link v-bind:to="{ name: 'user' }" v-if="$store.state.token != ''">{{ $store.state.user.username }}</router-link>
      &nbsp;
      <router-link v-bind:to="{ name: 'support' }">Support</router-link>
      &nbsp;
      <router-link v-bind:to="{ name: 'admin' }" v-if="$store.state.token != '' && $store.state.user.role == 'admin'">Admin Page</router-link>
    </div>
    <router-view />
  </div>
</template>
