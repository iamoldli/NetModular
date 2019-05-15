<template>
  <nm-box page>
    <nm-button @click="drawer1=true">简单抽屉</nm-button>
    <nm-button @click="drawer2=true">左侧抽屉</nm-button>
    <nm-button @click="drawer3=true">在附加到body</nm-button>
    <nm-button @click="drawer4=true">可拖拽</nm-button>
    <nm-drawer header title="简单抽屉" :visible.sync="drawer1"></nm-drawer>
    <nm-drawer header title="左侧抽屉" placement="left" :visible.sync="drawer2"></nm-drawer>
    <nm-drawer header title="在附加到body" append-to-body :visible.sync="drawer3"></nm-drawer>
    <nm-drawer header title="可拖拽" draggable :visible.sync="drawer4"></nm-drawer>
  </nm-box>
</template>
<script>
export default {
  data() {
    return {
      drawer1: false,
      drawer2: false,
      drawer3: false,
      drawer4: false
    }
  }
}
</script>
