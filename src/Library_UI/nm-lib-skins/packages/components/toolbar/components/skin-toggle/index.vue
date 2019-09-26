<template>
  <div class="nm-toolbar-item">
    <nm-button class="nm-toolbar-button" type="text" @click="open">
      <nm-icon name="skin" />
    </nm-button>

    <toggle-form :visible.sync="visible" />
  </div>
</template>
<script>
import ToggleForm from './form'
export default {
  components: { ToggleForm },
  data() {
    return {
      visible: false
    }
  },
  methods: {
    open() {
      this.visible = true
    }
  }
}
</script>
