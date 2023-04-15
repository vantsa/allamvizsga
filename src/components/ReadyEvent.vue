<template>
  <v-card class="main" v-if="proper">
    <div class="header">
      <div class="box">
        <v-avatar :color="getRandomColor()" size="58"
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
        <h4>
          <v-icon color="black" class="mb-1">mdi-map-marker</v-icon>
          {{ calcDistance() }} kilóméterre
        </h4>
        <h4>Elérhető helyek száma : {{ event.spaces }}</h4>
        <v-btn
          v-if="alreadyJoined"
          rounded
          color="#A20805"
          class="join"
          @click="userDelete"
          >Mégse<v-icon class="ic">mdi-alpha-x</v-icon></v-btn
        >
        <v-btn
          v-if="!isTheCreator"
          rounded
          color="#0D8D13"
          class="join"
          @click="joinEvent"
          >Csatlakozás</v-btn
        >
        <div v-else class="buttons">
          <v-btn rounded color="#A20805" class="join" @click="onDelete"
            >Törlés<v-icon class="ic">mdi-delete</v-icon></v-btn
          >
          <v-btn rounded color="#484948" class="join" @click="showJoined"
            >Résztvevők listája<v-icon class="ic">mdi-view-list</v-icon></v-btn
          >
        </div>
      </div>
      <l-map
        ref="map"
        :zoom="zoom"
        :center="center"
        style="z-index: 0; height: 350px; margin: 1.5rem"
      >
        <l-tile-layer :url="url"></l-tile-layer>
        <l-marker :lat-lng="markerLatLng"></l-marker>
      </l-map>
    </div>
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
    zoom: 15,
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
    proper: true,
    avatarColors: [
      "#F44336",
      "#E91E63",
      "#9C27B0",
      "#673AB7",
      "#3F51B5",
      "#2196F3",
      "#03A9F4",
      "#00BCD4",
      "#009688",
      "#4CAF50",
      "#8BC34A",
      "#FF9800",
      "#FF5722",
    ],
    isTheCreator: false,
    loggedUser: "",
    usersetts: "",
    successMsg: "",
    alreadyJoined: false,
    response3Data: "",
  }),
  methods: {
    getRandomColor() {
      return this.avatarColors[
        Math.floor(Math.random() * this.avatarColors.length)
      ];
    },
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
    async onDelete() {
      try {
        // eslint-disable-next-line no-unused-vars
        const response = await axios.delete(
          `api/events/remove/${this.event.id}`,
          {
            headers: {
              "Content-Type": "application/json",
            },
          }
        );
        window.location.reload();
        this.successMsg = "Sikeres törlés!";
      } catch (error) {
        this.successMsg = "";
      }
    },
    async joinEvent() {
      try {
        const data = {
          UserId: this.loggedUser.id,
          EventId: this.event.id,
        };
        // eslint-disable-next-line no-unused-vars
        const response = await axios.post(`api/events/adduser`, data, {
          headers: {
            "Content-Type": "application/json",
          },
        });
        this.succesMsg = "Sikeresen csatlakoztál";
      } catch (error) {
        this.successMsg = "";
      }
    },
    async showJoined() {
      try {
        // eslint-disable-next-line no-unused-vars
        const response = await axios.get(`api/events/showlist`);
      } catch (error) {
        console.log(error);
      }
    },
    async userDelete() {

    }
  },
  async mounted() {
    // eslint-disable-next-line no-unused-vars
    const response = await axios.get(`api/users/${this.event.userId}`);
    this.user = response.data;
    this.markerLatLng[0] = this.event.latitude;
    this.markerLatLng[1] = this.event.longitude;
    this.center[0] = this.event.latitude;
    this.center[1] = this.event.longitude;
    const token = localStorage.getItem("jwtToken");
    if (!token) {
      console.error("No token found");
      return;
    }
    this.loggedUser = jwt_decode(token);
    const response2 = await axios.get(
      `api/users/userprofile/${this.loggedUser.id}`
    );
    this.usersetts = response2.data;
    this.userdistance = this.usersetts.value;
    this.minUserAge = this.usersetts.rangeStart;
    this.maxUserAge = this.usersetts.rangeEnd;
    if (
      (this.userdistance < this.calcDistance() &&
        this.loggedUser.id != this.event.userId) ||
      this.minUserAge > this.loggedUser.age ||
      this.maxUserAge < this.loggedUser.age
    ) {
      this.proper = false;
    }
    if (
      this.loggedUser.id == this.event.userId ||
      this.loggedUser.username == "admin"
    ) {
      this.isTheCreator = true;
    } else this.isTheCreator = false;
    if (this.event.spaces == 0) {
      this.proper = false;
      if(this.event.userId == this.loggedUser.id)
      {
        this.proper = true;
      }
    }
    const response3 = await axios.get(
      `api/events/isjoined/${this.loggedUser.id}`
    );
    this.response3Data = response3.data;
    console.log(this.response3Data.UserId)
    if(this.response3Data.UserId == this.loggedUser.id && this.response3Data.EventId == this.event.id){
      this.alreadyJoined = true;
    } else this.alreadyJoined = false;

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
* {
  font-family: "Baloo-Regular";
}
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
.mid {
  margin-left: 1.5rem;
}
.mid:nth-child(1) {
  margin-left: 2rem;
}
.time {
  text-align: right;
  font-weight: bold;
  margin-right: 0.7rem;
}
.time:nth-child(2) {
  margin-right: 1.5rem;
}
.content {
  display: flex;
  justify-content: space-between;
  width: 95%;
  margin: 0 auto;
}
.box2 {
  margin-top: 3rem;
  width: 52%;
}
h4 {
  padding: 1rem 2rem;
  font-size: 1.5rem;
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
.join {
  color: white;
  font-size: 1.5rem;
  margin: 0 auto;
  width: 100%;
  margin-bottom: 5%;
}
.join:nth-child(1) {
  margin-top: 10%;
}
.ic {
  margin-left: 5%;
}
.main .is-full {
  background-color: rgba(0, 0, 0, 0.5);
}
.overlay {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 10;
  color: white;
  text-align: center;
}
.alert {
  position: fixed;
  top: 5%;
  left: 80%;
  transform: translateX(-50%);
  z-index: 9999;
  width: 40%;
}
@media only screen and (max-width: 1175px) {
  .join {
    margin-bottom: 5%;
  }
}
</style>
