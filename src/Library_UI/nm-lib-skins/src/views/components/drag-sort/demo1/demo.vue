<template>
  <nm-box page header title="拖拽排序示例" icon="sort">
    <nm-box-row height="400px">
      <nm-box-col :span="12">
        <nm-drag-sort style="width:500px" v-model="list"/>
      </nm-box-col>
      <nm-box-col :span="12">
        <textarea rows="30" cols="100" :value="listJson"></textarea>
      </nm-box-col>
    </nm-box-row>
  </nm-box>
</template>
<script>
export default {
  data() {
    let list = []
    for (var i = 1; i < 10; i++) {
      list.push({
        id: i,
        label: `第${i}个`,
        sort: i
      })
    }
    return {
      list
    }
  },
  computed: {
    listJson() {
      return JSON.stringify(this.list)
    }
  },
  watch: {
    list() {
      console.log(this.list)
    }
  }
}
</script>
