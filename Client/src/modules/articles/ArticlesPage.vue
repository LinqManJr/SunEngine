<template>
  <q-page class="articles-page">
    <div class="page-padding header-with-button">
      <h2 class="q-title">
        {{category.title}}
      </h2>
      <q-btn no-caps class="post-btn"
             @click="$router.push({name:'CreateMaterial',params:{categoriesNames: category.name, initialCategoryName: category.name}})"
             :label="$tl('newArticleBtn')" v-if="articles && canAddArticle" icon="fas fa-plus"/>

    </div>
    <div v-if="category.header" class="q-mb-sm page-padding" v-html="category.header"></div>


    <ArticlesList v-if="articles" :articles="articles"/>

    <LoaderWait ref="loader" v-else/>

    <q-pagination class="page-padding q-mt-md" v-if="articles && articles.totalPages > 1"
                  v-model="articles.pageIndex"
                  color="pagination"
                  :max-pages="12"
                  :max="articles.totalPages"
                  ellipses
                  direction-links
                  @input="pageChanges"/>
  </q-page>
</template>

<script>
    import {Page} from 'mixins'
    import {Pagination} from 'mixins'


    export default {
        name: 'ArticlesPage',
        mixins: [Page, Pagination],
        props: {
            categoryName: {
                type: String,
                required: true
            }
        },
        data() {
            return {
                articles: null
            }
        },
        watch: {
            '$route': 'loadData'
        },
        computed: {
            category() {
                return this.$store.getters.getCategory(this.categoryName);
            },
            canAddArticle() {
                return this.category?.categoryPersonalAccess?.materialWrite;
            }
        },
        methods: {
            loadData() {
                this.title = this.category?.title;

                this.$request(this.$Api.Articles.GetArticles,
                    {
                        categoryName: this.categoryName,
                        page: this.currentPage,
                        showDeleted: (this.$store.state.admin.showDeletedElements || this.$route.query.deleted) ? true : undefined
                    }
                ).then(response => {
                    this.articles = response.data;
                }).catch(x => {
                    this.$refs.loader.fail();
                });
            }
        },
        beforeCreate() {
            this.$options.components.LoaderWait = require('sun').LoaderWait;
            this.$options.components.ArticlesList = require('sun').ArticlesList;
        },
        created() {
            this.loadData()
        }
    }

</script>

<style lang="stylus">

</style>
