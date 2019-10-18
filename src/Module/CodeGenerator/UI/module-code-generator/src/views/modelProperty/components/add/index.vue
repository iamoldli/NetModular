<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="所属类：">
          <el-input v-model="parent.name" disabled />
        </el-form-item>
        <el-form-item label="名称：" prop="name">
          <el-input v-model="form.model.name" />
        </el-form-item>
        <el-form-item label="类型：" prop="type">
          <type-select v-model="form.model.type" />
        </el-form-item>
        <el-form-item v-show="showEnum" label="枚举：" prop="enumId">
          <enum-select v-model="form.model.enumId" />
        </el-form-item>
        <el-form-item label="可空：" prop="nullable">
          <el-switch v-model="form.model.nullable" />
        </el-form-item>
        <el-form-item label="备注：" prop="remarks">
          <el-input v-model="form.model.remarks" />
        </el-form-item>
        <el-form-item label="序号：" prop="sort">
          <el-input type="number" v-model.number="form.model.sort" />
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import TypeSelect from '../../../property/components/type-select'
import EnumSelect from '../../../enum/components/select'

const api = $api.codeGenerator.modelProperty
const enumApi = $api.codeGenerator.enum

export default {
  mixins: [mixins.dialog],
  components: { TypeSelect, EnumSelect },
  data() {
    // 验证枚举
    const validateEnum = (rule, value, callback) => {
      if (this.showEnum && value === '') {
        callback(new Error('请选择枚举'))
      } else {
        callback()
      }
    }
    return {
      form: {
        title: '添加模型属性',
        icon: 'add',
        width: '40%',
        action: api.add,
        model: {
          classId: 0,
          name: '',
          type: 0,
          modelType: 0,
          nullable: false,
          enumId: '',
          remarks: '',
          sort: 0
        },
        rules: {
          classId: [
            { required: true, message: '请选择类', trigger: 'blur' }
          ],
          name: [
            { required: true, message: '请输入类名', trigger: 'blur' }
          ],
          type: [
            { required: true, message: '请选择类型', trigger: 'blur' },
            { type: 'number', message: '请选择正确的类型', trigger: 'blur' }
          ],
          enumId: [{ validator: validateEnum }],
          remarks: [{ required: true, message: '请输入备注', trigger: 'blur' }]
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  props: {
    total: Number,
    parent: {
      type: Object,
      required: true
    },
    modelType: {
      type: Number,
      required: true
    }
  },
  computed: {
    showEnum() {
      return this.form.model.type === 11
    }
  },
  methods: {
    getEnumSelect() {
      return enumApi.select()
    },
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
        this.form.model.classId = this.parent.id
        this.form.model.sort = this.total
        this.form.model.modelType = this.modelType
      })
    }
  },
  watch: {
    'form.model.type'() {
      if (!this.showEnum) {
        this.form.model.enumId = ''
      }
    }
  }
}
</script>
