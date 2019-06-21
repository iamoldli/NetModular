<template>
  <nm-box-row height="400px">
    <nm-box-col :span="12">
      <nm-form-page header title="多文件上传示例">
        <el-row>
          <el-col :span="21" :offset="1">
            <el-form-item label="上传文件" prop="filePath">
              <nm-upload-multiple v-model="filePath" :name.sync="fileName" action="http://ip.com"/>
            </el-form-item>
          </el-col>
        </el-row>
      </nm-form-page>
    </nm-box-col>
  </nm-box-row>
</template>
<script>
export default {
  data() {
    return {
      filePath: '',
      fileName: ''
    }
  }
}
</script>
