<template>
  <div
    class="has-background-info content-auth is-flex is-flex-direction-column is-justify-content-center is-align-items-center"
  >
    <figure class="image is-96x96 mr-3 is-flex is-align-self-center">
      <img src="@/assets/manicure.png" />
    </figure>
    <div class="title is-size-3 has-text-white p-3">Панель администратора</div>
    <div
      class="card form is-flex is-justify-content-center is-flex-wrap-wrap p-4"
    >
      <p class="title is-5">Авторизация</p>
      <b-field label="Логин" class="form__input">
        <b-input v-model="login" required></b-input>
      </b-field>
      <b-field label="Пароль" class="form__input">
        <b-input
          v-model="password"
          type="password"
          required
          password-reveal
          icon-pack="fas"
        >
        </b-input>
      </b-field>
      <div class="form__buttons is-flex is-justify-content-start">
        <b-button type="is-success" @click="auth">Войти</b-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { ToastProgrammatic as Toast } from "buefy";

export default Vue.extend({
  data() {
    return {
      login: "",
      password: "",
    };
  },
  created() {},
  methods: {
    auth() {
      // не взламывайте пожалуйста
      if (this.login === "admin" && this.password === "admin") {
        const date = new Date();
        document.cookie = `key=${
          date.getFullYear() + date.getMonth() + date.getDate()
        }`;
        this.$router.push("/table");
      } else {
        Toast.open({
          message: "Неверные данные для входа",
          type: "is-danger",
        });
      }
    },
  },
});
</script>

<style>
.form {
  width: 30%;
}
.form__input {
  width: 100%;
}
.form__buttons {
  width: 100%;
  gap: 10px;
}
.content-auth {
  height: 100vh;
}
</style>
