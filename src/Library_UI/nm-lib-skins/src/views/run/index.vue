<template>
  <nm-box page header title="代码预览" icon="develop" no-scrollbar>
    <template v-slot:toolbar-before>
      <span style="position: relative; top: -13px;left:-15px">比例</span>
      <div style="display:inline-block;width:200px">
        <el-slider v-model="slider"></el-slider>
      </div>
    </template>
    <div class="nm-run-wrapper">
      <vuep ref="vuep" v-if="show" :template="template"></vuep>
    </div>
  </nm-box>
</template>
<script>
export default {
  data () {
    return {
      template: '',
      show: false,
      slider: 50
    }
  },
  methods: {
    receiveMessage (event) {
      if (typeof event.data === 'string') {
        this.template = event.data.replace('export default {', 'module.exports = {')
        this.show = true
        event.source.postMessage('1', event.origin)
        window.removeEventListener('message', this.receiveMessage, false)
      }
    }
  },
  created () {
    window.addEventListener('message', this.receiveMessage, false)
  },
  watch: {
    slider (val) {
      this.$refs.vuep.$el.querySelector('.vuep-editor').style.width = val + '%'
      this.$refs.vuep.$el.querySelector('.vuep-preview').style.width = 100 - val + '%'
    }
  }
}
</script>
<style lang="scss">
.nm-run-wrapper {
  position: absolute;
  width: 100%;
  height: 100%;
}
.vuep {
  height: 100%;
}
.vuep-preview {
  padding: 5px;
  background: #e1e6ef;
}
</style>
