<template>
  <nm-form-page v-bind="form">
    <el-row :gutter="20">
      <el-col :span="22">
        <el-form-item labelWidth="200px" prop="uploadPath">
          <template v-slot:label>
            <el-tooltip effect="dark" content="默认是应用根目录下的Upload目录" placement="top">
              <nm-icon class="nm-size-20 nm-text-warning" name="warning" />
            </el-tooltip>
            文件上传存储根路径：
          </template>
          <el-input v-model="form.model.uploadPath" />
        </el-form-item>
      </el-col>
      <el-col :span="22">
        <el-form-item labelWidth="200px" prop="tempPath">
          <template v-slot:label>
            <el-tooltip effect="dark" content="默认是应用根目录下的Temp目录" placement="top">
              <nm-icon class="nm-size-20 nm-text-warning" name="warning" />
            </el-tooltip>
            临时文件存储根路径：
          </template>
          <el-input v-model="form.model.tempPath" />
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
      code: 'Path',
      form: {
        model: {
          uploadPath: '',
          tempPath: ''
        }
      }
    }
  }
}
</script>
