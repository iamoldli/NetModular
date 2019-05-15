<template>
  <section class="nm-icon-picker">
    <div class="nm-icon-picker-input">
      <el-input :placeholder="placeholder" v-model="icon" @change="onChange">
        <template v-slot:prepend>
          <nm-icon :name="icon"/>
        </template>
      </el-input>
    </div>
    <div class="nm-icon-picker-button">
      <nm-button icon="search" @click="panelVisible=true"/>
    </div>
    <panel :visible.sync="panelVisible" @success="onSelect"/>
  </section>
</template>
<script>
import Panel from './panel'
export default {
  name: 'IconPicker',
  components: { Panel },
  data() {
    return {
      icon: this.value,
      panelVisible: false
    }
  },
  props: {
    value: String,
    placeholder: String
  },
  methods: {
    onChange(val) {
      this.$emit('input', val)
    },
    onSelect(icon) {
      if (icon !== '') {
        this.icon = icon
        this.onChange(icon)
      }
    }
  },
  watch: {
    value() {
      this.icon = this.value
    }
  }
}
</script>
