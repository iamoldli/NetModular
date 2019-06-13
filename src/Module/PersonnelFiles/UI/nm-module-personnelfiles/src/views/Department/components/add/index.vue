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
  mixins: [mixins.dialog],
  data() {
    return {
      form: {
        title: '添加部门',
        icon: 'add',
        action: api.add,
        model: {
          /** 公司编号 */
          companyId: '',
          /** 父节点 */
          parentId: '',
          /** 名称 */
          name: '',
          /** 负责人编号 */
          leader: '',
          leaderName: '',
          /** 排序 */
          sort: 0
        },
        rules: {
          name: [{ required: true, message: '请输入名称' }],
          sort: [{ required: true, message: '请输入序号' }]
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  props: {
    companyId: {
      type: String,
      required: true
    },
    companyName: {
      type: String
    },
    parentId: {
      type: String
    },
    fullPath: {
      type: String
    },
    total: {
      type: Number
    }
  },
  methods: {
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.companyId = this.companyId
        this.form.model.parentId = this.parentId
        this.form.model.sort = this.total
      })
    }
  }
}
</script>
