<template>
  <div>
    <transition name="router-anim" mode="out-in">
      <component v-bind:is="dashboardView" v-if="!isDashboard" key="1"></component>
      <component v-bind:is="descView" v-else key="2"></component>
    </transition>
  </div>
</template>

<script>
import description from "./description";
import dashboard from "./dashboard";

export default {
  data() {
    return {
      isDashboard: false,
      dashboardView: "dashboardView",
      descView: "descView"
    };
  },
  mounted: function() {
    EventBus.$on("changeView", viewChanged => {
      this.isDashboard = viewChanged;
    });
  },
  components: {
    dashboardView: description,
    descView: dashboard
  }
};
</script>

<style>
.router-anim-enter-active {
  animation: coming 1s;
  /* animation-delay: 0.5s; */
  /* opacity: 0; */
}
.router-anim-leave-active {
  animation: going 1s;
}

@keyframes going {
  from {
    transform: translatey(0);
  }
  to {
    transform: translatey(-110%);
  }
}
@keyframes coming {
  from {
    transform: translatey(90%);
    opacity: 0;
  }
  to {
    transform: translatey(0);
    opacity: 1;
  }
}
</style>
