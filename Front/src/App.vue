<template>
  <div id="app" class="has-background-info-light">
    <Auth v-if="isAuth" />
    <Page v-else :page="page" v-on:pageChange="pageChanged" />
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Page from "./components/Page.vue";
import Auth from "./components/Auth.vue";
type TPage = "record" | "table" | undefined;
export default Vue.extend({
  name: "App",
  components: {
    Page,
    Auth,
  },
  data() {
    return {
      page: undefined as TPage,
      isAuth: false,
    };
  },
  beforeMount() {
    const page = window.location.pathname.replaceAll("/", "");
    console.log("before", page);
    if (["record", "table"].includes(page)) {
      this.page = page as TPage;
      this.isAuth = false;
    } else if (["auth"].includes(page)) {
      this.isAuth = true;
    }
  },
  watch: {
    $route() {
      const page = window.location.pathname.replaceAll("/", "");
      console.log("watch", page);
      if (["record", "table"].includes(page)) {
        this.page = page as TPage;
        this.isAuth = false;
      } else if (["auth"].includes(page)) {
        this.isAuth = true;
      }
    },
  },
  methods: {
    pageChanged(page: TPage): void {
      this.page = page;
      window.history.pushState(null, "Messenger+", `/${page}`);
    },
  },
});
</script>

<style>
@import url("https://use.fontawesome.com/releases/v5.2.0/css/all.css");
html {
  overflow-y: auto !important;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  max-height: 100vh;
  min-height: 100vh;
  max-width: 100vw;
  overflow: hidden;
}
</style>