
<template>
  <nm-container>
    <nm-button @click="value1 = true" type="primary">Open</nm-button>
    <nm-drawer header footer title="Basic Drawer" append-to-body :visible.sync="value1" placement="right">
      <section v-for="item in 100" :key="item">
        <p>{{item}}Some contents</p>
      </section>
    </nm-drawer>
    <Button @click="value2 = true" type="primary">Open</Button>
    <nm-drawer header footer title="Basic Drawer" append-to-body :visible.sync="value2" placement="left">
      <p>Some contents...</p>
      <p>Some contents...</p>
      <p>Some contents...</p>
    </nm-drawer>
  </nm-container>
</template>
<script>
export default {
  data () {
    return {
      value1: false,
      value2: false
    }
  }
}
</script>
