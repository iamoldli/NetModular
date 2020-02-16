<template>
  <nm-form-page v-bind="form">
    <el-row :gutter="20">
      <template v-for="(option, index) in definitions">
        <el-col :key="option.dataName" :span="10" :offset="index % 2">
          <el-form-item :prop="option.dataName">
            <template v-slot:label>
              <el-tooltip v-if="option.tooltip" :content="option.tooltip" effect="dark" placement="top">
                <nm-icon class="nm-size-20 nm-text-warning" name="warning" />
              </el-tooltip>
              {{ option.name }}：
            </template>
            <el-input v-if="option.dataType < 4" v-model="form.model[option.dataName]" />
            <el-date-picker v-else-if="option.dataType === 4" v-model="form.model[option.dataName]" type="datetime"> </el-date-picker>
            <el-date-picker v-else-if="option.dataType === 5" v-model="form.model[option.dataName]" type="date"> </el-date-picker>
            <el-time-picker v-else-if="option.dataType === 6" v-model="form.model[option.dataName]"> </el-time-picker>
            <el-select v-else-if="option.dataType === 7" v-model="form.model[option.dataName]" placeholder="请选择...">
              <el-option v-for="item in option.enumOptions" :key="item.value" :label="item.label" :value="item.value"> </el-option>
            </el-select>
            <el-switch v-else-if="option.dataType === 8" v-model="form.model[option.dataName]"> </el-switch>
          </el-form-item>
        </el-col>
      </template>
    </el-row>
  </nm-form-page>
</template>
<script>
const { edit, update } = $api.admin.moduleOptions
export default {
  data() {
    return {
      definitions: [],
      form: {
        header: false,
        action: this.update,
        labelWidth: '200px',
        model: {}
      }
    }
  },
  props: {
    moduleCode: String
  },
  methods: {
    update(model) {
      return update({ moduleCode: this.moduleCode, values: model })
    }
  },
  created() {
    edit(this.moduleCode).then(data => {
      this.definitions = data.definitions
      this.form.model = data.instance
    })
  }
}
</script>
