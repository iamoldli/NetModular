<template>
  <nm-details-dialog ref="details" title="菜单详情" icon="detail" width="50%" show-fullscreen :action="get" :visible.sync="visible_">
    <template slot-scope="{model}">
      <nm-details-row :cols="2">
        <nm-details-cell label="父节点：">{{model.parentName}}</nm-details-cell>
        <nm-details-cell label="类型：">
          <el-radio>{{model.typeName}}</el-radio>
        </nm-details-cell>
      </nm-details-row>
      <nm-details-row :cols="2">
        <nm-details-cell label="名称：">{{model.name}}</nm-details-cell>
        <nm-details-cell label="是否显示：">
          <el-switch disabled :value="model.show"/>
        </nm-details-cell>
      </nm-details-row>
      <nm-details-row :cols="2">
        <nm-details-cell label="图标：">
          <nm-icon v-if="model.icon" :name="model.icon"/>
        </nm-details-cell>
        <nm-details-cell label="图标颜色：">
          <el-color-picker :value="model.iconColor" size="mini"/>
        </nm-details-cell>
      </nm-details-row>
      <nm-details-row :cols="2">
        <nm-details-cell label="路由名称：">{{model.routerName}}</nm-details-cell>
        <nm-details-cell label="链接：">{{model.url}}</nm-details-cell>
      </nm-details-row>
      <nm-details-row :cols="model.type===1?2:1">
        <nm-details-cell label="排序：">{{model.sort}}</nm-details-cell>
        <nm-details-cell label="打开方式：" v-show="model.type===1">
          <target-select disabled :value="model.target"/>
        </nm-details-cell>
      </nm-details-row>
      <nm-details-row>
        <nm-details-cell label="备注：">{{model.sort}}</nm-details-cell>
      </nm-details-row>
    </template>
  </nm-details-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import api from '../../../api/menu'
import TargetSelect from '../target-select'
export default {
  components: { TargetSelect },
  mixins: [mixins.dialog],
  props: ['id'],
  methods: {
    get () {
      if (this.id === '') {
        this._warning('请选择要查看的数据~')
        return
      }
      return api.details(this.id)
    }
  }
}
</script>
