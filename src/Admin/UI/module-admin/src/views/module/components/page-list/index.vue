<template>
  <nm-dialog v-bind="dialog" :visible.sync="visible_">
    <template v-slot:title> 页面列表({{ module.name }}) </template>
    <div class="page-list">
      <section class="left">
        <el-tabs ref="pageTab" v-model="pageTab" tab-position="left" style="height: 100%;">
          <el-tab-pane v-for="page in pages" :key="page.code" :name="page.code" :label="page.name">
            <span slot="label"><nm-icon :name="page.icon" /> {{ page.name }}</span>
          </el-tab-pane>
        </el-tabs>
      </section>
      <section class="right">
        <nm-tabs>
          <el-tabs type="card">
            <el-tab-pane>
              <span slot="label"><i class="el-icon-date"></i> 详情</span>
              <nm-scrollbar style="height:100%">
                <el-divider content-position="left">基本信息</el-divider>
                <el-form>
                  <el-row>
                    <el-col :span="10" :offset="1">
                      <el-form-item label="名称：">
                        <span>{{ page.name }} </span>
                      </el-form-item>
                    </el-col>
                    <el-col :span="10">
                      <el-form-item label="图标：">
                        <nm-icon class="nm-size-25" :name="page.icon" />
                      </el-form-item>
                    </el-col>
                    <el-col :span="10" :offset="1">
                      <el-form-item label="路由名称：">
                        <span>{{ page.code }} </span>
                      </el-form-item>
                    </el-col>
                    <el-col :span="10">
                      <el-form-item label="路由路径：">
                        <span>{{ page.path }} </span>
                      </el-form-item>
                    </el-col>
                  </el-row>
                </el-form>
                <el-divider content-position="left">关联权限</el-divider>
                <el-table :data="pagePermissions" border stripe size="mini" style="width: 100%">
                  <el-table-column prop="name" label="名称" width="180" align="center"> </el-table-column>
                  <el-table-column prop="controller" label="控制器" width="120" align="center"> </el-table-column>
                  <el-table-column prop="action" label="方法" align="center"> </el-table-column>
                  <el-table-column prop="httpMethodName" label="请求方式" width="70" align="center"> </el-table-column>
                  <el-table-column prop="code" label="编码" align="center" show-overflow-tooltip> </el-table-column>
                </el-table>
              </nm-scrollbar>
            </el-tab-pane>
            <el-tab-pane>
              <span slot="label"><i class="el-icon-date"></i> 按钮</span>
              <nm-box page>
                <el-table :data="buttons" border stripe size="mini" style="width: 100%;">
                  <el-table-column prop="text" label="名称" align="center" width="100"> </el-table-column>
                  <el-table-column prop="icon" label="图标" align="center" width="100">
                    <template v-slot="scope">
                      <nm-icon v-if="scope.row.icon" :name="scope.row.icon" class="nm-size-18" />
                    </template>
                  </el-table-column>
                  <el-table-column prop="code" label="编码" align="center"> </el-table-column>
                  <el-table-column prop="permissions" label="关联权限" align="center">
                    <template v-slot="scope">
                      <p v-for="p in scope.row.permissions" :key="p">{{ p }}</p>
                    </template>
                  </el-table-column>
                </el-table>
              </nm-box>
            </el-tab-pane>
          </el-tabs>
        </nm-tabs>
      </section>
    </div>
  </nm-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'

const { queryByCodes } = $api.admin.permission

export default {
  mixins: [mixins.visible],
  data() {
    return {
      dialog: {
        icon: 'permission',
        width: '900px',
        height: '80%',
        noScrollbar: true
      },
      pageTab: '',
      page: {
        name: '',
        code: '',
        icon: '',
        path: '',
        permissions: [],
        buttons: []
      },
      pagePermissions: [],
      buttons: []
    }
  },
  props: {
    module: Object
  },
  computed: {
    pages() {
      let pages = []
      if (!this.module || !this.module.code) {
        return pages
      }
      this.$router.options.routes.forEach(r => {
        if (r && r.name && r.name.toLowerCase().startsWith(this.module.code.toLowerCase())) {
          pages.push({
            name: r.meta.title,
            icon: r.meta.icon || '',
            code: r.name.toLowerCase(),
            path: r.path,
            permissions: r.meta.permissions,
            buttons: r.meta.buttons
          })
        }
      })

      return pages
    }
  },
  watch: {
    pages(val) {
      if (val.length > 0) {
        this.pageTab = val[0].code
      } else {
        this.pageTab = ''
      }
    },
    pageTab(val) {
      this.pagePermissions = []
      this.buttons = []
      if (val) this.page = this.pages.find(m => m.code === val)
      if (!this.page) {
        this.page = {
          name: '',
          code: '',
          icon: '',
          permissions: [],
          buttons: []
        }
      } else {
        queryByCodes(this.page.permissions).then(data => {
          if (data) {
            this.pagePermissions = data
          }
        })

        if (this.page.buttons) {
          for (let key in this.page.buttons) {
            this.buttons.push(this.page.buttons[key])
          }
        }
      }
    }
  }
}
</script>
<style lang="scss" scoped>
.page-list {
  display: flex;
  flex-direction: row;
  align-items: stretch;
  height: 100%;

  .left {
    flex-shrink: 0;
    ::v-deep .el-tabs__header {
      margin-right: 0;
    }
  }
  .right {
    flex-grow: 1;

    ::v-deep .el-tab-pane {
      box-sizing: border-box;

      .el-scrollbar__view {
        padding: 10px 20px;
      }
    }
  }
}
.left-pane {
  .el-tabs__content {
    display: none;
  }
}
</style>
