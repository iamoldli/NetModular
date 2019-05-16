<template>
  <nm-box page>
    <el-row :gutter="20">
      <el-col :span="12">
        <nm-box header footer title="基础用法" icon="edit">
          <el-row>
            <el-col :span="19" :offset="2">
              <nm-form :model="model">
                <el-form-item label="姓名：" prop="name">
                  <el-input v-model="model.name" type="text"/>
                </el-form-item>
                <el-form-item label="选择城市：" prop="checked">
                  <nm-checkbox-group v-model="model.checked" :action="query" border/>
                </el-form-item>
              </nm-form>
            </el-col>
          </el-row>
          <template v-slot:footer>{{modelStr}}</template>
        </nm-box>
      </el-col>
      <el-col :span="12">
        <nm-box header footer title="可禁用" icon="edit">
          <el-alert title="禁用个别选项" type="warning" description="通过在返回的数据项中指定 disabled:true 可以禁用对应的选项" show-icon :closable="false"></el-alert>
          <el-row>
            <el-col :span="19" :offset="2">
              <nm-form :model="model1">
                <el-form-item label="姓名：" prop="name">
                  <el-input v-model="model1.name" type="text"/>
                </el-form-item>
                <el-form-item label="选择城市：" prop="checked">
                  <nm-checkbox-group v-model="model1.checked" :action="query1" border/>
                </el-form-item>
              </nm-form>
            </el-col>
          </el-row>
          <template v-slot:footer>{{modelStr1}}</template>
        </nm-box>
      </el-col>
    </el-row>
  </nm-box>
</template>
<script>
export default {
  data () {
    return {
      model: {
        name: '',
        checked: []
      },
      model1: {
        name: '',
        checked: []
      }
    }
  },
  computed: {
    modelStr () {
      return '当前模型：' + JSON.stringify(this.model)
    },
    modelStr1 () {
      return '当前模型：' + JSON.stringify(this.model1)
    }
  },
  methods: {
    query () {
      return new Promise(resolve => {
        resolve([{ label: '南京', value: 'nj' }, { label: '江苏', value: 'js' }])
      })
    },
    query1 () {
      return new Promise(resolve => {
        resolve([{ label: '南京', value: 'nj', disabled: true }, { label: '江苏', value: 'js' }])
      })
    }
  }
}
</script>
<style>
</style>
