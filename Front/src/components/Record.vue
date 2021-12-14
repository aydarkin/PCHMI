<template>
  <div class="is-flex is-justify-content-center is-flex-wrap-wrap">
    <div class="card record mt-3">
      <b-tabs v-model="nameTab" type="is-boxed" :animated="false">
        <b-tab-item label="Ввести данные">
          <div
            class="
              record-enter
              is-flex is-flex-wrap-nowrap is-justify-content-space-around
            "
          >
            <div class="record-enter__left">
              <b-field label="Фамилия">
                <b-input
                  v-model="newClient.surname"
                  placeholder="Введите фамилию клиента"
                ></b-input>
              </b-field>
              <b-field label="Имя">
                <b-input
                  required
                  v-model="newClient.name"
                  placeholder="Введите имя клиента"
                ></b-input>
              </b-field>
              <b-field label="Отчество">
                <b-input
                  v-model="newClient.patronymic"
                  placeholder="Введите отчество клиента"
                ></b-input>
              </b-field>
            </div>
            <div class="record-enter__right">
              <b-field label="Телефон">
                <b-input
                  required
                  v-model="newClient.phone"
                  placeholder="Введите номер телефона клиента"
                ></b-input>
              </b-field>
              <b-field label="Почта">
                <b-input
                  v-model="newClient.email"
                  placeholder="Введите адрес электронной почты клиента"
                ></b-input>
              </b-field>
              <b-field label="Пол">
                <b-radio v-model="newClient.gender" native-value="M">
                  Мужской
                </b-radio>
                <b-radio v-model="newClient.gender" native-value="F">
                  Женский
                </b-radio>
              </b-field>
            </div>
          </div>
        </b-tab-item>
        <b-tab-item label="Выбрать данные">
          <div class="is-flex is-justify-content-center">
            <b-field label="Клиенты" class="record-select">
              <b-select
                expanded
                required
                v-model="selectedClient"
                placeholder="Выберите клиента"
              >
                <option
                  v-for="client in clients"
                  v-bind:key="client.id"
                  :value="client.id"
                >
                  {{ client.surname }} {{ client.name }}
                  {{ client.patronymic }}
                </option>
              </b-select>
            </b-field>
          </div>
        </b-tab-item>
      </b-tabs>
    </div>
    <div
      class="
        card
        record
        mt-2
        is-flex is-justify-content-center is-flex-wrap-wrap
        p-3
      "
    >
      <div class="record-master">
        <b-field label="Мастер">
          <b-select
            required
            expanded
            v-model="selectedMaster"
            placeholder="Выберите мастера"
          >
            <option
              v-for="master in masters"
              v-bind:key="master.id"
              :value="master.id"
            >
              {{ master.surname }} {{ master.name }}
              {{ master.patronymic }}
            </option>
          </b-select>
        </b-field>
        <b-field label="Услуги">
          <b-select
            required
            expanded
            v-model="selectedService"
            placeholder="Выберите тип услуги"
          >
            <option
              v-for="service in services"
              v-bind:key="service.id"
              :value="service.id"
            >
              {{ service.title }}
            </option>
          </b-select>
        </b-field>
        <div class="is-flex is-justify-content-space-between data-info">
          <b-field label="Дата" class="data-info__datepicker">
            <b-datepicker
              position="is-top-left"
              v-model="selectedDate"
              placeholder="Введите дату"
              icon="calendar"
              icon-pack="fas"
              required
              :icon-right="selectedDate ? 'times-circle' : ''"
              icon-right-clickable
              @icon-right-click="clearDate"
            >
            </b-datepicker>
          </b-field>
          <b-field label="Временной интервал" class="data-info__timeIntervals">
            <b-select
              required
              expanded
              v-model="selectedTimeInterval"
              placeholder="Выберите временной интервал"
            >
              <option
                v-for="timeInterval in timeIntervals"
                v-bind:key="timeInterval.id"
                :value="timeInterval.id"
              >
                {{ timeInterval.title }}
              </option>
            </b-select>
          </b-field>
        </div>
      </div>
      <div class="record-master is-flex is-justify-content-end mt-2">
        <b-button :disabled="!validated" type="is-success" @click="save">Сохранить</b-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Data from "../utils/Data";
import { ToastProgrammatic as Toast } from "buefy";

export default Vue.extend({
  data() {
    return {
      nameTab: 0,
      selectedDate: undefined as Date | undefined,
      selectedService: undefined as any,
      selectedClient: undefined as any,
      selectedMaster: undefined as any,
      selectedTimeInterval: undefined as any,

      newClient: {
        surname: "",
        name: "",
        patronymic: "",
        gender: null,
        phone: "",
        email: "",
      },

      masters: [],
      clients: [],
      timeIntervals: [],
      services: [],
    };
  },
  computed: {
    validated(): boolean {
      return !!(this.selectedDate
        && this.selectedService
        && (this.nameTab == 0 ? this.newClient.name : this.selectedClient)
        && this.selectedMaster
        && this.selectedTimeInterval);
    }
  },
  async created() {
    const promises = [];
    promises.push(Data.getQuery("master"));
    promises.push(Data.getQuery("service"));
    promises.push(Data.getQuery("timeinterval"));
    promises.push(Data.getQuery("client"));

    const [masters, services, timeIntervals, clients] = await Promise.all(
      promises
    );

    this.masters = masters;
    this.services = services;
    this.timeIntervals = timeIntervals;
    this.clients = clients;
  },
  watch: {},
  methods: {
    clearDate(): void {
      this.selectedDate = undefined;
    },
    save() {
      const date = this.selectedDate;
      const dateMonthNum = date ? (date.getMonth() + 1).toString() : '';
      const dateDayNum = date ? (date.getDate() + 1).toString() : '';

      const record = {
        serviceId: this.selectedService,
        timeIntervalId: this.selectedTimeInterval,
        masterId: this.selectedMaster,
        date: date
          ? `${date.getFullYear()}-${
            dateMonthNum.length === 1 ? '0' + dateMonthNum : dateMonthNum
            }-${dateDayNum.length === 1 ? '0' + dateDayNum : dateDayNum}`
          : undefined,
        check: null,
        clientId: null,
        client: undefined as any,
      };

      if (this.nameTab == 1) {
        record.clientId = this.selectedClient;
      } else {
        record.client = this.newClient;
      }

      Data.jsonQuery("record", record)
        .then(() => {
          Toast.open({
            message: "Запись успешно создана",
            type: "is-success",
          });
          this.$router.push("/table");
        })
        .catch(() => {
          Toast.open({
            message: "Ошибка создания записи",
            type: "is-danger",
          });
        });
    },
  },
});
</script>

<style>
.record {
  width: 70%;
}

.record-enter__left,
.record-enter__right {
  width: 46%;
}

.record-select {
  width: 96%;
}

.record-master {
  width: 94%;
}

.data-info {
  gap: 40px;
}

.data-info__datepicker,
.data-info__timeIntervals {
  width: 50%;
}
</style>
