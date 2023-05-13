<template>
  <div class="background">
    <div v-if="loading"><LoadingScreen /></div>
    <MenuBar />
    <v-spacer />
    <v-card>
      <h1>Profil szerkesztése</h1>
      <v-avatar color="yellow" size="150">
        <span class="avatarcontent"> {{ initials }} </span>
      </v-avatar>
      <h3>{{ username }}</h3>
      <h3 class="utolso">{{ email }}</h3>
      <div class="content">
        <span class="left">Maximális távolság</span>
        <span class="right">{{ value }} km</span>
        <v-slider
          class="slider"
          v-model="value"
          :min="0"
          :max="50"
          :step="1"
          thumb-label
        ></v-slider>
        <span class="left">Korcsoport</span>
        <span class="right">{{ format }}</span>
        <v-range-slider
          class="slider"
          v-model="range"
          :min="16"
          :max="100"
          :step="1"
          thumb-label
        ></v-range-slider>
        <span class="center">Általad létrehozott események</span>
        <span class="number"> {{ events.length }} </span>
      </div>
      <v-card-actions class="justify-end">
        <v-btn rounded class="create-btn" text large @click="submit">
          Mentés
        </v-btn>
      </v-card-actions>
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
    <FooterBar />
  </div>
</template>

<script>
import MenuBar from "../components/MenuBar.vue";
import FooterBar from "../components/FooterBar.vue";
import jwt_decode from "jwt-decode";
import axios from "axios";
import LoadingScreen from "../components/LoadingScreen.vue";

export default {
  name: "ProfileView",
  components: {
    MenuBar,
    FooterBar,
    LoadingScreen,
  },
  data() {
    return {
      value: null,
      range: [0,0],
      username: "",
      email: "",
      initials: "",
      successMsg: "",
      userId: "",
      rangeStart: 0,
      rangeEnd: 0,
      user: {},
      loading: true,
      events: [],
    };
  },
  methods: {
    async submit() {
      try {
        const userResponse = await axios.get(`api/users/${this.userId}`);
        const user = userResponse.data;
        const data = {
          value: this.value,
          rangeStart: this.range[0],
          rangeEnd: this.range[1],
          user: user,
        };
        // eslint-disable-next-line no-unused-vars
        const response = await axios.put(
          `api/users/savesettings/${this.userId}`,
          data,
          {
            headers: {
              "Content-Type": "application/json",
            },
          }
        );
        this.successMsg = "Beállitások mentve!";
      } catch (error) {
        this.successMsg = "";
      }
    },
  },
  computed: {
    format() {
      return this.range[0] + " - " + this.range[1];
    },
  },
  created() {
    const token = localStorage.getItem("jwtToken");
    if (!token) {
      console.error("No token found");
      this.$router.push('/')
    }
    const user = jwt_decode(token);
    this.initials = `${user.firstName.charAt(0)}${user.lastName.charAt(0)}`;
    this.username = user.username;
    this.email = user.email;
    this.userId = user.id;
    setTimeout(() => {
      this.loading = false;
    }, 2000);
  },
  async mounted() {
    try {
      const response = await axios.get(`api/users/userprofile/${this.userId}`);
      this.value = response.data.value;
      this.rangeStart = response.data.rangeStart;
      this.rangeEnd = response.data.rangeEnd;
      this.range = [this.rangeStart, this.rangeEnd];
      this.user = response.data.user;
    } catch (error) {
      console.error(error);
    }
    axios
      .get(`/api/events/user/${this.userId}`)
      .then((response) => {
        this.events = response.data;
      })
      .catch((error) => {
        console.error(error);
      });
  },
};
</script>

