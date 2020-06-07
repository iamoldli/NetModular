<template>
  <nm-box page :header="false" footer :loading="loading">
    <el-alert title="提示" type="warning" class="nm-m-b-10" description="路由菜单基于页面配置，同一个页面可以配置任意个菜单" show-icon> </el-alert>
    <el-tree ref="tree" class="nm-admin-role-menus-tree" v-bind="menuTree">
      <div class="nm-admin-role-menus-tree-node" slot-scope="{ data }">
        <span class="nm-admin-role-menus-tree-node-label"><nm-icon :name="data.item.icon || 'attachment'" /> {{ data.label }}</span>
      </div>
    </el-tree>

    <template v-slot:footer>
      <nm-button type="success" text="保存" @click="save" />
      <nm-button type="info" text="重置" @click="reset" />
    </template>
  </nm-box>
</template>
<script>
const menuApi = $api.admin.menu
const { queryBindMenus, bindMenus } = $api.admin.role
export default {
  data() {
    return {
      loading: false,
      menuTree: {
        data: [],
        nodeKey: 'id',
        'expand-on-click-node': false,
        'default-expand-all': true,
        'show-checkbox': true
      },
      menus: []
    }
  },
  props: {
    //角色ID
    id: String
  },
  methods: {
    queryMenuTree() {
      this.loading = true
      menuApi.getTree().then(data => {
        this.menuTree.data = [data]
        this.loading = false
      })
    },
    queryBindMenus() {
      this.menus = []
      if (this.id) {
        this.loading = true
        queryBindMenus(this.id).then(data => {
          this.menus = data
          this.reset()
          this.loading = false
        })
      }
    },
    save() {
      let model = {
        roleId: this.id,
        menus: this.$refs.tree.getCheckedKeys().concat(this.$refs.tree.getHalfCheckedKeys())
      }
      this.loading = true
      bindMenus(model).then(() => {
        this._success('保存成功')
        this.menus = model.menus
        this.loading = false
      })
    },
    reset() {
      this.$nextTick(() => {
        this.$refs.tree.setCheckedKeys(this.menus)
      })
    }
  },
  created() {
    this.queryMenuTree()
    this.queryBindMenus()
  },
  watch: {
    id() {
      //延迟加载，防止无法渲染已选择的数据
      setTimeout(this.queryBindMenus, 100)
    }
  }
}
</script>
