<template>
<div class="home"> 
    <div class="background">
    <div class="comps">
        <LandPageHeader/>
        <HelloWorld/>
    </div>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
         <span></span>
  </div>
</div>
  
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import LandPageHeader from '@/components/LandPageHeader.vue'
///import Background from '@/components/Background.vue'

export default {
  name: 'HomeView',
  components: {
    HelloWorld,
    LandPageHeader,
    ///Background,
  },
  mounted(){
    if(localStorage.getItem("jwtToken"))
    {
        this.$router.push('/feed')
    }
  }
}
</script>

<style scoped>
@keyframes move {
    100% {
        transform: translate3d(0, 0, 1px) rotate(360deg);
    }
}

.background {
    position: fixed;
    width: 100vw;
    height: 100vh;
    top: 0;
    left: 0;
    background: #3E1E68;
    overflow: hidden;
}

.background span {
    width: 20vmin;
    height: 20vmin;
    border-radius: 20vmin;
    backface-visibility: hidden;
    position: absolute;
    z-index: -1000;
    animation: move;
    animation-duration: 25;
    animation-timing-function: linear;
    animation-iteration-count: infinite;
}


.background span:nth-child(0) {
    color: #583C87;
    top: 84%;
    left: 38%;
    animation-duration: 9s;
    animation-delay: -131s;
    transform-origin: 4vw -23vh;
    box-shadow: -40vmin 0 5.870181116061071vmin currentColor;
}
.background span:nth-child(1) {
    color: #583C87;
    top: 83%;
    left: 91%;
    animation-duration: 108s;
    animation-delay: -149s;
    transform-origin: -17vw -11vh;
    box-shadow: 40vmin 0 5.072802001039467vmin currentColor;
}
.background span:nth-child(2) {
    color: #E45A84;
    top: 95%;
    left: 38%;
    animation-duration: 32s;
    animation-delay: -7s;
    transform-origin: 10vw 10vh;
    box-shadow: 40vmin 0 5.73362370171799vmin currentColor;
}
.background span:nth-child(3) {
    color: #583C87;
    top: 23%;
    left: 42%;
    animation-duration: 53s;
    animation-delay: -236s;
    transform-origin: 12vw -10vh;
    box-shadow: 40vmin 0 5.183975440729551vmin currentColor;
}
.background span:nth-child(4) {
    color: #E45A84;
    top: 13%;
    left: 17%;
    animation-duration: 188s;
    animation-delay: -158s;
    transform-origin: 9vw -10vh;
    box-shadow: 40vmin 0 5.4656362019920515vmin currentColor;
}
.background span:nth-child(5) {
    color: #E45A84;
    top: 44%;
    left: 85%;
    animation-duration: 240s;
    animation-delay: -166s;
    transform-origin: -12vw -6vh;
    box-shadow: -40vmin 0 5.610326589495565vmin currentColor;
}
.background span:nth-child(6) {
    color: #583C87;
    top: 6%;
    left: 65%;
    animation-duration: 147s;
    animation-delay: -147s;
    transform-origin: -11vw -2vh;
    box-shadow: 40vmin 0 5.611360986438118vmin currentColor;
}
.background span:nth-child(7) {
    color: #FFACAC;
    top: 13%;
    left: 90%;
    animation-duration: 161s;
    animation-delay: -208s;
    transform-origin: 4vw 21vh;
    box-shadow: -40vmin 0 5.630932901927192vmin currentColor;
}
.background span:nth-child(8) {
    color: #FFACAC;
    top: 8%;
    left: 13%;
    animation-duration: 186s;
    animation-delay: -100s;
    transform-origin: 12vw -2vh;
    box-shadow: 40vmin 0 5.837111606945961vmin currentColor;
}
.background span:nth-child(9) {
    color: #E45A84;
    top: 61%;
    left: 5%;
    animation-duration: 230s;
    animation-delay: -205s;
    transform-origin: -21vw 21vh;
    box-shadow: -40vmin 0 5.658654495456642vmin currentColor;
}
.background span:nth-child(10) {
    color: #583C87;
    top: 30%;
    left: 34%;
    animation-duration: 159s;
    animation-delay: -152s;
    transform-origin: 8vw 21vh;
    box-shadow: 40vmin 0 5.8814656355194055vmin currentColor;
}
.background span:nth-child(11) {
    color: #E45A84;
    top: 75%;
    left: 49%;
    animation-duration: 190s;
    animation-delay: -138s;
    transform-origin: -24vw -3vh;
    box-shadow: -40vmin 0 5.282384542091724vmin currentColor;
}
.background span:nth-child(12) {
    color: #FFACAC;
    top: 98%;
    left: 27%;
    animation-duration: 33s;
    animation-delay: -61s;
    transform-origin: -12vw -9vh;
    box-shadow: -40vmin 0 5.365079915292089vmin currentColor;
}
.background span:nth-child(13) {
    color: #583C87;
    top: 25%;
    left: 77%;
    animation-duration: 174s;
    animation-delay: -246s;
    transform-origin: 24vw -4vh;
    box-shadow: 40vmin 0 5.539832363580388vmin currentColor;
}
.background span:nth-child(14) {
    color: #583C87;
    top: 87%;
    left: 54%;
    animation-duration: 204s;
    animation-delay: -75s;
    transform-origin: -23vw 17vh;
    box-shadow: 40vmin 0 5.140532489098769vmin currentColor;
}
.background span:nth-child(15) {
    color: #FFACAC;
    top: 71%;
    left: 12%;
    animation-duration: 117s;
    animation-delay: -146s;
    transform-origin: -7vw -13vh;
    box-shadow: 40vmin 0 5.757722574847255vmin currentColor;
}
.background span:nth-child(16) {
    color: #583C87;
    top: 25%;
    left: 16%;
    animation-duration: 91s;
    animation-delay: -235s;
    transform-origin: 16vw 13vh;
    box-shadow: 40vmin 0 5.686719984095317vmin currentColor;
}
.background span:nth-child(17) {
    color: #FFACAC;
    top: 44%;
    left: 59%;
    animation-duration: 54s;
    animation-delay: -118s;
    transform-origin: -11vw -5vh;
    box-shadow: -40vmin 0 5.416193879420436vmin currentColor;
}
.background span:nth-child(18) {
    color: #FFACAC;
    top: 26%;
    left: 37%;
    animation-duration: 190s;
    animation-delay: -189s;
    transform-origin: 7vw -19vh;
    box-shadow: -40vmin 0 5.099836666001359vmin currentColor;
}
.background span:nth-child(19) {
    color: #FFACAC;
    top: 40%;
    left: 9%;
    animation-duration: 112s;
    animation-delay: -196s;
    transform-origin: -13vw -23vh;
    box-shadow: 40vmin 0 5.3223450634491vmin currentColor;
}
</style>