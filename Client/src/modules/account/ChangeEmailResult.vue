<template>
  <q-page class="change-email-result flex middle page-padding">
    <q-banner v-if="success" class="bg-positive text-white">
      <template v-slot:avatar>
        <q-icon name="fas fa-check-circle" size="2em"/>
      </template>
      {{$tl("success")}}
    </q-banner>
    <q-banner v-else class="bg-negative text-white">
      <template v-slot:avatar>
        <q-icon name="fas fa-exclamation-circle" size="2em"/>
      </template>
      {{$tl("error")}}
    </q-banner>
  </q-page>
</template>

<script>
  import {Page} from 'mixins'

  export default {
    name: "ChangeEmailResult",
    mixins: [Page],
    computed: {
      success() {
        return this.$route.query["result"] === "ok";
      }
    },
    created() {
      this.title = this.$tl("title");
    }
  }

</script>

<style lang="stylus">

</style>
