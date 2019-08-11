<template>
  <div class="nm-department-select">
    <div class="nm-department-select-top">
      <el-form class="nm-pf-dt-company-select" label-width="70px">
        <el-form-item label="公司单位">
          <company-select v-model="company.id" checked-first @change="onCompanyChange"/>
        </el-form-item>
      </el-form>
    </div>
    <div class="nm-department-select-bottom">
      <el-tree ref="tree" v-bind="tree" :current-node-key="selection.id" @current-change="onSelectChange"/>
    </div>
  </div>
</template>
<script>
import api from '../../../../api/Department'
import CompanySelect from '../../../Company/components/select'
export default {
  components: { CompanySelect },
  data() {
    return {
      selection: {
        id: this.value,
        name: '',
        fullPath: '',
        data: null
      },
      company: {
        id: '',
        name: ''
      },
      tree: {
        data: [],
        nodeKey: 'id',
        'highlight-current': true,
        'show-checkbox': false,
        'expand-on-click-node': false,
        'default-expand-all': true
      }
    }
  },
  props: {
    value: {
      type: String
    }
  },
  methods: {
    refresh() {
      if (!this.company.id) {
        this.tree.data = []
        return
      }
      let root = { id: '', label: this.company.name, children: [] }
      api.getTree(this.company.id).then(data => {
        data.map(item => {
          root.children.push(this.model2Tree(item))
        })

        this.tree.data = [root]

        this.$nextTick(() => {
          this.$refs.tree.setCurrentKey(this.selection.id)
        })
      })
    },
    model2Tree(model) {
      let node = {
        id: model.id,
        label: model.name,
        sort: model.sort,
        children: []
      }
      if (model.children) {
        model.children.map(item => [
          node.children.push(this.model2Tree(item))
        ])
      }
      return node
    },
    onCompanyChange(val, selection) {
      this.company.name = selection ? selection.label : ''
      this.selection.id = ''
      this.selection.name = ''
      this.selection.fullPath = ''
      this.refresh()
      this.$emit('input', this.selection.id)
      this.$emit('change', this.selection, this.company)
    },
    onSelectChange(data, node) {
      this.selection.id = data.id
      this.selection.name = data.name
      this.selection.fullPath = this.getFullPath(node)
      this.selection.data = data.data
      this.$emit('input', this.selection.id)
      this.$emit('change', this.selection, this.company)
    },
    /**
     * 获取节点的完整路径
     */
    getFullPath(node) {
      if (node.parent === null || node.data.id === 0) { return '' }
      const parentPath = this.getFullPath(node.parent)
      if (parentPath === '') { return node.data.label } else { return parentPath + ' / ' + node.data.label }
    },
    remove(id) {
      this.$refs.tree.remove(id)
    },
    reset() {
      this.company.id = ''
      this.company.name = ''
    }
  }
}
</script>
<style lang="scss">
.nm-department-select {
  &-top {
    padding: 10px;
    border-bottom: 1px solid #eee;

    .el-form-item {
      margin: 0;
    }
  }
}
</style>
