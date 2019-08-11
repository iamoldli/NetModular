<template>
  <el-cascader :props="props"></el-cascader>
</template>

<script>
import api from '../../../../api/Area'
export default {
  data() {
    return {
      props: {
        lazy: true,
        lazyLoad(node, resolve) {

        }
      }
    }
  }
}
</script>
