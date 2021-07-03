<template>
  <div>
    <SideNav/>
    <div class="pa-5">
      <router-view></router-view>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class MainContainerComponent extends Vue {}
</script>
