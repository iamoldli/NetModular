<template>
  <nm-form ref="form" v-bind="form" v-on="on">
    <el-row :gutter="50">
      <el-col :span="20" :offset="2">
        <el-form-item label="父节点">
          <el-input v-model="parent.path" disabled />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="菜单名称：" prop="name">
          <el-input v-model="form.model.name" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="链接：" prop="url">
          <el-input v-model="form.model.url" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="图标：" prop="icon">
          <nm-icon-picker v-model="form.model.icon" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="图标颜色：" prop="iconColor">
          <nm-color-picker v-model="form.model.iconColor" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="打开方式：" prop="target">
          <nm-enum-select v-model="form.model.target" module-code="admin" enum-name="MenuTarget" />
        </el-form-item>
      </el-col>
    </el-row>
    <div v-show="dialogTarget">
      <el-row :gutter="50">
        <el-col :span="10" :offset="2">
          <el-form-item label="宽度：" prop="dialogWidth">
            <el-input v-model="form.model.dialogWidth" placeholder="默认 60%" />
          </el-form-item>
        </el-col>
        <el-col :span="10">
          <el-form-item label="高度：" prop="dialogHeight">
            <el-input v-model="form.model.dialogHeight" placeholder="默认 70%" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row :gutter="50">
        <el-col :span="10" :offset="2">
          <el-form-item label="可全屏：" prop="dialogFullscreen">
            <el-switch v-model="form.model.dialogFullscreen" />
          </el-form-item>
        </el-col>
      </el-row>
    </div>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="排序：" prop="sort">
          <el-input v-model="form.model.sort" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="显示：" prop="show">
          <el-switch v-model="form.model.show" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="20" :offset="2">
        <el-form-item label="备注：" prop="remarks">
          <el-input v-model="form.model.remarks" type="textarea" :rows="2" placeholder="请输入内容"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form>
</template>
<script>
import mixins from '../mixins'
export default {
  mixins: [mixins],
  data() {
    return {
      route: '',
      form: {
        model: {
          type: 2
        },
        rules: {
          url: [{ type: 'url', message: '请输入正确的链接格式' }]
        }
      }
    }
  },
  computed: {
    dialogTarget() {
      return this.form.model.target === 2
    }
  }
}
</script>
