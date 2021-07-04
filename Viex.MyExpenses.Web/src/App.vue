<template>
  <v-app>
    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<style>
.app-border-red {
  border: 2px solid red;
}

.app-hover-pointer:hover {
  cursor: pointer;
}

.app-hover-scale-sm {
  transition: all ease 0.2s;
}

.app-hover-scale-sm:hover {
  transform: scale(1.02);
}
</style>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'App',

  data: () => ({
    //
  }),
});
</script>
