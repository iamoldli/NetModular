<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="分类：" prop="categoryId">
          <el-input v-model="form.model.categoryId" clearable/>
        </el-form-item>
        <el-form-item label="标题：" prop="title">
          <el-input v-model="form.model.title" clearable/>
        </el-form-item>
        <el-form-item label="内容：" prop="body">
          <el-input v-model="form.model.body" clearable/>
        </el-form-item>
        <el-form-item label="标签列表：" prop="tagList">
          <el-input v-model="form.model.tagList" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import api from '../../../../api/Article'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.formDialogEdit],
  data () {
    return {
      api,
      form: {
        title: '编辑文章',
        action: api.update,
        model: {
          /** 分类 */
          categoryId: '',
          /** 标题 */
          title: '',
          /** 内容 */
          body: '',
          /** 标签列表 */
          tagList: ''
        },
        rules: {
        }
      }
    }
  }
}
</script>
