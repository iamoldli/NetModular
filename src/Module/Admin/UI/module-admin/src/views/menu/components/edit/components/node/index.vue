<template>
  <nm-form ref="form" v-bind="form" v-on="on">
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="父节点">
          <el-input v-model="parent.path" disabled/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="菜单名称：" prop="name">
          <el-input v-model="form.model.name"/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="图标：" prop="icon">
          <nm-icon-picker v-model="form.model.icon"/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="图标颜色：" prop="iconColor">
          <nm-color-picker v-model="form.model.iconColor"/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="排序：" prop="sort">
          <el-input v-model="form.model.sort"/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="显示：" prop="show">
          <el-switch v-model="form.model.show"/>
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
  mixins: [mixins]
}
</script>
