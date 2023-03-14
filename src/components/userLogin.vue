<template>
  <div>
    <v-card class="hatter">
      <v-card-text>
        <v-form ref="form" v-model="valid" lazy-validation>
          <v-text-field
            v-model="username"
            label="Felhasználónév"
            :rules="usernameRules"
            required
          ></v-text-field>
          <v-text-field
            v-model="password"
            label="Jelszó"
            :rules="passwordRules"
            :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
            :type="showPassword ? 'text' : 'password'"
            @click:append="showPassword = !showPassword"
            required
          ></v-text-field>
        </v-form>
      </v-card-text>
      <v-card-actions class="justify-center">
        <v-btn
          rounded
          color="#3E1E68"
          class="reg"
          text
          @click="submit"
          :disabled="!valid"
          >Bejelentkezés</v-btn
        >
      </v-card-actions>
    </v-card>
    <v-alert
      type="error"
      v-if="errorMsg"
      dismissible
      @input="errorMsg = ''"
      fixed
      class="alert"
      >{{ errorMsg }}</v-alert
    >
    <v-alert
      fixed
      class="alert"
      type="success"
      v-if="successMsg"
      dismissible
      @input="successMsg = ''"
      >{{ successMsg }}</v-alert
    >
  </div>
</template>

<script>
import axios from "axios";

export default {
  data: () => ({
    dialog: false,
    valid: true,
    username: "",
    password: "",
    confirmPassword: "",
    errorMsg: "",
    successMsg: "",
    showPassword: false,
    usernameRules: [(v) => !!v || "Felhaszálónév kötelező"],
    passwordRules: [(v) => !!v || "Jelszó kötelező"],
  }),
  methods: {
    async submit() {
      if (this.$refs.form.validate()) {
        try {
          const data = {
            username: this.username,
            password: this.password,
          };
          // eslint-disable-next-line no-unused-vars
          const response = await axios.post("api/users/login", data, {
            headers: {
              "Content-Type": "application/json",
            },
          });

          this.successMsg = "Sikeres bejelentkezés";
          this.errorMsg = "";
          if (response.data) {
            localStorage.setItem("jwtToken", response.data);
            this.$router.push({ name: "main" });
          }
          //Átirányit a main oldalra
        } catch (error) {
          this.errorMsg = "Helytelen felhasználónév vagy jelszó";
          this.successMsg = "";
        }
      }
    },
  },
};
</script>

<style scoped>
.jobbalso {
  margin-left: 2em;
  size: 1em;
}
.blue--text {
  text-decoration: none;
}
.v-btn {
  background-color: white;
}
.v-btn.reg {
  background-color: yellow;
}
.v-card.hatter {
  background-color: lightgray;
}
.alert {
  position: fixed;
  top: 5%;
  left: 80%;
  transform: translateX(-50%);
  z-index: 9999;
  width: 40%;
}
</style>