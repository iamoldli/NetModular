<template>
  <nm-box page header refresh title="角色列表" icon="role" @refresh="refresh">
    <template v-slot:toolbar>
      <nm-button :code="buttons.add.code" type="text" icon="add" @click="add" />
      <nm-button :code="buttons.edit.code" type="text" icon="edit" @click="edit" :disabled="canEdit" />
    </template>
    <el-tree ref="tree" v-bind="tree" v-on="on">
      <span style="width:100%;height:32px;line-height:32px;" slot-scope="{ data }">
        <el-tooltip v-if="data.isSpecified" effect="dark" content="指定角色，在其他业务模块中硬编码的角色，不允许编辑删除" placement="top">
          <nm-icon class="nm-size-20 nm-text-warning" name="star-fill" />
        </el-tooltip>
        <nm-icon class="nm-size-20" v-else name="role" />
        <span class="nm-m-l-5" :title="data.remarks">{{ data.label }}</span>
        <nm-button v-if="!data.isSpecified" :code="buttons.del.code" type="text" icon="delete" class="nm-pull-right nm-text-danger" @click.native.stop="del(data)" />
      </span>
    </el-tree>

    <!--保存页-->
    <save-page :id="curr.id" :visible.sync="dialog.save" @success="refresh" />
  </nm-box>
</template>
<script>
import page from '../../index/page'
import SavePage from '../save'
const api = $api.admin.role
export default {
  components: { SavePage },
  data() {
    return {
      tree: {
        data: [],
        nodeKey: 'id',
        highlightCurrent: true
      },
      on: {
        'current-change': this.onTreeChange
      },
      selection: null,
      curr: { id: '' },
      dialog: {
        save: false
      },
      buttons: page.buttons
    }
  },
  computed: {
    canEdit() {
      return !this.selection || this.selection.isSpecified
    }
  },
  methods: {
    //刷新
    refresh() {
      api.query().then(data => {
        let treeData = data.rows.map(m => {
          return {
            id: m.id,
            label: m.name,
            remarks: m.remarks,
            isSpecified: m.isSpecified
          }
        })
        this.tree.data = treeData

        //选中第一个
        if (treeData.length > 0) {
          this.selection = treeData[0]
          this.$nextTick(() => {
            this.$refs.tree.setCurrentKey(this.selection.id)
            this.$emit('change', this.selection)
          })
        }
      })
    },
    add() {
      this.curr.id = ''
      this.dialog.save = true
    },
    edit() {
      if (!this.selection) {
        this._warning('请选择要修改的角色')
        return
      }
      this.curr.id = this.selection.id
      this.dialog.save = true
    },
    del(role) {
      this._delete(async () => {
        this._openLoading('正在删除，请稍后...')
        api
          .remove(role.id)
          .then(() => {
            this._closeLoading()
            this._success('删除成功~')
            this.refresh()
          })
          .catch(() => {
            this._closeLoading()
          })
      }, this.msg).catch(() => {})
    },
    onTreeChange(data) {
      if (this.selection === data) return
      this.selection = data
      this.$emit('change', this.selection)
    }
  },
  created() {
    this.refresh()
  }
}
</script>
