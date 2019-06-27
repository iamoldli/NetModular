<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="公司单位：">
          <el-input v-model="companyName" disabled/>
        </el-form-item>
        <el-form-item label="节点路径：">
          <el-input v-model="fullPath" disabled/>
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" autofocus clearable/>
        </el-form-item>
        <el-form-item label="负责人：" prop="leaderName">
          <el-input v-model="form.model.leaderName" clearable/>
        </el-form-item>
        <el-form-item label="排序：" prop="sort">
          <el-input v-model="form.model.sort" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/Department'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data() {
    return {
      api,
      form: {
        title: '编辑部门',
        action: api.update,
        model: {
          /** 名称 */
          name: '',
          /** 负责人 */
          leader: '',
          leaderName: '',
          /** 排序 */
          sort: ''
        },
        rules: {
          name: [{ required: true, message: '请输入名称' }],
          sort: [{ required: true, message: '请输入序号' }]
        }
      }
    }
  },
  props: {
    companyName: {
      type: String
    },
    fullPath: {
      type: String
    }
  }
}
</script>
