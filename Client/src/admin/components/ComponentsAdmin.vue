<template>
  <q-page class="components-admin page-padding">
    <div class="header-with-button">
      <h2 class="q-title">
        {{$tl("title")}}
      </h2>
      <q-btn icon="fas fa-plus" class="post-btn q-mr-lg" type="a" :to="{name: 'CreateComponent'}" no-caps
             :label="$tl('addComponentBtn')"/>
      <div class="clear"></div>
    </div>

    <div class="components" v-if="components">
      <div v-for="component in components">
        <q-icon name="fas fa-cube" class="q-mr-xs"/>
        {{component.name}}
        <q-btn color="info" class="q-ml-sm" dense size="10px" flat icon="fas fa-wrench"
               :to="{name: 'EditComponent', params: {name: component.name}}"/>

        <q-btn color="info" dense size="10px" flat icon="fas fa-arrow-right" :to="'/'+component.name.toLowerCase()"/>

        <span class="q-ml-lg text-grey-7">[{{component.type}}]</span>
      </div>
    </div>
    <LoaderWait v-else/>
  </q-page>
</template>

<script>
    import {Page} from 'mixins';


    export default {
        name: "ComponentsAdmin",
        mixins: [Page],
        data() {
            return {
                components: null
            }
        },
        methods: {
            loadData() {
                this.$request(
                    this.$AdminApi.ComponentsAdmin.GetAllComponents
                ).then(response => {
                    this.components = response.data;
                })
            }
        },
        beforeCreate() {
            this.$options.components.LoaderWait = require('sun').LoaderWait;
        },
        created() {
            this.title = this.$tl('title');
            this.loadData();
        }
    }

</script>

<style lang="stylus">

  .components-admin {
    .components {
      font-size: 1.15em;
    }
  }

</style>
