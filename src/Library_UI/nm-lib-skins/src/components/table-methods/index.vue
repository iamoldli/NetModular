<template>
  <section class="nm-table-methods">
    <h1 class="nm-table-methods-title">方法(Method)</h1>
    <el-table :data="data" border style="width: 100%">
      <el-table-column prop="name" label="名称" width="200"/>
      <el-table-column prop="desc" label="说明"/>
    </el-table>
  </section>
</template>
<script>
export default {
  props: ['data']
}
</script>
<style lang="scss">
.nm-table-methods {
  margin-bottom: 30px;
  &-title {
    margin: 10px 0;
    font-size: 25px;
  }
}
</style>
