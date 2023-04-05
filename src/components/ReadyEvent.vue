<template>
  <v-card class="main" v-if="proper = true">
    <div class="header">
      <div class="box">
        <v-avatar color="yellow" size="58"
          ><span class="text-h5">{{
            user.firstName.charAt(0) + user.lastName.charAt(0)
          }}</span></v-avatar
        >
      </div>
      <div class="box">
        <p class="mid">{{ user.username }}</p>
        <h2 class="mid">{{ event.description }}</h2>
      </div>
      <div class="box">
        <p class="time">{{ formatDate() }}</p>
        <h2 class="time">{{ event.time }}</h2>
      </div>
    </div>
    <div class="content">
      <div class="box2">
        <h4>Ennyi km-re tőled: {{ calcDistance() }}</h4>
        <h4>Elérhető helyek száma : {{ event.spaces }}</h4>
        <v-btn rounded color="#3e1e68" class="join">Csatlakozás</v-btn>
      </div>
      <l-map
        ref="map"
        :zoom="zoom"
        :center="center"
        style="z-index: 0; height: 300px; margin: 1rem"
      >
        <l-tile-layer :url="url"></l-tile-layer>
        <l-marker :lat-lng="markerLatLng"></l-marker>
      </l-map>
    </div>
  </v-card>
</template>

<script>
import { LMap, LTileLayer, LMarker } from "vue2-leaflet";
import "leaflet/dist/leaflet.css";
import { Icon } from "leaflet";
import axios from "axios";
import jwt_decode from "jwt-decode";

export default {
  name: "CreateEvent",
  components: {
    LMap,
    LTileLayer,
    LMarker,
  },
  props: {
    event: {
      type: Object,
      required: true,
    },
  },
  data: () => ({
    url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
    zoom: 14,
    center: [46.36, 25.802],
    markerLatLng: [46.3604, 25.802],
    mapVisible: true,
    description: "",
    spaces: "",
    date: "",
    time: "",
    userId: "",
    user: "",
    latitude: "",
    longitude: "",
    userdistance: "",
    minUserAge: "",
    maxUserAge: "",
    proper: ''
  }),
  methods: {
    deg2rad(deg) {
      return deg * (Math.PI / 180);
    },
    calcDistance() {
      const R = 6371;
      const dLat = this.deg2rad(this.event.latitude - this.latitude);
      const dLon = this.deg2rad(this.event.longitude - this.longitude);
      const a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(this.deg2rad(this.latitude)) *
          Math.cos(this.deg2rad(this.event.latitude)) *
          Math.sin(dLon / 2) *
          Math.sin(dLon / 2);
      const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
      const d = R * c;
      return d.toFixed(2);
    },
    formatDate() {
      return this.event.date.substring(0, 10);
    },
  },
  async mounted() {
    // eslint-disable-next-line no-unused-vars
    const response = await axios.get(`api/users/${this.event.userId}`);
    this.user = response.data;
    this.markerLatLng[0] = this.event.latitude;
    this.markerLatLng[1] = this.event.longitude;
    this.center[0] = this.event.latitude;
    this.center[1] = this.event.longitude;
    const response2 = await axios.get(`api/users/userprofile/${this.event.userId}`);
    this.userdistance = response2.value;
    this.minUserAge = response2.rangeStart;
    this.maxUserAge = response2.rangeEnd;
    const token = localStorage.getItem("jwtToken");
    if (!token) {
      console.error("No token found");
      return;
    }
    // eslint-disable-next-line no-unused-vars
    const loggedUser = jwt_decode(token);
    if(this.userdistance > this.calcDistance() || this.minUserAge > this.loggedUser.age || this.maxUserAge < this.loggedUser.age)
    {
      this.proper = false;
    }
  },
  created() {
    delete Icon.Default.prototype._getIconUrl;
    Icon.Default.mergeOptions({
      iconRetinaUrl: require("leaflet/dist/images/marker-icon-2x.png"),
      iconUrl: require("leaflet/dist/images/marker-icon.png"),
      shadowUrl: require("leaflet/dist/images/marker-shadow.png"),
    });
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          this.latitude = position.coords.latitude;
          this.longitude = position.coords.longitude;
          // Do something with latitude and longitude
        },
        (error) => {
          console.error(error);
        }
      );
    } else {
      console.error("Geolocation is not supported by this browser.");
    }
  },
};
</script>

<style scoped>
.main {
  width: 85%;
  margin: 0 auto;
  margin-bottom: 2rem;
  border-radius: 5px;
  background-color: rgba(255, 255, 255, 0.35);
}
.header {
  display: flex;
  justify-content: space-between;
  border-bottom: 1px solid rgba(255, 255, 255, 0.6);
}
.v-avatar {
  margin: 0.7rem 0rem 0.7rem 0.7rem;
}
p {
  margin-top: 0.7rem;
}
.v-application p {
  margin-bottom: 0px;
}
.time {
  text-align: right;
  margin-right: 0.7rem;
}

.content {
  display: flex;
  justify-content: space-between;
}
.box2 {
  margin-top: 1rem;
}
h4 {
  padding: 1rem 2rem;
}
.v-btn {
  margin: 2rem;
}
.box:first-child {
  margin-right: 0.5rem;
}
.box:last-child {
  flex-grow: 1;
}
.join{
  color: white;
}
</style>
