<template>
  <v-navigation-drawer app permanent class="elevation-2">
    <template v-slot:prepend>
      <v-list-item two-line>

        <v-menu bottom left>
          <template v-slot:activator="{ on, attrs }">
            <v-list-item-avatar v-bind="attrs" v-on="on">
              <img src="https://randomuser.me/api/portraits/women/81.jpg" />
            </v-list-item-avatar>
          </template>

          <v-list>
            <v-list-item @click="onSignOutClicked">
              <v-list-item-title>Sign Out</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>

        <v-list-item-content>
          <v-list-item-title>Alejandro CR</v-list-item-title>
          <v-list-item-subtitle>Total Balance: $1,000</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </template>

    <v-divider></v-divider>

    <v-list>
      <v-list-item v-for="(item, index) in items" :key="index" :to="item.route">
        <v-list-item-icon class="mx-0 ml-2 mr-5">
          <v-icon>{{item.icon}}</v-icon>
        </v-list-item-icon>
        <v-list-item-content>
          <v-list-item-title>{{item.title}}</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>

  </v-navigation-drawer>
</template>

<script lang="ts">
import api from "@/api";
import timers from "@/utils/timers";
import { Component, Vue } from "vue-property-decorator";
import { DefaultSideNavItems } from './SideNavItem'

@Component
export default class SideNavComponent extends Vue {
  
    items = DefaultSideNavItems

    async onSignOutClicked() {
      await api.auth.signOut()
      this.$router.push('/')
      await timers.wait(500)
      window.location.reload()
    }
}
</script>
