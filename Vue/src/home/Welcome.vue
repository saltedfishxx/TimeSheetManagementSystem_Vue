<template>
  <div class="spaceContainer has-text-centered">
    <div id="str"></div>
  </div>
</template>

<style>
.spaceContainer {
  height: 100%;
  margin-top: 5em;
}

#str {
  font-size: 10em;
  color: rgb(99, 99, 99)
}
</style>

<script>
import { router, authHeader } from "../_helpers";

export default {
  data() {
    return {};
  },
  mounted() {
    var string = "WELCOME! :)";
    var str = string.split("");
    var el = document.getElementById("str");
    (function animate() {
      str.length > 0 ? (el.textContent += str.shift()) : clearTimeout(running);
      var running = setTimeout(animate, 100);
    })();
    setTimeout(() => this.$router.push({ path: "/login" }), 3000);
  }
};
</script>