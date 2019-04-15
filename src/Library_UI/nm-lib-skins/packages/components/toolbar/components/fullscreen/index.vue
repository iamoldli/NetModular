<template>
  <div class="nm-toolbar-item">
    <nm-button class="nm-toolbar-button" type="text" @click="toggle">
      <nm-icon :name="icon"/>
    </nm-button>
  </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
export default {
  computed: {
    ...mapState('app/fullscreen', ['active']),
    icon () {
      return this.active ? 'fullscreen-c' : 'fullscreen-o'
    }
  },
  methods: {
    ...mapActions('app/fullscreen', ['toggle'])
  }
}
</script>
