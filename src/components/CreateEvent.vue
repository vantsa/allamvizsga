<template>
  <v-card class="main">
    <h1>Új esemény létrehozása</h1>
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-text-field
        class="elso"
        v-model="topic"
        label="Esemény típusa"
        :rules="topicRules"
        required
      ></v-text-field>
      <v-text-field
        v-model="time"
        readonly
        :rules="timeRules"
        label="Esemény időpontja"
        @click="timePicker = true"
      ></v-text-field>
      <v-dialog v-model="timePicker" width="350">
        <v-card>
          <v-card-text>
            <v-layout align-center justify-center>
              <v-date-picker
                v-model="time"
                :rules="timeRules"
                :year-size="2"
                required
              ></v-date-picker>
            </v-layout>
          </v-card-text>
          <v-card-actions>
            <v-layout align-center justify-center>
              <v-btn color="primary" @click="timePicker = false">Mentés</v-btn>
            </v-layout>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-text-field
        v-model="ptime"
        label="Pontos időpont"
        hint="Használd ezt a formátumot HH:mm"
        :rules="ptimeRules"
        v-mask="'##:##'"
      ></v-text-field>
      <v-text-field
        v-model="location"
        label="Helyszín"
        hint="írd be a címet, vagy a térképen jelöld be"
        :rules="locationRules"
        required
      ></v-text-field>
      <v-card-actions class="justify-end">
        <v-btn
          rounded
          class="create-btn"
          text
          @click="submit"
          :disabled="!valid"
          >Létrehozás
        </v-btn>
      </v-card-actions>
    </v-form>
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
  name: "CreateEvent",
  data: () => ({
    valid: true,
    topic: "",
    location: "",
    ptime: "",
    successMsg: "",
    dialog: false,
    timePicker: false,
    time: "",
    timeRules: [
      (v) => !!v || "Dátum kötelező",
      (v) => {
        const selectedDate = new Date(v);
        const currentDate = new Date();
        if (selectedDate < currentDate) {
          return "A kiválasztott dátum nem lehet a múltban.";
        }
        return true;
      },
    ],
    topicRules: [
      (v) => !!v || "Leírás kötelező",
      (v) =>
        (v && v.length >= 4) || "A leírás legalább 4 karakter kell legyen!",
    ],
    locationRules: [(v) => !!v || "Helyszín megadása kötelező"],
    ptimeRules: [
      (v) => !!v || "Time is required",
      (v) =>
        /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])$/.test(v) || "Helytelen formátum",
    ],
  }),
  methods: {
    async submit() {
      if (this.$refs.form.validate()) {
        try {
          const data = {
            topic: this.topic,
            location: this.location,
          };
          // eslint-disable-next-line no-unused-vars
          const response = await axios.post("api/users", data, {
            headers: {
              "Content-Type": "application/json",
            },
          });
          this.successMsg = "Esemény létrehozva!";
        } catch (error) {
          this.successMsg = "";
        }
      }
    },
  },
};
</script>

<style scoped>
.main {
  width: 35%;
  margin: 0 auto;
  margin-bottom: 2rem;
  border-radius: 5px;
}
.v-text-field {
  width: 70%;
  padding-left: 1.5rem;
}
h1{
    padding: 1.5rem 0 0 1.5rem;
}
.elso {
  padding-top: 1.5rem;
}
.alert {
  position: fixed;
  top: 5%;
  left: 80%;
  transform: translateX(-50%);
  z-index: 9999;
  width: 40%;
}
.create-btn {
  background-color: #3e1e68;
  color: white;
  margin: 0 1.5rem 1.5rem 0;
}
</style>
