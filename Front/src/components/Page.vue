<template>
  <div class="Page">
    <b-navbar type="is-info" :spaced="true">
      <template #start>
        <b-navbar-item v-on:click="route('table')">
          <b-icon pack="fas" icon="table" size="is-small" />
          <span class="ml-2">Расписание</span>
        </b-navbar-item>
        <b-navbar-item v-on:click="route('record')">
          <b-icon pack="fas" icon="user-edit" size="is-small" />
          <span class="ml-2">Новая запись</span>
        </b-navbar-item>
      </template>
      <template #end>
        <b-navbar-item tag="div">
          <div class="buttons">
            <a class="button is-success" @click="exit">
              <strong>Выйти</strong>
            </a>
          </div>
        </b-navbar-item>
      </template>
    </b-navbar>
    <div class="Page__content">
      <Record v-if="page === 'record'" />
      <Table v-if="page === 'table'" />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Record from "./Record.vue";
import Table from "./Table.vue";

type TPage = "record" | "table";
export default Vue.extend({
  name: "Page",
  components: {
    Record,
    Table,
  },
  props: {
    page: String,
  },
  data() {
    return {};
  },
  created() {},
  watch: {},
  methods: {
    route(page: TPage): void {
      this.$emit("pageChange", page);
    },

    exit() {
      document.cookie = 'key=';
      this.$router.push('/auth');
    }
  },
});
</script>

<style></style>
