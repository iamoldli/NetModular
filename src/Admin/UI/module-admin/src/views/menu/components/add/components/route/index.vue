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
        <el-form-item label="所属模块：" prop="moduleCode">
          <nm-module-select v-model="form.model.moduleCode" />
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="选择页面：" prop="route">
          <el-select v-model="form.model.routeName" placeholder="请选择" @change="onPageChange">
            <el-option v-for="p in pages" :key="p.code" :label="p.name" :value="p.code">
              <span>{{ p.name }}__{{ p.code }}</span>
            </el-option>
          </el-select>
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
        <el-form-item label="路由名称：" prop="routeName">
          <el-input v-model="form.model.routeName" disabled />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="路由参数：" prop="routeParams">
          <el-input v-model="form.model.routeParams" type="textarea" :rows="2" placeholder="params"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="路由参数：" prop="routeQuery">
          <el-input v-model="form.model.routeQuery" type="textarea" :rows="2" placeholder="query"></el-input>
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
import { mapState } from 'vuex'
import mixins from '../mixins'
export default {
  mixins: [mixins],
  data() {
    return {
      form: {
        model: {
          type: 1
        },
        rules: {
          moduleCode: [{ required: true, message: '请选择模块' }],
          routeName: [{ required: true, message: '请选择路由名称' }]
        }
      }
    }
  },
  computed: {
    ...mapState('app/page', { allPages: s => s.pages }),
    pages() {
      if (!this.form.model.moduleCode) return []
      return this.allPages.filter(m => m.moduleCode.toLowerCase() === this.form.model.moduleCode.toLowerCase())
    }
  },
  methods: {
    onPageChange(val) {
      for (var i = 0; i < this.pages.length; i++) {
        let page = this.pages[i]
        if (page.code === val) {
          this.form.model.name = page.name
          this.form.model.routeName = page.code
          this.form.model.icon = page.icon
          break
        }
      }
    }
  }
}
</script>
