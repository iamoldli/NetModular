<template>
  <nm-box page :header="false" footer :loading="loading">
    <el-alert
      title="提示"
      type="warning"
      class="nm-m-b-10"
      description="Web端授权是通过绑定页面以及按钮实现的，页面以及按钮都会关联所用到接口权限，因此只要绑定了页面以及按钮就能拥有所关联的接口权限"
      show-icon
    >
    </el-alert>

    <el-tree ref="tree" class="nm-admin-role-menus-tree" v-bind="tree" @check="onTreeCheck">
      <div class="nm-admin-role-menus-tree-node" slot-scope="{ data }">
        <span class="nm-admin-role-menus-tree-node-label">{{ data.label }}</span>
        <div class="nm-admin-role-menus-tree-node-buttons">
          <el-checkbox v-for="btn in data.buttons" :key="btn.code" v-model="btn.checked" :label="btn.text" :disabled="!data.checked"></el-checkbox>
        </div>
      </div>
    </el-tree>

    <template v-slot:footer>
      <nm-button type="success" text="保存" @click="save" />
      <nm-button type="info" text="重置" @click="resetTree" />
    </template>
  </nm-box>
</template>
<script>
import { mapState } from 'vuex'
const moduleApi = $api.admin.module
const { queryBindPages, bindPages } = $api.admin.role
export default {
  data() {
    return {
      loading: false,
      tree: {
        data: [],
        nodeKey: 'code',
        'expand-on-click-node': false,
        'default-expand-all': true,
        'show-checkbox': true
      },
      bindPages: [],
      pages: []
    }
  },
  props: {
    //角色ID
    id: String
  },
  computed: {
    ...mapState('app/page', { allPages: 'pages' })
  },
  methods: {
    resolveTreeData() {
      this.loading = true
      //获取模块列表
      moduleApi.select().then(data => {
        data.forEach(m => {
          let node = {
            id: m.value.toLowerCase(),
            label: m.label,
            checked: false,
            children: this.allPages
              .filter(x => x.moduleCode.toLowerCase() === m.value.toLowerCase())
              .map(y => {
                let buttons = []
                for (let key in y.buttons) {
                  let btn = y.buttons[key]
                  buttons.push({
                    text: btn.text,
                    code: btn.code,
                    permissions: btn.permissions,
                    checked: false
                  })
                }
                let page = {
                  code: y.code,
                  label: y.name,
                  buttons: buttons,
                  permissions: y.permissions,
                  checked: false
                }

                this.pages.push(page)
                return page
              })
          }
          this.tree.data.push(node)
        })
        this.loading = false

        this.queryBindPages()
      })
    },
    queryBindPages() {
      this.bindPages = []
      if (this.id) {
        this.loading = true
        queryBindPages(this.id).then(data => {
          this.bindPages = data.pages
          this.resetTree()
          this.loading = false
        })
      }
    },
    save() {
      let model = {
        roleId: this.id,
        pages: this.pages
          .filter(m => m.checked)
          .map(m => {
            let buttons = m.buttons.filter(n => n.checked)
            let page = {
              code: m.code,
              buttons: buttons.map(x => x.code),
              permissions: m.permissions
            }

            if (page.buttons.length > 0) {
              buttons.forEach(x => {
                x.permissions.forEach(y => {
                  page.permissions.push(y)
                })
              })
            }

            return page
          })
      }
      this.loading = true
      bindPages(model).then(() => {
        this._success('保存成功')
        this.loading = false
      })
    },
    /**重置树 */
    resetTree() {
      let checkedKeys = []

      //已绑定的菜单
      this.pages.forEach(m => {
        let n = this.bindPages.find(x => x.code === m.code)
        if (n) {
          checkedKeys.push(m.code)
          m.checked = true
          if (m.buttons && m.buttons.length > 0) {
            m.buttons.forEach(x => {
              x.checked = n.buttons.includes(x.code)
            })
          }
        } else {
          m.checked = false
          if (m.buttons && m.buttons.length > 0) {
            m.buttons.forEach(x => {
              x.checked = false
            })
          }
        }
      })
      this.$nextTick(() => {
        this.$refs.tree.setCheckedKeys(checkedKeys)
      })
    },
    onTreeCheck(data, nodes) {
      this.pages.forEach(m => {
        m.checked = nodes.checkedNodes.findIndex(n => n.code === m.code) > -1
        if (!m.checked && m.buttons && m.buttons.length > 0) {
          m.buttons.forEach(m => {
            m.checked = false
          })
        }
      })
    }
  },
  created() {
    this.resolveTreeData()
  },
  watch: {
    id() {
      //延迟加载，防止无法渲染已选择的数据
      setTimeout(this.queryBindPages, 100)
    }
  }
}
</script>
