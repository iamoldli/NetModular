<template>
  <nm-container>
    <nm-box-row height="100%" style="margin-bottom:20px">
      <nm-box-col :span="12">
        <nm-box header title="显示等级" icon="list">
          <el-alert type="warning">通过level属性设置等级</el-alert>
          <nm-form ref="form1" :model="model">
            <el-row>
              <el-col :span="21" :offset="1">
                <el-form-item label="省：">
                  <nm-area-select level="1234" mode="cascader" separator="/" v-model="model" @change="onChange1" />
                </el-form-item>
              </el-col>
              <!-- <el-col :span="21" :offset="1">
                <el-form-item label="省市：">
                  <nm-area-select :level="2" @change="onChange1" />
                </el-form-item>
              </el-col>
              <el-col :span="21" :offset="1">
                <el-form-item label="省市区：">
                  <nm-area-select :level="3" @change="onChange1" />
                </el-form-item>
              </el-col>
              <el-col :span="21" :offset="1">
                <el-form-item label="省市区乡：">
                  <nm-area-select :level="4" :default-value="defaultValue" @change="onChange1" />
                </el-form-item>
              </el-col>
              -->
              <el-col :span="21" :offset="1">
                <el-form-item label="值：">
                  <el-input type="textarea" :rows="10" v-model="demo1" readonly></el-input>
                </el-form-item>
              </el-col>
            </el-row>
          </nm-form>
        </nm-box>
      </nm-box-col>
      <nm-box-col :span="12">
        <nm-box header title="指定返回格式" icon="list">
          <el-alert type="warning">通过 dataType 属性设置返回数据格式</el-alert>
          <!-- <nm-form ref="form1" :model="model" label-width="150px">
            <el-row>
              <el-col :span="21" :offset="1">
                <el-form-item label="完整数据(all)：">
                  <nm-area-select :level="4" @change="onChange2" />
                </el-form-item>
              </el-col>
              <el-col :span="21" :offset="1">
                <el-form-item label="编号(id)：">
                  <nm-area-select :level="4" data-type="value" @change="onChange2" />
                </el-form-item>
              </el-col>
              <el-col :span="21" :offset="1">
                <el-form-item label="名称(name)：">
                  <nm-area-select :level="4" data-type="name" @change="onChange2" />
                </el-form-item>
              </el-col>
              <el-col :span="21" :offset="1">
                <el-form-item label="编码(code)：">
                  <nm-area-select :level="4" data-type="code" @change="onChange2" />
                </el-form-item>
              </el-col>
              <el-col :span="21" :offset="1">
                <el-form-item label="值：">
                  <el-input type="textarea" :rows="10" v-model="demo2" readonly></el-input>
                </el-form-item>
              </el-col>
            </el-row>
          </nm-form>-->
        </nm-box>
      </nm-box-col>
    </nm-box-row>
  </nm-container>
</template>
<script>
import page from './page'
export default {
  name: page.name,
  data() {
    return {
      demo1: '',
      demo2: '',
      model: {}
    }
  },
  methods: {
    onChange1(val) {
      this.demo1 = JSON.stringify(val)
    },
    onChange2(val) {
      this.demo2 = JSON.stringify(val)
    }
  },
  mounted() {
    setTimeout(() => {
      this.model = { province: { code: '110000000000' } }
    }, 2000)
  }
}
</script>
