<template>
  <nm-form-page v-bind="form">
    <el-row :gutter="20">
      <el-col :span="22">
        <el-form-item label="临时文件存储根路径：" labelWidth="200px" prop="tempPath">
          <el-input v-model="form.model.tempPath" placeholder="如果不填，则与通用路径中的临时文件根路径一致" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import mixins from '../../form-mixins'
export default {
  mixins: [mixins],
  data() {
    return {
      code: 'Excel',
      form: {
        model: {
          tempPath: ''
        }
      }
    }
  }
}
</script>
