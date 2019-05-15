<template>
  <section class="nm-tmpl-box">
    <h1 class="nm-tmpl-box-title">属性(Attributes)</h1>
    <el-table :data="data" border style="width: 100%">
      <el-table-column prop="name" label="参数"/>
      <el-table-column prop="desc" label="说明"/>
      <el-table-column prop="type" label="类型"/>
      <el-table-column prop="opt" label="可选值"/>
      <el-table-column label="默认值">
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
