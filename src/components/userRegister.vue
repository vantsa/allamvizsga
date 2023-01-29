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
          <v-text-field v-model="birthdate" readonly :rules="birthdateRules" label="Születési dátum" @click="bdPicker = true"></v-text-field>
            <v-dialog v-model="bdPicker" width="350">
              <v-card>
                <v-card-text>
                   <v-layout align-center justify-center>
                  <v-date-picker v-model="birthdate" :rules="birthdateRules" :year-size="2" required min="1905-01-01" max="2022-12-31"  ></v-date-picker>
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
          <v-checkbox v-model="acceptTos" label="Elfogadom a felhasználói feltételeket"></v-checkbox>
          <v-checkbox size="small" v-model="signupForNewsletter" label="Feliratkozom a hírlevélre"></v-checkbox>
        </v-form>
      </v-card-text>
      <p class="jobbalso"> Nincs még fiókod ? <a class="blue--text" href="#" @click="dialog = false">Regisztrálj itt !</a></p>
      <v-card-actions class="justify-center" >
        <v-btn rounded 
        color="#3E1E68"
         class="reg"
          text @click="submit" :disabled="!valid">Regisztráció</v-btn>
      </v-card-actions>
      
    </v-card>
    

</template>

<script>
import axios from 'axios'


export default {
  data: () => ({
    dialog: false,
    bdPicker: false,
    valid: true,
    firstName: '',
    lastName: '',
    email:'',
    username: '',
    password: '',
    confirmPassword: '',
    showPassword: false,
    showConfirmPassword: false,
    acceptTos: false,
    signupForNewsletter: false,
    nameRules: [
      v => !!v || 'Név kötelező',
      v => (v && v.length <= 40) || 'A név hossza, kevesebb kell legyen mint 40 karakter!'
    ],
    emailRules: [
        v => !!v || 'E-mail kötelező',
        v => /.+@.+/.test(v) || 'Érvénytelen formátum',
      ],
    usernameRules: [
      v => !!v || 'Felhasználónév kötelező',
      v => (v && v.length <= 20) || 'A felhasználónév hossza, kevesebb kell legyen mint 20 karakter!'
    ],
    passwordRules: [
      v => !!v || 'Jelszó kötelező',
      v => (v && v.length >= 8 || 'A jelszó legalább 8 karaktert kell tartalmazzon!')
    ],
    birthdate: null,
    birthdateRules: [(v) => {
      if (!v) return 'Születési dátum kötelező';
      let ageDifMs = Date.now() - new Date(v).getTime();
      let ageDate = new Date(ageDifMs);
      return (Math.abs(ageDate.getUTCFullYear() - 1970) >= 16) || 'Legalább 16 évesnek kell lenned !';
    }],
  }),
  methods: {
    submit() {
      if (this.$refs.form.validate()) {
        const data = {
        firstName: this.firstName,
        lastName: this.lastName,
        birthdate: this.birthdate,
        username: this.username,
        password: this.password,
        email: this.email
      }
      axios.post('http://localhost/lecreventApi/register', data)
      }
    },
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