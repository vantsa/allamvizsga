<template>
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
      <v-card-actions class="justify-center" >
        <v-btn rounded 
        color="#3E1E68"
         class="reg"
          text @click="submit" :disabled="!valid">Bejelentkezés</v-btn>
      </v-card-actions>
      
    </v-card>
    

</template>

<script>
export default {
  data: () => ({
    dialog: false,
    valid: true,
    username: '',
    password: '',
    confirmPassword: '',
    showPassword: false,
    usernameRules: [
      v => !!v || 'Felhaszálónév kötelező'
    ],
    passwordRules: [
      v => !!v || 'Jelszó kötelező',
    ]
  }),
  methods: {
    submit() {
      if (this.$refs.form.validate()) {
        // handle form submission here
      }
    }
  }
}
</script>

<style scoped>
.jobbalso{
    margin-left: 2em;
    size: 1em;
}
.blue--text{
    text-decoration: none;
}
.v-btn{
    background-color: white;
}
.v-btn.reg{
    background-color: yellow;
}
.v-card.hatter{
    background-color: lightgray;
}
</style>