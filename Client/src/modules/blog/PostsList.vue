<template>
  <div class="posts-list">

    <template v-if="posts.items">
      <div v-for="post in posts.items" :key="post.id">
        <Post :post="post"/>
        <hr class="hr-sep"/>
      </div>
    </template>

  </div>
</template>

<script>

  export default {
    name: 'PostsList',
    props: {
        posts: {
            type: Object,
            required: true
        }
    },
    beforeCreate() {
      this.$options.components.Post = require('sun').Post;
    }
  }

</script>

<style lang="stylus">

  .posts-list {
    .hr-sep {
      height: 0;
      border-top: dashed #d3eecc 1px !important;
      border-left: none;
    }
  }

</style>
