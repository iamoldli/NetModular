<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--查询条件-->
      <template v-slot:querybar>
        <el-form-item label="选择模块：" prop="moduleCode">
          <nm-module-select v-model="list.model.moduleCode" checked-first clearable @change="onModuleChange" />
        </el-form-item>
      </template>

      <!--操作列-->
      <template v-slot:col-operation="{ row }">
        <nm-button v-bind="buttons.clear" @click="clear(row)" />
      </template>
    </nm-list>
  </nm-container>
</template>
<script>
import { mixins } from 'netmodular-ui'
import page from './page'
import cols from './cols'

// 接口
const api = $api.admin.cache

export default {
  name: page.name,
  mixins: [mixins.list],
  data() {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        queryOnCreated: false,
        model: {
          moduleCode: ''
        }
      },
      buttons: page.buttons
    }
  },
  methods: {
    clear(row) {
      this._delete(async () => {
        this._openLoading('正在清除缓存，请稍后...')
        api
          .clear(row.name)
          .then(() => {
            this._closeLoading()
            this._success('清除成功~')
          })
          .catch(() => {
            this._closeLoading()
            this._error('清除失败~')
          })
      }, `您确定要清除缓存《${row.desc}》吗？`).catch(() => {})
    },
    onModuleChange() {
      this.$refs.list.refresh()
    }
  }
}
</script>
