<template>
  <div class="is-flex is-justify-content-center is-flex-wrap-wrap">
    <div class="card table mt-3">
      <b-table
        type="is-success"
        :data="records"
        :columns="columns"
        :striped="true"
        :selected.sync="selected"
      >
        <b-table-column
          field="client.id"
          label="ID"
          width="40"
          numeric
          v-slot="props"
        >
          {{ props.row.id }}
        </b-table-column>
        <b-table-column label="Клиент" v-slot="props">
          {{ getFio(props.row.client) }}
        </b-table-column>
        <b-table-column
          field="service.title"
          label="Услуга"
          width="300"
          v-slot="props"
        >
          {{ props.row.service.title }}
        </b-table-column>
        <b-table-column label="Мастер" v-slot="props">
          {{ getFioShort(props.row.master) }}
        </b-table-column>
        <b-table-column label="Дата" width="150" v-slot="props">
          {{ getDateText(props.row.date) }}
        </b-table-column>
        <b-table-column
          field="timeInterval.title"
          label="Время"
          width="240"
          v-slot="props"
        >
          {{ props.row.timeInterval.title }}
        </b-table-column>
      </b-table>
    </div>
    <div
      class="table__buttons is-flex is-flex-wrap-nowrap is-justify-content-space-between mt-1"
    >
      <b-button type="is-success" :disabled="!selected" @click="selected = null"
        >Снять выделение</b-button
      >
      <div
        class="record__complete is-flex is-flex-wrap-nowrap is-justify-content-end"
      >
        <b-button type="is-danger" :disabled="!selected" @click="recordDelete()"
          >Удалить запись</b-button
        >
        <b-button
          v-if="!(selected && selected.check)"
          type="is-primary"
          :disabled="!selected"
          @click="recordComplete()"
          >Завершить запись</b-button
        >
        <b-button
          type="is-info"
          :disabled="!(selected && selected.check)"
          @click="showModal()"
          >Показать чек</b-button
        >
      </div>
    </div>
    <b-modal v-model="isModalActive" :width="640">
      <div
        class="notification is-info is-light is-flex iis-flex is-flex-direction-column is-align-items-center p-3"
      >
        <div class="title is-size-7">
          Номер чека: {{ selected && selected.check }}
        </div>
        <div class="title is-size-5">
          {{ selected && selected.client.surname }}
          {{ selected && selected.client.name }}
          {{ selected && selected.client.patronymic }}
        </div>
        <div
          class="modal-content is-flex is-flex-direction-column is-flex-wrap-nowrap p-5"
        >
          <div class="Subtitle is-size-6">
            <span class="has-text-weight-bold">Услуга:</span>
            {{ selected && selected.service.title }}
          </div>
          <div class="Subtitle is-size-6">
            <span class="has-text-weight-bold">Мастер:</span>
            {{ selected && getFio(selected.master) }}
          </div>
          <div class="Subtitle is-size-6">
            <span class="has-text-weight-bold">Дата:</span>
            {{ selected && getDateText(selected.date) }}
          </div>
          <div class="Subtitle is-size-6">
            <span class="has-text-weight-bold">Время:</span>
            {{ selected && selected.timeInterval.title }}
          </div>
          <div class="Subtitle is-size-6">
            <span class="has-text-weight-bold">Стоимость:</span>
            {{ selected && selected.service.cost }}₽
          </div>
        </div>
      </div>
    </b-modal>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Data from "../utils/Data";
import { ToastProgrammatic as Toast } from "buefy";

export default Vue.extend({
  data() {
    return {
      selected: undefined as any,
      isModalActive: false,
      records: [],
      columns: [],
      sortPriority: [
        {
          field: "date",
          order: "desc",
        },
        {
          field: "timeInterval.title",
          order: "desc",
        },
      ],
    };
  },
  computed: {},
  async created() {
    this.records = await Data.getQuery("record");
  },
  watch: {},
  methods: {
    getFio(client: any) {
      return client
        ? `${client.surname ?? ""} ${client.name} ${
            client.patronymic ?? ""
          }`.trim()
        : "";
    },
    getFioShort(master: any) {
      return master
        ? `${master.surname ?? ""} ${
            master.name ? master.name[0] + "." : ""
          }`.trim()
        : "";
    },
    getDateText(date: string) {
      return new Date(date).toLocaleDateString();
    },
    showModal() {
      this.isModalActive = true;
    },
    recordComplete() {
      if (this.selected) {
        Data.putQuery(
          "record/" + this.selected.id,
          {
            // костыль
            check: "create",
          },
          undefined,
          true
        )
          .then((rec) => {
            this.selected.check = rec.check;
            Toast.open({
              message: "Запись успешно завершена",
              type: "is-success",
            });
          })
          .catch(() => {
            Toast.open({
              message: "Не удалось завершить запись",
              type: "is-danger",
            });
          });
      }
    },
    recordDelete(){
      if (this.selected) {
        Data.deleteQuery("record/" + this.selected.id)
          .then(() => {
            const selectedIndex = this.records.findIndex((rec: any) => rec.id === this.selected.id);
            this.records.splice(selectedIndex, 1);
            Toast.open({
              message: "Запись успешно завершена",
              type: "is-success",
            });
          })
          .catch(() => {
            Toast.open({
              message: "Не удалось завершить запись",
              type: "is-danger",
            });
          });
      }
    }
  },
});
</script>

<style>
.table,
.table__buttons {
  width: 90%;
}

.record__complete {
  width: 50%;
  gap: 50px;
}
</style>
