<template>
  <section class="nm-demo">
    <h1 class="title">示例：{{options.title}}</h1>
    <el-tabs type="border-card">
      <el-tab-pane :style="{height}" label="示例">
        <slot/>
      </el-tab-pane>
      <el-tab-pane :style="{height}" label="HTML" v-if="options.html">
        <nm-scrollbar class="nm-demo-scrollbar">
          <pre v-highlightjs="options.html"><code class="html"></code></pre>
        </nm-scrollbar>
      </el-tab-pane>
      <el-tab-pane :style="{height}" label="JavaScript" v-if="options.js">
        <nm-scrollbar class="nm-demo-scrollbar">
          <pre v-highlightjs="options.js"><code class="javascript"></code></pre>
        </nm-scrollbar>
      </el-tab-pane>
      <el-tab-pane :style="{height}" label="Css" v-if="options.css">
        <nm-scrollbar class="nm-demo-scrollbar">
          <pre v-highlightjs="options.css"><code class="css"></code></pre>
        </nm-scrollbar>
      </el-tab-pane>
    </el-tabs>
  </section>
</template>
<script>
export default {
  computed: {
    height () {
      return this.options.height + 'px'
    }
  },
  props: {
    options: Object
  },
  methods: {
    toggle () {
      this.visible = !this.visible
    }
  }
}
</script>

<style lang="scss">
$border: 1px solid #ccc;
.nm-demo {
  margin-bottom: 30px;
  .title {
    margin: 10px 0;
    font-size: 25px;
  }
  &-scrollbar.el-scrollbar {
    height: 100%;
    .el-scrollbar__wrap {
      overflow-x: auto;
      .el-scrollbar__view {
        min-height: 100%;
        background: #f8f8f8;
      }
    }
  }
  .el-tabs__content {
    padding: 0;
  }
  .el-tab-pane {
    height: 100%;
    &:nth-child(1) {
      padding: 10px;
      position: relative;
      background: #f0f4f5;
    }
    pre {
      margin: 0;
      code {
        padding: 15px;
        box-sizing: border-box;
      }
    }
  }
}
</style>
