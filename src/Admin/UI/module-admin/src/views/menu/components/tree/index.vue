<template>
  <nm-box page header refresh title="菜单预览" icon="menu" @refresh="refresh">
    <el-tree class="nm-tree" ref="tree" v-bind="tree" v-on="on">
      <span slot-scope="{ data }">
        <nm-icon :name="data.item.icon || 'attachment'" />
        <span class="nm-m-l-5">{{ data.label }}</span>
      </span>
    </el-tree>
  </nm-box>
</template>
<script>
const api = $api.admin.menu
export default {
  data() {
    return {
      tree: {
        data: [],
        nodeKey: 'id',
        highlightCurrent: true,
        props: { children: 'children', label: 'label' },
        currentNodeKey: '00000000-0000-0000-0000-000000000000',
        expandOnClickNode: false,
        defaultExpandedKeys: ['00000000-0000-0000-0000-000000000000']
      },
      on: {
        'current-change': this.onSelectChange,
        'node-expand': this.onNodeExpand,
        'node-collapse': this.onNodeCollapse
      },
      selection: null
    }
  },
  methods: {
    //刷新
    refresh(init) {
      api.getTree().then(data => {
        this.tree.data = [data]
        if (init) {
          //初始化触发一次change事件
          this.onSelectChange(data)
        } else {
          //刷新要保留当前点击节点
          this.$nextTick(() => {
            this.$refs.tree.setCurrentKey(this.tree.currentNodeKey)
          })
        }
      })
    },
    onSelectChange(data) {
      if (this.selection === data) return

      this.tree.currentNodeKey = data.id
      this.selection = data
      this.$emit('change', this.selection)
    },
    onNodeExpand(data) {
      //记录展开的节点
      this.tree.defaultExpandedKeys.push(data.id)
    },
    onNodeCollapse(data) {
      //移除展开的节点
      this.$_.pull(this.tree.defaultExpandedKeys, data.id)
    },
    /**插入 */
    insert(data) {
      //设置子节点
      if (!data.children) {
        data.children = []
      }

      let children = this.selection.children
      //如果不包含子节点，直接push
      if (children.length < 1) {
        children.push(data)
        return
      }
      for (let i = 0; i < children.length; i++) {
        if (data.item.sort < children[i].item.sort) {
          children.splice(i, 0, data)
          break
        }

        //如果是最后一个，则附加到最后一个节点后面
        if (i === children.length - 1) {
          children.push(data)
          break
        }
      }
    },
    /**删除 */
    remove(id) {
      let children = this.selection.children
      for (let i = 0; i < children.length; i++) {
        let child = children[i]
        if (id === child.id) {
          children.splice(i, 1)
          return child
        }
      }
    },
    /**更新 */
    update(model) {
      //先判断是否展开,已展开的先删除
      let expanded = this.$refs.tree.getNode(model.id).expanded
      if (!expanded) {
        this.$_.pull(this.tree.defaultExpandedKeys, model.id)
      }
      //保存原来的子节点，同时先删除，再添加，这样可以保证排序
      model.children = this.remove(model.id).children
      this.insert(model)
      //若是展开状态要再次展开
      if (expanded) {
        this.tree.defaultExpandedKeys.push(model.id)
      }
    },
    /**排序，重新刷新 */
    sort() {
      this.refresh()
    }
  },
  created() {
    this.refresh(true)
  }
}
</script>
