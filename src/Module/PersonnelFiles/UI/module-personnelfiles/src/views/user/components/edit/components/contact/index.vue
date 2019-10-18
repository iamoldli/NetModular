<template>
  <nm-form ref="form" v-bind="form" v-on="on">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="居住地址：">
          <nm-area-select v-model="form.model.area" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="详细地址：" prop="address">
          <el-input v-model="form.model.address" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="QQ：" prop="qq">
          <el-input v-model="form.model.qq" clearable />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="微信：" prop="wechat">
          <el-input v-model="form.model.wechat" clearable />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="钉钉：" prop="dingDing">
          <el-input v-model="form.model.dingDing" clearable />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form>
</template>
<script>
import mixins from '../mixins'

const api = $api.personnelFiles.user

export default {
  mixins: [mixins],
  data() {
    return {
      editAction: api.editContact,
      form: {
        action: api.updateContact,
        model: {
          userId: '',
          qq: '',
          wechat: '',
          dingDing: '',
          area: {},
          address: ''
        }
      }
    }
  }
}
</script>
