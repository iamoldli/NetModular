<template>
  <nm-dialog class="nm-user-picture-show" v-bind="dialog" :visible.sync="visible_">
    <nm-attachment-img :id="user.picture"/>
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  props: {
    user: {
      type: Object
    }
  },
  computed: {
    dialog() {
      return {
        title: `${this.user.name}的照片`,
        icon: 'photo',
        width: '400px',
        closeOnClickModal: true
      }
    }
  }
}
</script>
<style lang="scss">
.nm-user-picture-show {
  .el-scrollbar__view {
    padding: 20px 0;
    text-align: center;
    img {
      max-width: 300px;
    }
  }
}
</style>
