<template>
  <section class="nm-table-attributes">
    <h1 class="nm-table-attributes-title">属性(Attributes)</h1>
    <el-table :data="data" border style="width: 100%">
      <el-table-column prop="name" label="参数" width="200"/>
      <el-table-column prop="desc" label="说明" width="400"/>
      <el-table-column prop="type" label="类型" width="200"/>
      <el-table-column prop="opt" label="可选值"/>>
      <el-table-column label="默认值" width="200">
        <template slot-scope="{row}">{{row.def}}</template>
      </el-table-column>
    </el-table>
  </section>
</template>
<script>
export default {
  props: ['data']
}
</script>
<style lang="scss">
.nm-table-attributes {
  margin-bottom: 30px;
  &-title {
    margin: 10px 0;
    font-size: 25px;
  }
}
</style>
