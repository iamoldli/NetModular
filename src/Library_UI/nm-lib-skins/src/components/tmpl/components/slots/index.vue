<template>
  <section class="nm-tmpl-box">
    <h1 class="nm-tmpl-box-title">插槽(Slot)</h1>
    <el-table :data="data" border style="width: 100%">
      <el-table-column prop="name" label="名称"/>
      <el-table-column prop="desc" label="说明"/>
    </el-table>
  </section>
</template>
<script>
export default {
  props: ['data']
}
</script>
