<template>
  <nm-split v-model="split">
    <template v-slot:left>
      <menu-tree ref="tree" v-bind="tree" v-on="treeOn" />
    </template>
    <template v-slot:right>
      <nm-box page header title="按钮设置" icon="system">
        <template v-if="hasButtons">
          <el-checkbox :indeterminate="isIndeterminate" :disabled="disabledButton" v-model="checkAll" @change="onButtonCheckAllChange">全选</el-checkbox>
          <el-divider />
          <el-checkbox-group v-model="buttonChecked" @change="onButtonCheckedChange" :disabled="disabledButton">
            <el-checkbox v-for="button in buttons" :label="button.id" :key="button.id" border @change="onButtonChange(button.id, $event)">{{ button.name }}</el-checkbox>
          </el-checkbox-group>
        </template>
      </nm-box>
    </template>
  </nm-split>
</template>
<script>
import { mixins } from 'netmodular-ui'
import MenuTree from '../tree'
export default {
  components: { MenuTree },
  mixins: [mixins.dialog],
  data() {
    return {
      split: 0.3,
      selection: null,
      buttons: [],
      checkAll: false,
      isIndeterminate: false,
      buttonChecked: [],
      tree: {
        showCheckbox: true,
        defaultExpandAll: true
      },
      treeOn: {
        check: this.onTreeCheck,
        change: this.onTreeChange,
        'check-change': this.onTreeCheckChange
      },
      disabledButton: true
    }
  },
  props: {
    // 关联的对象id
    id: {
      type: String,
      required: true
    },
    /** 已选菜单查询方法 */
    menuQueryAction: {
      type: Function,
      required: true
    },
    // 菜单保存方法
    menuUpdateAction: {
      type: Function,
      required: true
    },
    // 按钮查询方法
    buttonQueryAction: {
      type: Function,
      required: true
    },
    // 按钮更新方法
    buttonUpdateAction: {
      type: Function,
      required: true
    }
  },
  computed: {
    // 是否显示右侧的按钮选择
    hasButtons() {
      return this.selection && this.buttons.length > 0
    }
  },
  methods: {
    queryMenus() {
      this.menuQueryAction().then(data => {
        this.$refs.tree.setCheckedKeys(data)
        this.queryButtons()
      })
    },
    // 查询按钮信息
    queryButtons() {
      this.buttons = []
      this.buttonChecked = []
      if (this.selection) {
        this.buttonQueryAction(this.selection.id).then(data => {
          this.buttons = data
          this.buttons.forEach(b => {
            if (b.checked) {
              this.buttonChecked.push(b.id)
            }
          })

          this.onButtonCheckedChange(this.buttonChecked)
        })
      }
    },
    // 菜单选择事件
    onTreeCheck(selection, checkedObject) {
      let ids = [].concat(checkedObject.checkedKeys).concat(checkedObject.halfCheckedKeys)
      this.menuUpdateAction(ids)
    },
    onTreeCheckChange(data, checked) {
      if (this.selection && data.id === this.selection.id) {
        this.selection.checked = checked
        this.disabledButton = !this.selection || !this.selection.checked
      }
    },
    // 菜单树选中事件
    onTreeChange(data, node) {
      this.selection = Object.assign({}, data.item)
      if (this.selection.type === 1) {
        this.selection.checked = node.checked
        this.disabledButton = !this.selection || !this.selection.checked
        this.queryButtons()
      } else {
        this.buttons = []
      }
    },
    // 按钮全选事件
    onButtonCheckAllChange(val) {
      this.buttonChecked = val ? this.buttons.map(b => b.id) : []
      this.isIndeterminate = false
      this.buttonUpdateAction({ menuId: this.selection.id, checked: this.checkAll })
    },
    // 按钮组改变事件
    onButtonCheckedChange(value) {
      let checkedCount = value.length
      this.checkAll = checkedCount === this.buttons.length
      this.isIndeterminate = checkedCount > 0 && checkedCount < this.buttons.length
    },
    // 按钮单个更改事件
    onButtonChange(id, checked) {
      this.buttonUpdateAction({ menuId: this.selection.id, buttonId: id, checked })
    }
  },
  watch: {
    id: {
      immediate: true,
      handler(val) {
        if (val) {
          this.queryMenus()
        }
      }
    }
  }
}
</script>
