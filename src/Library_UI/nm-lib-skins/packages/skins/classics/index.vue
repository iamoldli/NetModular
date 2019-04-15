<template>
  <section style="position:relative;height:100%">
    <h1>经典皮肤
      <el-button @click="skinToggle('classics')">主题切换</el-button>
    </h1>
    <div style="position:relative;height:100%">
      <router-view/>
    </div>
  </section>
</template>
<script>
import { mapMutations } from 'vuex'
export default {
  methods: {
    ...mapMutations('app', ['skinToggle'])
  }
}
</script>
