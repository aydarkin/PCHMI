<template>
  <div class="is-flex is-justify-content-center is-flex-wrap-wrap">
    <div class="card record mt-3">
      <b-tabs v-model="nameTab" type="is-boxed" :animated="false">
        <b-tab-item label="Ввести данные">
          <div
            class="record-enter is-flex is-flex-wrap-nowrap is-justify-content-space-around"
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
                <b-radio v-model="newClient.gender" name="name" native-value="1">
                  Мужской
                </b-radio>
                <b-radio v-model="newClient.gender" name="name" native-value="2">
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
      class="card record mt-2 is-flex is-justify-content-center is-flex-wrap-wrap p-3"
    >
      <div class="record-master">
        <b-field label="Мастер">
          <b-select
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
              :show-week-number="showWeekNumber"
              :locale="locale"
              placeholder="Введите дату"
              icon="calendar"
              icon-pack="fas"
              :icon-right="selectedDate ? 'times-circle' : ''"
              icon-right-clickable
              @icon-right-click="clearDate"
              trap-focus
            >
            </b-datepicker>
          </b-field>
          <b-field label="Временной интервал" class="data-info__timeIntervals">
            <b-select
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
        <b-button type="is-success" @click="save">Сохранить</b-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

interface IState {
  selectedDate?: Date | null;
  [x: string]: any;
}
export default Vue.extend({
  data() {
    return {
      nameTab: 0,
      selectedDate: null,
      selectedService: null,
      selectedClient: null,
      selectedMaster: null,
      selectedTimeInterval: null,

      newClient: {
        surname: "",
        name: "",
        patronymic: "",
        gender: null,
        phone: "",
        email: "",
      },

      masters: [
        {
          id: 1,
          name: "Варвара",
          surname: "Новикова",
          patronymic: "Викторовна",
          workPhone: "7(998)999-88-99",
        },
        {
          id: 2,
          name: "Агата",
          surname: "Смирнова",
          patronymic: "Данииловна",
          workPhone: "7(997)999-77-99",
        },
        {
          id: 3,
          fio: "",
          name: "Катерина",
          surname: "Нестерова",
          patronymic: "Марковна",
          workPhone: "7(998)999-88-99",
        },
      ],
      clients: [
        {
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
        {
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
        {
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
        {
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
      ],
      timeIntervals: [
        {
          id: 1,
          title: "с 9:00 до 13:00",
        },
        {
          id: 2,
          title: "с 14:00 до 18:00",
        },
      ],
      services: [
        {
          id: 1,
          title: "Маникюр",
          description: null,
          cost: 3000,
        },
        {
          id: 2,
          title: "Педикюр",
          description: null,
          cost: 3000,
        },
      ],
    } as IState;
  },
  created() {},
  watch: {},
  methods: {
    clearDate(): void {
      this.selectedDate = null;
    },
    save() {
      const record = {
        serviceId: this.selectedService,
        timeIntervalId: this.selectedTimeInterval,
        masterId: this.selectedMaster,
        date: this.selectedDate?.toISOString(),
        check: null,
        clientId: null,
        client: undefined,
      };

      if (this.nameTab == 1) {
        record.clientId = this.selectedClient;
      } else {
        record.client = this.newClient;
      }

      console.log(record)
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
