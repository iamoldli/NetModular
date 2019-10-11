<template>
  <nm-dialog v-bind="dialog" :visible.sync="visible_" @open="onOpen">
    <template v-slot:toolbar>
      <nm-button icon="refresh" @click="refresh" />
    </template>
    <div class="top">
      <el-tabs v-model="active" type="card">
        <el-tab-pane name="query">
          <span slot="label">
            <nm-icon name="search" />查询模型
          </span>
        </el-tab-pane>
        <el-tab-pane name="add">
          <span slot="label">
            <nm-icon name="add" />添加模型
          </span>
        </el-tab-pane>
        <el-tab-pane name="update">
          <span slot="label">
            <nm-icon name="edit" />编辑模型
          </span>
        </el-tab-pane>
      </el-tabs>
    </div>
    <div class="bottom">
      <list-page ref="list" :parent="parent" :model-type="modelType" />
    </div>
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import ListPage from '../components/list'
export default {
  mixins: [mixins.dialog],
  components: { ListPage },
  data() {
    return {
      active: 'query'
    }
  },
  props: {
    parent: {
      type: Object,
      required: true
    }
  },
  computed: {
    dialog() {
      return {
        class: 'nm-code-generator-model-property',
        title: `模型属性列表(${this.parent.name})`,
        icon: 'list',
        width: '70%'
      }
    },
    modelType() {
      return this.active === 'query' ? 1 : this.active === 'add' ? 2 : 3
    }
  },
  methods: {
    refresh() {
      this.$refs.list.refresh()
    },
    onOpen() {
      this.$nextTick(() => {
        this.active = 'query'
        this.$refs.list.reset()
        this.$refs.list.refresh()
      })
    }
  }
}
</script>
<style lang="scss">
.nm-code-generator-model-property {
  .top {
    flex-shrink: 0;
    .el-tabs__header {
      margin: 0;
    }
  }

  .bottom {
    flex-grow: 1;
    height: 500px;
  }
}
</style>
