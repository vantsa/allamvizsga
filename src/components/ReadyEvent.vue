<template>
  <v-card class="main">
    <div class="header">
      <div class="box">
        <v-avatar color="yellow" size="58"></v-avatar>
      </div>
      <div class="box">
        <p class="mid">username</p>
        <h2 class="mid">Ide jön az esemény neve</h2>
      </div>
      <div class="box">
        <p class="time">2023.04.12</p>
        <h2 class="time">17:30</h2>
      </div>
    </div>
    <div class="content">
      <div class="box2">
        <h4>Ennyi km-re tőled: 1.5km</h4>
        <h4>Elérhető helyek száma: 3</h4>
        <v-btn rounded>Csatlakozás</v-btn>
      </div>
      <l-map
          ref="map"
          :zoom="zoom"
          :center="center"
          style="z-index: 0; height: 300px; margin: 1rem;"
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

export default {
  name: "CreateEvent",
  components: {
    LMap,
    LTileLayer,
    LMarker,
  },
  data: () => ({
    url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
    zoom: 17,
    center: [46.36, 25.802],
    markerLatLng: [46.3604, 25.802],
    mapVisible: true,
  }),
  methods: {
  },
  mounted() {
    delete Icon.Default.prototype._getIconUrl;
    Icon.Default.mergeOptions({
      iconRetinaUrl: require("leaflet/dist/images/marker-icon-2x.png"),
      iconUrl: require("leaflet/dist/images/marker-icon.png"),
      shadowUrl: require("leaflet/dist/images/marker-shadow.png"),
    });
  },
};
</script>

<style scoped>
.main {
  width: 45%;
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
</style>
