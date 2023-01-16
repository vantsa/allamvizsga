<template>
    <v-card class="hatter">
      <v-card-title class="justify-center">
        Regisztráció
      </v-card-title>
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
          <v-checkbox v-model="acceptTos" label="Elfogadom a felhasználói feltételeket"></v-checkbox>
          <v-checkbox size="small" v-model="signupForNewsletter" label="Feliratkozom a hírlevélre"></v-checkbox>
        </v-form>
      </v-card-text>
      <p class="jobbalso"> Van már fiókod ? <a class="blue--text" href="#" @click="dialog = false">Jelentkezz be itt !</a></p>
      <v-card-actions class="justify-center" >
        <v-btn rounded
         color="#3E1E68"
          text @click="dialog = false">Vissza</v-btn>
        <v-btn rounded 
        color="#3E1E68"
         class="reg"
          text @click="submit" :disabled="!valid">Regisztráció</v-btn>
      </v-card-actions>
      
    </v-card>
    

</template>

<script>
export default {
  data: () => ({
    dialog: false,
    valid: true,
    firstName: '',
    lastName: '',
    username: '',
    password: '',
    confirmPassword: '',
    showPassword: false,
    showConfirmPassword: false,
    acceptTos: false,
    signupForNewsletter: false,
    nameRules: [
      v => !!v || 'Name is required',
      v => (v && v.length <= 40) || 'Name must be less than 40 characters'
    ],
    usernameRules: [
      v => !!v || 'Username is required',
      v => (v && v.length <= 20) || 'Username must be less than 20 characters'
    ],
    passwordRules: [
      v => !!v || 'Password is required',
      v => (v && v.length >= 8) || 'Password must be at least 8 characters'
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