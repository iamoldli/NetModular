<template>
  <div>
    <nm-box-row height="250px" style="margin-bottom:20px">
      <nm-box-col :span="12">
        <nm-box header footer title="基础表单示例" icon="list">
          <nm-form ref="form1" v-bind="form1">
            <el-row>
              <el-col :span="21" :offset="1">
                <el-form-item label="姓名：" prop="name">
                  <el-input v-model="form1.model.name"/>
                </el-form-item>
              </el-col>
              <el-col :span="21" :offset="1">
                <el-form-item label="年龄：" prop="age">
                  <el-input v-model="form1.model.age"/>
                </el-form-item>
              </el-col>
            </el-row>
          </nm-form>
          <template v-slot:footer>
            <nm-button type="success" text="保存" @click="$refs.form1.submit()"/>
          </template>
        </nm-box>
      </nm-box-col>
      <nm-box-col :span="12">
        <nm-box header footer title="基础表单示例" icon="list">
          <nm-form ref="form2" v-bind="form2">
            <el-row>
              <el-col :span="10" :offset="1">
                <el-form-item label="姓名：" prop="name">
                  <el-input v-model="form2.model.name"/>
                </el-form-item>
              </el-col>
              <el-col :span="10">
                <el-form-item label="年龄：" prop="age">
                  <el-input v-model="form2.model.age"/>
                </el-form-item>
              </el-col>
            </el-row>
          </nm-form>
          <template v-slot:footer>
            <nm-button type="success" text="保存" @click="$refs.form2.submit()"/>
            <nm-button type="info" text="重置" @click="$refs.form2.reset()"/>
          </template>
        </nm-box>
      </nm-box-col>
    </nm-box-row>
  </div>
</template>
<script>
export default {
  data() {
    return {
      drawer1: false,
      drawer2: false,
      drawer3: false,
      drawer4: false,
      form1: {
        model: {
          name: '',
          age: 0
        },
        rules: {
          name: [{ required: true, message: '请输入姓名' }]
        },
        action() {
          return new Promise(resolve => {
            resolve()
          })
        }
      },
      form2: {
        model: {
          name: '',
          age: 0
        },
        rules: {
          name: [{ required: true, message: '请输入姓名' }]
        },
        action() {
          return new Promise(resolve => {
            resolve()
          })
        }
      }
    }
  }
}
</script>
