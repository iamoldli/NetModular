<template>
  <nm-container>
    <nm-list ref="list" v-bind="list">
      <!--按钮-->
      <template v-slot:toolbar>
        <nm-button :text="buttons.sync.text" :icon="buttons.sync.icon" @click="sync" v-nm-has="buttons.sync"/>
      </template>

      <!--是否显示-->
      <template v-slot:col-isShow="{row}">{{row.isShow?'是':'否'}}</template>

      <!--操作列-->
      <template v-slot:col-operation="{row}">
        <nm-button-delete :action="removeAction" :id="row.id" @success="refresh" v-nm-has="buttons.del"/>
      </template>
    </nm-list>
  </nm-container>
</template>
<script>
import api from '../../../api/moduleInfo.js'
import page from './page'
import cols from './cols'

export default {
  name: page.name,
  data () {
    return {
      list: {
        title: page.title,
        cols,
        action: api.query,
        search: {
          advanced: {
            enabled: false
          }
        },
        conditions: {
          name: ''
        }
      },
      removeAction: api.remove,
      buttons: page.buttons
    }
  },
  methods: {
    refresh () {
      this.$refs.list.refresh()
    },
    sync () {
      this._confirm('您确认要同步模块信息吗', '同步模块信息').then(() => {
        api.sync().then(data => {
          this._success('同步成功')
          this.refresh()
        })
      })
    }
  }
}
</script>
