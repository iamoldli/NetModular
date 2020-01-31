<template>
  <nm-form-page v-bind="form" @success="onSuccess">
    <el-row :gutter="20">
      <el-col :span="5" :offset="1">
        <el-form-item labelWidth="160px" label="菜单只能打开一个" prop="menu.uniqueOpened">
          <el-switch v-model="form.model.menu.uniqueOpened" />
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item labelWidth="200px" label="点击模态框可关闭对话框" prop="dialog.closeOnClickModal">
          <el-switch v-model="form.model.dialog.closeOnClickModal" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item labelWidth="200px" label="列表页序号表头名称" prop="list.serialNumberName">
          <el-input v-model="form.model.list.serialNumberName" placeholder="默认 #" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="5" :offset="1">
        <el-form-item labelWidth="160px" label="标签导航显示图标" prop="tabnav.showIcon">
          <el-switch v-model="form.model.tabnav.showIcon" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="20">
      <el-col :span="24">
        <el-form-item label="自定义Css" prop="customCss">
          <el-input type="textarea" :rows="5" placeholder="请输入自定义Css" v-model="form.model.customCss"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-page>
</template>
<script>
import { mapMutations } from 'vuex'
const api = $api.admin.system
export default {
  data() {
    return {
      form: {
        header: false,
        action: api.updateComponentConfig,
        model: this.model
      }
    }
  },
  props: {
    model: Object
  },
  methods: {
    ...mapMutations('app/system', ['setComponentConfig']),
    onSuccess() {
      this.setComponentConfig(this.form.model)
    }
  }
}
</script>
