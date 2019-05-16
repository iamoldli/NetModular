<template>
  <nm-split v-model="split">
    <template v-slot:left>
      <nm-box page>左侧面板</nm-box>
    </template>
    <template v-slot:right>
      <nm-split v-model="split1" mode="vertical">
        <template v-slot:top>
          <nm-box page>上方面板</nm-box>
        </template>
        <template v-slot:bottom>
          <nm-box page>下方面板</nm-box>
        </template>
      </nm-split>
    </template>
  </nm-split>
</template>
<script>
export default {
  data () {
    return {
      split: 0.3,
      split1: 0.3
    }
  }
}
</script>
