<template>
  <nm-form ref="form" v-bind="form" v-on="on">
    <el-row :gutter="50">
      <el-col :span="20" :offset="2">
        <el-form-item label="父节点">
          <el-input v-model="parent.path" disabled/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="所属模块：" prop="moduleCode">
          <module-info-select v-model="form.model.moduleCode"/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="选择路由">
          <el-select v-model="route" placeholder="请选择" @change="onRouteChange">
            <el-option v-for="r in routes" :key="r.code" :label="r.name" :value="r.code">
              <span>{{r.name}}({{r.code}})</span>
            </el-option>
          </el-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row :gutter="50">
      <el-col :span="10" :offset="2">
        <el-form-item label="菜单名称：" prop="name">
          <el-input v-model="form.model.name"/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="路由名称：" prop="routeName">
          <el-input v-model="form.model.routeName" disabled/>
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
import ModuleInfoSelect from '../../../../moduleInfo/select'
export default {
  mixins: [mixins],
  components: { ModuleInfoSelect },
  data () {
    return {
      route: '',
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
    routes () {
      let routes = []
      if (!this.form.model.moduleCode) { return routes }

      this.$router.options.routes.forEach(r => {
        if (r && r.name && r.name.startsWith(this.form.model.moduleCode.toLowerCase())) {
          routes.push({
            name: r.meta.title,
            code: r.name
          })
        }
      })
      return routes
    }
  },
  methods: {
    onRouteChange (val) {
      for (var i = 0; i < this.routes.length; i++) {
        let route = this.routes[i]
        if (route.code === val) {
          this.form.model.name = route.name
          this.form.model.routeName = route.code
          break
        }
      }
    }
  }
}
</script>
