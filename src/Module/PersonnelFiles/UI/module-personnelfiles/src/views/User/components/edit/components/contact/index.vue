<template>
  <nm-form ref="form" v-bind="form" v-on="on">
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="居住地址：">
          <nm-area-select :default-value="areaDefault" :level="4" @change="onAreaChange"/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="详细地址：" prop="address">
          <el-input v-model="form.model.address" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="QQ：" prop="qq">
          <el-input v-model="form.model.qq" clearable/>
        </el-form-item>
      </el-col>
      <el-col :span="10">
        <el-form-item label="微信：" prop="wechat">
          <el-input v-model="form.model.wechat" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="10" :offset="1">
        <el-form-item label="钉钉：" prop="dingDing">
          <el-input v-model="form.model.dingDing" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form>
</template>
<script>
import mixins from '../mixins'
import api from '../../../../../../api/User'

export default {
  mixins: [mixins],
  data() {
    return {
      editAction: api.editContact,
      form: {
        action: api.updateContact,
        model: {
          userId: '',
          qq: '',
          wechat: '',
          dingDing: '',
          provinceCode: '',
          provinceName: '',
          cityCode: '',
          cityName: '',
          areaCode: '',
          areaName: '',
          townCode: '',
          townName: '',
          address: ''
        }
      },
      areaDefault: []
    }
  },
  methods: {
    callback() {
      this.areaDefault = []
      if (this.form.model.provinceCode) {
        this.areaDefault.push(this.form.model.provinceCode)
        if (this.form.model.cityCode) {
          this.areaDefault.push(this.form.model.cityCode)
          if (this.form.model.areaCode) {
            this.areaDefault.push(this.form.model.areaCode)
            if (this.form.model.townCode) {
              this.areaDefault.push(this.form.model.townCode)
            }
          }
        }
      }
    },
    onAreaChange(val) {
      if (val.length > 0) {
        this.form.model.provinceCode = val[0].value
        this.form.model.provinceName = val[0].name

        if (val.length > 1) {
          this.form.model.cityCode = val[1].value
          this.form.model.cityName = val[1].name

          if (val.length > 2) {
            this.form.model.areaCode = val[2].value
            this.form.model.areaName = val[2].name
            if (val.length > 3) {
              this.form.model.townCode = val[3].value
              this.form.model.townName = val[3].name
            }
          }
        }
      }
    }
  }
}
</script>
