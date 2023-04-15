<template>
  <v-card class="hatter">
    <v-card-text>
      <v-form ref="form" v-model="valid" lazy-validation>
        <v-row>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="firstName"
              label="Családnév"
              :rules="nameRules"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="lastName"
              label="Keresztnév"
              :rules="nameRules"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-text-field
          v-model="email"
          label="E-mail"
          :rules="emailRules"
          required
        ></v-text-field>
        <v-text-field
          v-model="age"
          readonly
          :rules="birthdateRules"
          label="Születési dátum"
          @click="bdPicker = true"
        ></v-text-field>
        <v-dialog v-model="bdPicker" width="350">
          <v-card>
            <v-card-text>
              <v-layout align-center justify-center>
                <v-date-picker
                  v-model="age"
                  :rules="birthdateRules"
                  :year-size="2"
                  required
                  min="1905-01-01"
                  max="2022-12-31"
                ></v-date-picker>
              </v-layout>
            </v-card-text>
            <v-card-actions>
              <v-layout align-center justify-center>
                <v-btn color="primary" @click="bdPicker = false">Mentés</v-btn>
              </v-layout>
            </v-card-actions>
          </v-card>
        </v-dialog>
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
        <v-text-field
          v-model="confirmPassword"
          label="Jelszó megerősítése"
          :rules="passwordRules"
          :append-icon="showConfirmPassword ? 'mdi-eye' : 'mdi-eye-off'"
          :type="showConfirmPassword ? 'text' : 'password'"
          @click:append="showConfirmPassword = !showConfirmPassword"
          required
        ></v-text-field>
        <v-checkbox
          v-model="acceptTos"
          label="Elfogadom a felhasználói feltételeket"
          @click.native="redirectToTos"
        ></v-checkbox>
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
        >Regisztráció</v-btn
      >
    </v-card-actions>
    <v-alert
      fixed
      class="alert"
      type="error"
      v-if="errorMsg"
      dismissible
      @input="errorMsg = ''"
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
  </v-card>
</template>

<script>
import axios from "axios";

export default {
  data: () => ({
    dialog: false,
    bdPicker: false,
    valid: true,
    firstName: "",
    lastName: "",
    email: "",
    username: "",
    password: "",
    confirmPassword: "",
    age: "",
    errorMsg: "",
    successMsg: "",
    showPassword: false,
    showConfirmPassword: false,
    acceptTos: false,
    nameRules: [
      (v) => !!v || "Név kötelező",
      (v) =>
        (v && v.length <= 40) ||
        "A név hossza, kevesebb kell legyen mint 40 karakter!",
    ],
    emailRules: [
      (v) => !!v || "E-mail kötelező",
      (v) => /.+@.+/.test(v) || "Érvénytelen formátum",
    ],
    usernameRules: [
      (v) => !!v || "Felhasználónév kötelező",
      (v) =>
        (v && v.length <= 20) ||
        "A felhasználónév hossza, kevesebb kell legyen mint 20 karakter!",
    ],
    passwordRules: [
      (v) => !!v || "Jelszó kötelező",
      (v) =>
        (v && v.length >= 8) ||
        "A jelszó legalább 8 karaktert kell tartalmazzon!",
    ],
    birthdate: null,
    birthdateRules: [
      (v) => {
        if (!v) return "Születési dátum kötelező";
        let ageDifMs = Date.now() - new Date(v).getTime();
        let ageDate = new Date(ageDifMs);
        return (
          Math.abs(ageDate.getUTCFullYear() - 1970) >= 16 ||
          "Legalább 16 évesnek kell lenned !"
        );
      },
    ],
    tosUrl: 'https://www.termsfeed.com/live/d602a85c-9aab-4733-80a8-22afdc9f9f0c'
  }),
  methods: {
    async submit() {
      if (this.$refs.form.validate()) {
        try {
          const data = {
            firstName: this.firstName,
            lastName: this.lastName,
            age: this.calculateAge(this.age),
            username: this.username,
            password: this.password,
            email: this.email,
          };
          // eslint-disable-next-line no-unused-vars
          const response = await axios.post("api/users", data, {
            headers: {
              "Content-Type": "application/json",
            },
          });

          this.successMsg = "Sikeres regisztráció!";
          this.errorMsg = "";
        } catch (error) {
          this.errorMsg = "Ez az email-cim vagy felhasználó már létezik";
          this.successMsg = "";
        }
      }
    },
    calculateAge(date) {
      let aDate = new Date(date);
      let today = new Date();
      let age = today.getFullYear() - aDate.getFullYear();
      let month = today.getMonth() - aDate.getMonth();
      if (month < 0 || (month === 0 && today.getDate() < aDate.getDate())) {
        age--;
      }
      return age;
    },
    redirectToTos() {
      window.open(this.tosUrl, '_blank');
    }
  },
};
</script>

<style scoped>
*{
  font-family: 'Baloo-Regular';
}
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