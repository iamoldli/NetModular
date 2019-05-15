<template>
  <div class="nm-code-preview" :class="{show:visible}">
    <nm-button class="nm-code-preview-btn" type="warning" icon="develop" @click="onCBtnlick"/>
    <nm-drawer class="nm-code-preview-drawer" v-bind="drawer" :visible.sync="visible">
      <template v-slot:toolbar>
        <nm-button icon="transmit" @click="run"/>
      </template>
      <pre v-highlightjs="code"><code class="html"></code></pre>
    </nm-drawer>
  </div>
</template>
<script>
export default {
  data() {
    return {
      visible: false,
      drawer: {
        header: true,
        title: '代码预览',
        icon: 'preview',
        width: '50%',
        draggable: true
      },
      timer: null
    }
  },
  props: ['code'],
  methods: {
    onCBtnlick() {
      this.visible = !this.visible
    },
    run() {
      window.addEventListener('message', this.receiveMessage, false)
      const runUrl = window.location.href.split('#')[0] + '#/run'
      const runWindow = window.open(runUrl, '_blank')
      this.timer = setInterval(() => {
        runWindow.postMessage(this.code, runUrl)
      }, 300)
    },
    receiveMessage(event) {
      if (this.timer && event.origin === window.location.origin) {
        clearInterval(this.timer)
      }
    }
  }
}
</script>
<style lang="scss">
.nm-code-preview {
  &-btn {
    padding: 0;
    position: absolute;
    right: 5px;
    bottom: 5px;
    width: 44px;
    height: 44px;
    line-height: 44px;
    border-radius: 22px;
    font-size: 1.5rem;
    text-align: center;
    z-index: 9999;
    transition: all 0.3s ease-in-out;
  }
}
.nm-code-preview-drawer {
  .nm-drawer-modal,
  .nm-drawer-dialog {
    z-index: 9998;
  }
  .el-scrollbar__wrap {
    background: #2b2b2b;
  }
  .el-scrollbar__view {
    padding: 0;
  }
}
</style>
