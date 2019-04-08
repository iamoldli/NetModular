<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="2">
        <el-form-item label="所属菜单">
          <el-input v-model="currentMenu.name" disabled/>
        </el-form-item>
        <el-form-item label="名称" prop="name">
          <el-input autofocus v-model="form.model.name"/>
        </el-form-item>
        <el-form-item label="编码" prop="code">
          <el-input v-model="form.model.code">
            <template v-slot:prepend>{{codePrefix}}</template>
          </el-input>
        </el-form-item>
        <el-form-item label="图标" prop="icon">
          <nm-icon-picker v-model="form.model.icon"/>
        </el-form-item>
        <el-form-item label="排序" prop="sort">
          <el-input v-model="form.model.sort"/>
        </el-form-item>
      </el-col>
    </el-row>
    <!-- <el-row :gutter="20">
      <el-col :span="22">
        <el-form-item label="排序" prop="sort">
          <el-checkbox v-for="btn in buttonList" :key="btn.code" :label="btn.text" :value="btn.code"/>
        </el-form-item>
      </el-col>
    </el-row>-->
  </nm-form-dialog>
</template>
<script>
import { mapState } from 'vuex'
import api from '../../../api/button'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data () {
    return {
      form: {
        title: '添加按钮',
        icon: 'add',
        width: '500px',
        model: {
          // 菜单编号
          menuId: '',
          // 名称
          name: '',
          // 图标
          icon: '',
          // 编码
          code: '',
          // 排序
          sort: 0
        },
        rules: {
          name: [
            { required: true, message: '请输入按钮名称' }
          ],
          code: [
            { required: true, message: '请输入按钮编码' }
          ]
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  props: ['sort'],
  computed: {
    ...mapState('module/admin', ['currentMenu']),
    ...mapState('app/system', ['buttons']),
    buttonList () {
      return this.buttons.filter(item => item.code.startsWith(this.currentMenu.routerName))
    },
    codePrefix () {
      return this.currentMenu.routerName + '_'
    }
  },
  methods: {
    action (model) {
      const m = Object.assign({}, model)
      m.code = this.codePrefix + m.code
      return api.add(m)
    },
    onOpen () {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.menuId = this.currentMenu.id
        this.form.model.sort = this.sort
      })
    },
    onSuccess () {
      this.$emit('success')
    }
  }
}
</script>
