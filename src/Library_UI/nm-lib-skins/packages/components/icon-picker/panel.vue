<template>
  <nm-dialog v-bind="dialog" v-on="dialogOn" :visible.sync="visible_">
    <template v-slot:toolbar>
      <el-input class="nm-icon-picker-panel-filter" v-model="filter" placeholder="请输入英文或中文名称" clearable></el-input>
    </template>
    <section class="nm-icon-picker-panel">
      <div v-for="icon in iconList" :key="icon.code" class="nm-icon-picker-panel-item" @click="onSelect(icon)">
        <nm-icon :name="icon.code"/>
      </div>
    </section>
  </nm-dialog>
</template>
<script>
import dialog from '../../mixins/components/dialog.js'
import { iconData } from 'nm-lib-icon'
export default {
  mixins: [dialog],
  data () {
    return {
      filter: '',
      dialog: {
        title: '选择图标',
        icon: 'list',
        height: '60%',
        width: '600px',
        fullscreen: true
      },
      dialogOn: {
        open: this.onOpen,
        opened: this.onOpened,
        close: this.onClose,
        closed: this.onClosed
      }
    }
  },
  computed: {
    iconList () {
      if (!this.filter) { return iconData }

      let list = []
      iconData.forEach(icon => {
        if (icon.name.indexOf(this.filter) > -1 || icon.code.indexOf(this.filter) > -1) { list.push(icon) }
      })
      return list
    }
  },
  methods: {
    onSelect (icon) {
      this.$emit('success', icon.code)
      this.hide()
    },
    onOpen () {
      this.$emit('open')
    },
    onOpened () {
      this.$emit('opened')
    },
    onClose () {
      this.$emit('close')
    },
    onClosed () {
      this.$emit('closed')
    }
  }
}
</script>
