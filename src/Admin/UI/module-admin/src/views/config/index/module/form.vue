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
            <!--数字类型-->
            <el-input v-if="option.dataType < 4" v-model="form.model[option.dataName]" />
            <!--时间类型-->
            <el-date-picker v-else-if="option.dataType === 4" v-model="form.model[option.dataName]" type="datetime"> </el-date-picker>
            <!--日期类型-->
            <el-date-picker v-else-if="option.dataType === 5" v-model="form.model[option.dataName]" type="date"> </el-date-picker>
            <!--时间类型-->
            <el-time-picker v-else-if="option.dataType === 6" v-model="form.model[option.dataName]"> </el-time-picker>
            <!--枚举类型-->
            <el-select v-else-if="option.dataType === 7" v-model="form.model[option.dataName]" placeholder="请选择...">
              <el-option v-for="item in option.enumOptions" :key="item.value" :label="item.label" :value="item.value"> </el-option>
            </el-select>
            <!--布尔类型-->
            <el-switch v-else-if="option.dataType === 8" v-model="form.model[option.dataName]"> </el-switch>
            <!--文本类型-->
            <el-input v-else-if="option.dataType === 9" v-model="form.model[option.dataName]" type="textarea" :rows="5"> </el-input>
          </el-form-item>
        </el-col>
      </template>
    </el-row>
  </nm-form-page>
</template>
<script>
const { optionsEdit, optionsUpdate } = $api.admin.module
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
      return optionsUpdate({ code: this.moduleCode, values: model })
    }
  },
  created() {
    optionsEdit(this.moduleCode).then(data => {
      this.definitions = data.definitions
      this.form.model = data.instance
    })
  }
}
</script>
