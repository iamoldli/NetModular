<template>
  <div>
    <el-input v-model="path_" @click.native="dialog.visible=true" readonly/>
    <nm-dialog v-bind="dialog" :visible.sync="dialog.visible">
      <tree ref="tree" v-model="id" @change="onTreeChange"/>
      <template v-slot:footer>
        <nm-button type="success" text="确定" @click="onSuccess"/>
        <nm-button type="info" text="取消" @click="dialog.visible = false"/>
      </template>
    </nm-dialog>
  </div>
</template>
<script>
import Tree from '../tree'
export default {
  components: { Tree },
  data() {
    return {
      id: this.value,
      path_: this.path,
      temp: {},
      dialog: {
        visible: false,
        title: '选择部门',
        icon: 'department',
        width: '400px',
        height: '80%',
        footer: true
      }
    }
  },
  props: {
    value: {
      type: String
    },
    path: {
      type: String
    }
  },
  methods: {
    onTreeChange(selection) {
      this.temp = selection
    },
    onSuccess() {
      this.id = this.temp.id
      this.path_ = this.temp.id ? this.temp.fullPath : ''
      this.dialog.visible = false
      this.$emit('input', this.id)
    },
    reset() {
      if (this.$refs.tree) {
        this.id = ''
        this.path_ = ''
        this.$refs.tree.reset()
      }
    }
  },
  watch: {
    path() {
      this.path_ = this.path
    }
  }
}
</script>
