<template>
  <v-card class="main">
    <h1>Új esemény létrehozása</h1>
    <v-form ref="form" v-model="valid" lazy-validation>
      <v-text-field
        class="elso"
        v-model="description"
        label="Esemény típusa"
        :rules="descriptionRules"
        required
        style="color: white"
      ></v-text-field>
      <v-text-field
        v-model="spaces"
        label="Szabad helyek száma"
        :rules="spaceRules"
        required
        style="color: white"
      ></v-text-field>
      <v-text-field
        v-model="date"
        readonly
        :rules="dateRules"
        label="Esemény időpontja"
        @click="datePicker = true"
      ></v-text-field>
      <v-dialog v-model="datePicker" width="350">
        <v-card class="timeDialog">
          <v-card-text>
            <v-layout align-center justify-center>
              <v-date-picker
                v-model="date"
                :rules="dateRules"
                :year-size="2"
                required
              ></v-date-picker>
            </v-layout>
          </v-card-text>
          <v-card-actions>
            <v-layout align-center justify-center>
              <v-btn color="primary" @click="datePicker = false">Mentés</v-btn>
            </v-layout>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-text-field
        v-model="time"
        label="Pontos időpont"
        hint="Használd ezt a formátumot HH:mm"
        :rules="timeRules"
        v-mask="'##:##'"
      ></v-text-field>
      <div class="leaflet">
        <l-map
          ref="map"
          :zoom="zoom"
          :center="center"
          @click="onMapClick"
          style="z-index: 0; height: 300px"
        >
          <l-tile-layer :url="url"></l-tile-layer>
          <l-marker :lat-lng="markerLatLng"></l-marker>
        </l-map>
      </div>
      <vue-google-autocomplete
        ref="locationAutocomplete"
        id="location-autocomplete"
        class="form-control"
        placeholder="Start typing address"
        @place_changed="onPlaceChanged"
        :options="{
          types: ['geocode'],
          componentRestrictions: { country: 'ro' },
          key: 'AIzaSyBtFkammvlxbrJunQW5XcXXTrnHfgWj77E',
        }"
      ></vue-google-autocomplete>
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
import { LMap, LTileLayer, LMarker } from "vue2-leaflet";
import "leaflet/dist/leaflet.css";
import { Icon } from "leaflet";
import VueGoogleAutocomplete from "vue-google-autocomplete";
import jwt_decode from "jwt-decode";
import VueTheMask from 'vue-the-mask';

export default {
  name: "CreateEvent",
  directives: {
    mask: VueTheMask.directive,
  },
  components: {
    LMap,
    LTileLayer,
    LMarker,
    VueGoogleAutocomplete,
  },
  data: () => ({
    valid: true,
    description: "",
    spaces: "",
    time: "",
    successMsg: "",
    dialog: false,
    datePicker: false,
    date: "",
    dateRules: [
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
    descriptionRules: [
      (v) => !!v || "Leírás kötelező",
      (v) =>
        (v && v.length >= 4) || "A leírás legalább 4 karakter kell legyen!",
    ],
    spaceRules: [
      (v) => !!v || "Elérhető helyek meghatározása kötelező",
      (v) => (v && v >= 0) || "Negativ mennyiség nem lehetséges",
    ],
    timeRules: [
      (v) => !!v || "Pontos idő megadása kötelező",
      (v) =>
        /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])$/.test(v) || "Helytelen formátum",
    ],
    url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
    zoom: 17,
    center: [46.36, 25.802],
    markerLatLng: [46.3604, 25.802],
    mapVisible: true,
    latitude: "",
    longitude: "",
  }),
  methods: {
    async submit() {
      if (this.$refs.form.validate()) {
        try {
          const userResponse = await axios.get(`api/users/${this.userId}`);
          const user = userResponse.data;
          const data = {
            description: this.description,
            spaces: this.spaces,
            date: this.date,
            time: this.time,
            latitude: this.latitude,
            longitude: this.longitude,
            user: user,
          };
          console.log(data)
          // eslint-disable-next-line no-unused-vars
          const response = await axios.post(`api/events/${this.userId}`, data, {
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
    onMapClick(e) {
      this.markerLatLng = [e.latlng.lat, e.latlng.lng];
      this.latitude = e.latlng.lat;
      this.longitude = e.latlng.lng;
      console.log(this.latitude, this.longitude);
    },
    onPlaceChanged() {
      const place = this.$refs.locationAutocomplete.getPlace();
      if (place.geometry) {
        this.markerLatLng = [
          place.geometry.location.lat(),
          place.geometry.location.lng(),
        ];
      }
    },
  },
  mounted() {
    delete Icon.Default.prototype._getIconUrl;
    Icon.Default.mergeOptions({
      iconRetinaUrl: require("leaflet/dist/images/marker-icon-2x.png"),
      iconUrl: require("leaflet/dist/images/marker-icon.png"),
      shadowUrl: require("leaflet/dist/images/marker-shadow.png"),
    });
    const token = localStorage.getItem("jwtToken");
    if (!token) {
      console.error("No token found");
      return;
    }
    const user = jwt_decode(token);
    this.initials = `${user.firstName.charAt(0)}${user.lastName.charAt(0)}`;
    this.username = user.username;
    this.email = user.email;
    this.userId = user.id;
    console.log(this.userId);
  },
};
</script>

<style scoped>
.main {
  width: 35%;
  margin: 0 auto;
  margin-bottom: 2rem;
  border-radius: 5px;
  background-color: rgba(255, 255, 255, 0.35);
}
.v-text-field {
  margin: 0 auto;
  margin-top: 0.7rem;
  width: 95%;
  padding: 0rem 1.5rem 0rem 1.5rem;
  background-color: rgba(255, 255, 255, 0.15);
  border-radius: 35px;
}
h1 {
  padding: 1.5rem 0 0 1.5rem;
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
.leaflet {
  margin: 1.5rem 1rem;
}
.timeDialog {
  z-index: 200099;
}
</style>