<style scoped>
.background {
  background-color: #3e1e68;
  background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='100%25'%3E%3Cdefs%3E%3ClinearGradient id='a' gradientUnits='userSpaceOnUse' x1='0' x2='0' y1='0' y2='100%25' gradientTransform='rotate(319,960,464)'%3E%3Cstop offset='0' stop-color='%233E1E68'/%3E%3Cstop offset='1' stop-color='%238F286E'/%3E%3C/linearGradient%3E%3Cpattern patternUnits='userSpaceOnUse' id='b' width='467' height='389.2' x='0' y='0' viewBox='0 0 1080 900'%3E%3Cg fill-opacity='0.02'%3E%3Cpolygon fill='%23444' points='90 150 0 300 180 300'/%3E%3Cpolygon points='90 150 180 0 0 0'/%3E%3Cpolygon fill='%23AAA' points='270 150 360 0 180 0'/%3E%3Cpolygon fill='%23DDD' points='450 150 360 300 540 300'/%3E%3Cpolygon fill='%23999' points='450 150 540 0 360 0'/%3E%3Cpolygon points='630 150 540 300 720 300'/%3E%3Cpolygon fill='%23DDD' points='630 150 720 0 540 0'/%3E%3Cpolygon fill='%23444' points='810 150 720 300 900 300'/%3E%3Cpolygon fill='%23FFF' points='810 150 900 0 720 0'/%3E%3Cpolygon fill='%23DDD' points='990 150 900 300 1080 300'/%3E%3Cpolygon fill='%23444' points='990 150 1080 0 900 0'/%3E%3Cpolygon fill='%23DDD' points='90 450 0 600 180 600'/%3E%3Cpolygon points='90 450 180 300 0 300'/%3E%3Cpolygon fill='%23666' points='270 450 180 600 360 600'/%3E%3Cpolygon fill='%23AAA' points='270 450 360 300 180 300'/%3E%3Cpolygon fill='%23DDD' points='450 450 360 600 540 600'/%3E%3Cpolygon fill='%23999' points='450 450 540 300 360 300'/%3E%3Cpolygon fill='%23999' points='630 450 540 600 720 600'/%3E%3Cpolygon fill='%23FFF' points='630 450 720 300 540 300'/%3E%3Cpolygon points='810 450 720 600 900 600'/%3E%3Cpolygon fill='%23DDD' points='810 450 900 300 720 300'/%3E%3Cpolygon fill='%23AAA' points='990 450 900 600 1080 600'/%3E%3Cpolygon fill='%23444' points='990 450 1080 300 900 300'/%3E%3Cpolygon fill='%23222' points='90 750 0 900 180 900'/%3E%3Cpolygon points='270 750 180 900 360 900'/%3E%3Cpolygon fill='%23DDD' points='270 750 360 600 180 600'/%3E%3Cpolygon points='450 750 540 600 360 600'/%3E%3Cpolygon points='630 750 540 900 720 900'/%3E%3Cpolygon fill='%23444' points='630 750 720 600 540 600'/%3E%3Cpolygon fill='%23AAA' points='810 750 720 900 900 900'/%3E%3Cpolygon fill='%23666' points='810 750 900 600 720 600'/%3E%3Cpolygon fill='%23999' points='990 750 900 900 1080 900'/%3E%3Cpolygon fill='%23999' points='180 0 90 150 270 150'/%3E%3Cpolygon fill='%23444' points='360 0 270 150 450 150'/%3E%3Cpolygon fill='%23FFF' points='540 0 450 150 630 150'/%3E%3Cpolygon points='900 0 810 150 990 150'/%3E%3Cpolygon fill='%23222' points='0 300 -90 450 90 450'/%3E%3Cpolygon fill='%23FFF' points='0 300 90 150 -90 150'/%3E%3Cpolygon fill='%23FFF' points='180 300 90 450 270 450'/%3E%3Cpolygon fill='%23666' points='180 300 270 150 90 150'/%3E%3Cpolygon fill='%23222' points='360 300 270 450 450 450'/%3E%3Cpolygon fill='%23FFF' points='360 300 450 150 270 150'/%3E%3Cpolygon fill='%23444' points='540 300 450 450 630 450'/%3E%3Cpolygon fill='%23222' points='540 300 630 150 450 150'/%3E%3Cpolygon fill='%23AAA' points='720 300 630 450 810 450'/%3E%3Cpolygon fill='%23666' points='720 300 810 150 630 150'/%3E%3Cpolygon fill='%23FFF' points='900 300 810 450 990 450'/%3E%3Cpolygon fill='%23999' points='900 300 990 150 810 150'/%3E%3Cpolygon points='0 600 -90 750 90 750'/%3E%3Cpolygon fill='%23666' points='0 600 90 450 -90 450'/%3E%3Cpolygon fill='%23AAA' points='180 600 90 750 270 750'/%3E%3Cpolygon fill='%23444' points='180 600 270 450 90 450'/%3E%3Cpolygon fill='%23444' points='360 600 270 750 450 750'/%3E%3Cpolygon fill='%23999' points='360 600 450 450 270 450'/%3E%3Cpolygon fill='%23666' points='540 600 630 450 450 450'/%3E%3Cpolygon fill='%23222' points='720 600 630 750 810 750'/%3E%3Cpolygon fill='%23FFF' points='900 600 810 750 990 750'/%3E%3Cpolygon fill='%23222' points='900 600 990 450 810 450'/%3E%3Cpolygon fill='%23DDD' points='0 900 90 750 -90 750'/%3E%3Cpolygon fill='%23444' points='180 900 270 750 90 750'/%3E%3Cpolygon fill='%23FFF' points='360 900 450 750 270 750'/%3E%3Cpolygon fill='%23AAA' points='540 900 630 750 450 750'/%3E%3Cpolygon fill='%23FFF' points='720 900 810 750 630 750'/%3E%3Cpolygon fill='%23222' points='900 900 990 750 810 750'/%3E%3Cpolygon fill='%23222' points='1080 300 990 450 1170 450'/%3E%3Cpolygon fill='%23FFF' points='1080 300 1170 150 990 150'/%3E%3Cpolygon points='1080 600 990 750 1170 750'/%3E%3Cpolygon fill='%23666' points='1080 600 1170 450 990 450'/%3E%3Cpolygon fill='%23DDD' points='1080 900 1170 750 990 750'/%3E%3C/g%3E%3C/pattern%3E%3C/defs%3E%3Crect x='0' y='0' fill='url(%23a)' width='100%25' height='100%25'/%3E%3Crect x='0' y='0' fill='url(%23b)' width='100%25' height='100%25'/%3E%3C/svg%3E");
  background-attachment: fixed;
  background-size: cover;
}
*{
  font-family: 'Baloo-Regular';
}
.v-card {
  width: 50%;
  margin: 0 auto;
  margin-top: 4rem;
  background-color: rgba(255, 255, 255, 0.35);
  border-radius: 4%;
}
h1 {
  text-align: center;
  padding: 1rem 0rem;
  border-bottom: 1px solid lightgray;
}
.v-avatar {
  margin: 2rem auto;
  margin-bottom: 1rem;
  display: block;
}
h3 {
  text-align: center;
}
.utolso {
  margin-bottom: 3rem;
}
.slider {
  display: block;
  margin-top: 1rem;
  width: 100%;
  margin: 0 auto;
}
.content {
  width: 60%;
  margin: 0 auto;
}
.left {
  float: left;
}
.right {
  float: right;
}
.center {
  display: table;
  margin: 0 auto;
}
.number {
  display: table;
  margin: 0 auto;
  font-size: 2rem;
}
span {
  font-weight: 600;
  padding: 0.5rem;
}
.create-btn {
  background-color: #3e1e68;
  color: white;
  margin: 0 4rem 1.5rem 0;
  font-size: 1rem;
}
.avatarcontent {
  font-size: 4rem;
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 1.75rem;
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