<template>
  <nm-dialog class="nm-admin-permission-bind" v-bind="dialog" :visible.sync="visible_" @open="onOpen">
    <template v-slot:toolbar>
      <nm-button icon="refresh" @click="refresh" />
    </template>
    <nm-flex fix-mode="top" fix="105px">
      <template v-slot:top>
        <nm-form v-bind="form">
          <el-form-item label="当前角色：">
            <el-input :value="roleName" disabled />
          </el-form-item>
          <el-form-item label="选择平台：">
            <el-select v-model="form.model.platform" @change="refresh">
              <el-option label="Android" :value="1"></el-option>
              <el-option label="IOS" :value="2"></el-option>
            </el-select>
          </el-form-item>
        </nm-form>
      </template>
      <template v-slot:bottom>
        <nm-scrollbar>
          <div class="nm-p-l-15 nm-p-r-15 nm-p-b-15">
            <el-tree ref="tree" class="nm-tree" v-bind="tree">
              <span slot-scope="{ data }">
                <nm-icon name="attachment" />
                <span class="nm-m-l-5"
                  >{{ data.label
                  }}<label v-if="data.item.isPermission" class="nm-p-l-5 nm-text-info nm-size-14" style="font-style: italic;"
                    >(<label>{{ data.item.code }}</label
                    >)</label
                  ></span
                >
              </span>
            </el-tree>
          </div>
        </nm-scrollbar>
      </template>
    </nm-flex>
    <template v-slot:footer>
      <nm-button type="success" text="确认" @click="onSave" />
      <nm-button type="info" text="关闭" @click="visible_ = false" />
    </template>
  </nm-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'
const api = $api.admin.permission
export default {
  mixins: [mixins.visible],
  data() {
    return {
      dialog: {
        title: '角色平台权限绑定',
        icon: 'android',
        iconColor: '#67C23A',
        width: '600px',
        height: '90%',
        noScrollbar: true,
        footer: true
      },
      tree: {
        data: [],
        nodeKey: 'id',
        highlightCurrent: true,
        props: { children: 'children', label: 'label' },
        currentNodeKey: 0,
        expandOnClickNode: true,
        defaultExpandAll: true,
        showCheckbox: true
      },
      form: {
        labelWidth: '90px',
        model: {
          platform: 1
        }
      }
    }
  },
  props: {
    roleId: String,
    roleName: String,
    //获取绑定关系的方法
    getAction: {
      type: Function,
      required: true
    },
    //绑定方法
    bindAction: {
      type: Function,
      required: true
    }
  },
  methods: {
    //刷新
    refresh() {
      this._openLoading()
      api
        .tree()
        .then(data => {
          this.tree.data = [data]
          this.initialized = true
          //查询已有绑定
          this.getAction({ roleId: this.roleId, platform: this.form.model.platform }).then(list => {
            let selection = []
            this.tree.data.forEach(m => {
              m.children.forEach(n => {
                n.children.forEach(o => {
                  o.children.forEach(i => {
                    const p = list.find(t => t.permissionCode === i.item.code)
                    if (p) {
                      selection.push(i.id)
                    }
                  })
                })
              })
            })

            this.$refs.tree.setCheckedKeys(selection)
            this._closeLoading()
          })
        })
        .catch(() => {
          this._closeLoading()
        })
    },
    onSave() {
      let permissions = this.$refs.tree.getCheckedNodes(true).map(m => {
        return m.item.code
      })
      this._openLoading()
      this.bindAction({ roleId: this.roleId, platform: this.form.model.platform, permissions })
        .then(() => {
          this._success('保存成功')
          this._closeLoading()
        })
        .catch(() => {
          this._error('保存失败')
          this._closeLoading()
        })
    },
    onOpen() {
      this.refresh()
    }
  }
}
</script>
<style lang="scss">
.nm-admin-permission-bind {
  .nm-scrollbar {
    padding: 10px 0;
    border-top: 1px solid #e4e7ed;
  }
  .nm-dialog-footer {
    border-top: 1px solid #e4e7ed;
  }
}
</style>
