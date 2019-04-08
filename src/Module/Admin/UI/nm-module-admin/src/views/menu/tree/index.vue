<template>
  <el-tree
    ref="tree"
    class="nm-admin-menu-tree"
    node-key="id"
    default-expand-all
    highlight-current
    :data="tree"
    :show-checkbox="showCheckbox"
    :current-node-key="currentNodeKey"
    :expand-on-click-node="false"
    @current-change="onSelectChange"
    @check="onCheck"
  >
    <div class="nm-menu-tree-node" slot-scope="{ node, data }">
      <div class="nm-menu-tree-node-label">
        <nm-icon v-if="showIcon" :name="data.menu.icon"/>
        {{ node.label }}
      </div>
    </div>
  </el-tree>
</template>
<script>
import { mapState } from 'vuex'
import api from '../../../api/menu'

export default {
  data () {
    return {
      // 菜单树
      tree: [],
      /** 菜单列表 */
      menus: [],
      // 当前点击的菜单信息
      current: {
        id: '',
        checked: false,
        menu: null,
        node: null
      }
    }
  },
  props: {
    value: String,
    /** 是否显示多选框 */
    showCheckbox: Boolean,
    /** 是否显示图标 */
    showIcon: {
      type: Boolean,
      default: true
    },
    /** 默认选择节点 */
    checkedKeys: Array,
    /** 当前选择节点 */
    currentNodeKey: {
      type: String,
      default: ''
    }
  },
  computed: {
    ...mapState('app/system', { systemTitle: 'title' })
  },
  methods: {
    // 刷新菜单树
    refresh () {
      // 树根节点
      const root = { id: '', label: this.systemTitle, children: [], menu: { id: '', name: this.systemTitle, icon: 'system' } }

      api.getTree().then(data => {
        root.children = this.menus2Tree(data)
        this.tree = [root]
        this.current.id = ''
        this.setCheckedKeys()
        this.$nextTick(() => {
          const rootNode = this.$refs.tree.getNode('')
          this.onSelectChange(rootNode.data, rootNode)
        })
      })
    },
    // 菜单转换为树形结构
    menus2Tree (menus) {
      const nodes = []
      if (menus.length > 0) {
        menus.map(item => {
          item.icon = item.icon || 'list'

          var node = {
            id: item.id,
            label: item.name,
            menu: item
          }
          this.menus.push(item)

          if (item.type === 0) {
            node.children = this.menus2Tree(item.children)
          }
          nodes.push(node)
        })
      }
      return nodes
    },
    // 设置选中的节点
    setCheckedKeys () {
      if (this.checkedKeys) {
        this.$nextTick(() => {
          console.log(this.checkedKeys)
          this.$refs.tree.setCheckedKeys(this.checkedKeys)
          this.$refs.tree.setCurrentKey('')
        })
      }
    },
    // 节点选中事件
    onSelectChange (data, node) {
      if (!this.current.id || data.menu.id !== this.current.id) {
        this.current.id = data.menu.id
        this.current.checked = node.checked
        this.current.menu = data.menu
        this.current.node = node

        this.$emit('input', this.current.id)
        this.$emit('select-change', this.current)
      }
    },
    // 节点checkbox选择事件
    onCheck (data, options) {
      let selection = []
      options.checkedNodes.map(m => {
        if (m.menu.id) { selection.push(m.menu) }
      })
      options.halfCheckedNodes.map(m => {
        if (m.menu.id) { selection.push(m.menu) }
      })
      this.$emit('check', selection)
    }
  }
}
</script>
<style lang="scss">
.nm-admin-menu-tree {
  &-node {
    position: relative;
    display: flex;
    flex-direction: row;
    align-items: stretch;
    width: 100%;
    &-label {
      flex-grow: 1;
      font-size: 14px;
      height: 28px;
      line-height: 28px;
    }
    &-button {
      flex-shrink: 1;
      width: 100px;
    }
  }
}
</style>
