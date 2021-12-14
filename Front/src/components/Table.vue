<template>
  <div class="is-flex is-justify-content-center is-flex-wrap-wrap">
    <div class="card table mt-3">
      <b-table
        type="is-success"
        :data="records"
        :columns="columns"
        :striped="true"
        :selected.sync="selected"
      ></b-table>
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
          Номер чека:{{ selected && selected.check }}
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
            Тип услуги - {{ selected && selected.service.title }}
          </div>
          <div class="Subtitle is-size-6">
            Мастер - {{ selected && selected.master.surname }}
            {{ selected && selected.master.name }}
            {{ selected && selected.master.patronymic }}
          </div>
          <div class="Subtitle is-size-6">
            Дата - {{ selected && selected.date }}
          </div>
          <div class="Subtitle is-size-6">
            Временной интервал - {{ selected && selected.timeInterval.title }}
          </div>
          <div class="Subtitle is-size-6">
            Стоимость - {{ selected && selected.service.cost}}₽
          </div>
        </div>
      </div>
    </b-modal>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  data() {
    return {
      selected: undefined as any,
      isModalActive: false,
      records: [
        {
          id: 1,
          clientId: 1,
          client: {
            id: 1,
            fio: "",
            surname: "Иванов",
            name: "Иван",
            patronymic: "Иванович",
            gender: null,
            phone: "8(800)555-35-35",
            email: "test1@test.com",
            hasTelegram: true,
            hasWhatsapp: null,
            isVIP: null,
          },
          serviceId: 1,
          service: {
            id: 1,
            title: "Ноготочки",
            description: null,
            cost: 3000,
          },
          timeIntervalId: 1,
          timeInterval: {
            id: 1,
            title: "с 9:00 до 13:00",
          },
          masterId: 1,
          master: {
            id: 1,
            fio: "",
            name: "Катерина",
            surname: "Нестерова",
            patronymic: "Марковна",
            workPhone: "7(998)999-88-99",
          },
          date: "2021-08-03T00:00:00",
          check: null as string | null,
        },
        {
          id: 2,
          clientId: 2,
          client: {
            id: 2,
            surname: "Чеботарева",
            name: "Полина",
            patronymic: "Богдановна",
            gender: null,
            phone: "7(937)999-99-99",
            email: "test2@test.com",
            hasTelegram: null,
            hasWhatsapp: true,
            isVIP: null,
          },
          serviceId: 1,
          service: {
            id: 1,
            title: "Ноготочки",
            description: null,
            cost: 3000,
          },
          timeIntervalId: 2,
          timeInterval: {
            id: 2,
            title: "с 14:00 до 18:00",
          },
          masterId: 1,
          master: {
            id: 1,
            fio: "",
            name: "Катерина",
            surname: "Нестерова",
            patronymic: "Марковна",
            workPhone: "7(998)999-88-99",
          },
          date: "2021-07-15T00:00:00",
          check: null,
        },
        {
          id: 3,
          clientId: 3,
          client: {
            id: 3,
            surname: "Новикова",
            name: "Мария",
            patronymic: "Кирилловна",
            gender: null,
            phone: "7(937)945-99-99",
            email: "test3@test.com",
            hasTelegram: null,
            hasWhatsapp: null,
            isVIP: true,
          },
          serviceId: 2,
          service: {
            id: 2,
            title: "Педикюр",
            description: null,
            cost: 3500,
          },
          timeIntervalId: 1,
          timeInterval: {
            id: 1,
            title: "с 9:00 до 13:00",
          },
          masterId: 2,
          master: {
            id: 2,
            name: "Агата",
            surname: "Смирнова",
            patronymic: "Данииловна",
            workPhone: "7(997)999-77-99",
          },
          date: "2021-06-19T00:00:00",
          check: null,
        },
        {
          id: 4,
          clientId: 4,
          client: {
            id: 4,
            surname: "Егорова",
            name: "Василиса",
            patronymic: "Львовна",
            gender: null,
            phone: "7(937)777-99-91",
            email: "test4@test.com",
            hasTelegram: null,
            hasWhatsapp: true,
            isVIP: null,
          },
          serviceId: 2,
          service: {
            id: 2,
            title: "Педикюр",
            description: null,
            cost: 3500,
          },
          timeIntervalId: 2,
          timeInterval: {
            id: 2,
            title: "с 14:00 до 18:00",
          },
          masterId: 2,
          master: {
            id: 2,
            name: "Агата",
            surname: "Смирнова",
            patronymic: "Данииловна",
            workPhone: "7(997)999-77-99",
          },
          date: "2021-04-21T00:00:00",
          check: null,
        },
      ],
      columns: [
        {
          field: "client.id",
          label: "ID",
          width: "40",
          numeric: true,
        },
        {
          field: "client.fio",
          label: "Клиент",
        },
        {
          field: "service.title",
          label: "Услуга",
          width: "300",
        },
        {
          field: "master.fio",
          label: "Мастер",
        },
        {
          field: `date`,
          label: "Дата",
          width: "150",
        },
        {
          field: "timeInterval.title",
          label: "Временной интервал",
          width: "300",
        },
      ],
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
  created() {
    this.records.forEach((rec) => {
      rec.date = new Date(rec.date).toLocaleDateString();
      rec.client.fio = `${rec.client.surname ?? ""} ${rec.client.name} ${
        rec.client.patronymic ?? ""
      }`.trim();
      const name = rec.master.name ? rec.master.name[0] + "." : "";
      rec.master.fio = `${rec.master.surname ?? ""} ${name}`.trim();
    });
  },
  watch: {},
  methods: {
    showModal() {
      this.isModalActive = true;
    },
    recordComplete() {
      if (this.selected) {
        this.selected.check = Math.floor(Math.random() * 1000000).toString();
      }
    },
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
