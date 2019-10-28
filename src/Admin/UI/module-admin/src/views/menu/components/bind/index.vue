<template>
  <nm-split v-model="split">
    <template v-slot:left>
      <nm-box page header title="菜单树" type="success" icon="menu" :toolbar="null">
        <menu-tree ref="tree" v-model="menuId" v-bind="menuTree" v-on="treeOn"/>
      </nm-box>
    </template>
    <template v-slot:right>
      <nm-box page header title="按钮设置" icon="system">
        <template v-if="showButtonBox">
          <el-checkbox :indeterminate="isIndeterminate" :disabled="!isMenuChecked" v-model="checkAll" @change="onButtonCheckAllChange">全选</el-checkbox>
          <el-divider/>
          <el-checkbox-group v-model="buttonChecked" @change="onButtonCheckedChange" :disabled="!isMenuChecked">
            <el-checkbox v-for="button in buttons" :label="button.id" :key="button.id" border @change="onButtonChange(button.id,$event)">{{button.name}}</el-checkbox>
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
  data () {
    return {
      split: 0.3,
      // 当前选中的菜单编号
      menuId: '',
      // 当前选中的菜单信息
      currentMenu: {},
      // 默认选择的菜单节点
      checkedKeys: [],
      buttons: [],
      checkAll: false,
      isIndeterminate: false,
      buttonChecked: [],
      treeOn: {
        check: this.onTreeCheck,
        'select-change': this.onTreeSelectChange,
        'check-change': this.onTreeCheckChange
      }
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
    menuTree () {
      return {
        'show-checkbox': true,
        'checked-keys': this.checkedKeys
      }
    },
    // 是否显示右侧的按钮选择
    showButtonBox () {
      return this.menuId && this.buttons.length > 0
    },
    // 判断选中的菜单是否被选择
    isMenuChecked () {
      return this.menuId && this.currentMenu && this.currentMenu.checked
    }
  },
  methods: {
    queryMenus () {
      this.menuQueryAction().then(data => {
        this.checkedKeys = data
        this.queryButtons()
      })
    },
    // 查询按钮信息
    queryButtons () {
      this.buttons = []
      this.buttonChecked = []
      if (this.menuId) {
        this.buttonQueryAction(this.menuId).then(data => {
          this.buttons = data
          this.buttons.map(b => {
            if (b.checked) {
              this.buttonChecked.push(b.id)
            }
          })
          this.onButtonCheckedChange(this.buttonChecked)
        })
      }
    },
    // 菜单选择事件
    onTreeCheck (selection) {
      this.menuUpdateAction(selection.map(m => m.id))
    },
    onTreeCheckChange (data, checked) {
      if (data.menu.id === this.menuId) {
        this.currentMenu.checked = checked
      }
    },
    // 菜单树选中事件
    onTreeSelectChange (menu) {
      this.currentMenu = menu
      this.queryButtons()
    },
    // 按钮全选事件
    onButtonCheckAllChange (val) {
      this.buttonChecked = val ? this.buttons.map(b => b.id) : []
      this.isIndeterminate = false
      this.buttonUpdateAction({ menuId: this.currentMenu.id, checked: this.checkAll })
    },
    // 按钮组改变事件
    onButtonCheckedChange (value) {
      let checkedCount = value.length
      this.checkAll = checkedCount === this.buttons.length
      this.isIndeterminate = checkedCount > 0 && checkedCount < this.buttons.length
    },
    // 按钮单个更改事件
    onButtonChange (id, checked) {
      this.buttonUpdateAction({ menuId: this.currentMenu.id, buttonId: id, checked })
    }
  },
  created () {
    if (this.id) {
      this.queryMenus()
    }
  },
  watch: {
    id () {
      if (this.id) {
        this.queryMenus()
      }
    }
  }
}
</script>
