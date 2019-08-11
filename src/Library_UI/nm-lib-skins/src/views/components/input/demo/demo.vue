<template>
  <nm-box-row height="100%">
    <nm-box-col :span="12">
      <nm-form-page v-bind="form">
        <el-form-item label="统一社会信用代码：" prop="code">
          <nm-input-credit-code v-model="form.model.code" />
        </el-form-item>
        <el-form-item label="手机号：" prop="phone">
          <nm-input-phone v-model="form.model.phone" />
        </el-form-item>
        <el-form-item label="金额：" prop="money">
          <nm-input-number v-model="form.model.money">
            <template slot="append">元</template>
          </nm-input-number>
          <nm-txt>整数</nm-txt>
        </el-form-item>
        <el-form-item label="金额：" prop="money1">
          <nm-input-number v-model="form.model.money1" allow-decimal>
            <template slot="append">元</template>
          </nm-input-number>
          <nm-txt>带小数</nm-txt>
        </el-form-item>
        <el-form-item label="金额：" prop="money2">
          <nm-input-number v-model="form.model.money2" allow-decimal :decimal-limit="4" align-right>
            <template slot="append">元</template>
          </nm-input-number>
          <nm-txt>指定小数位长度4位</nm-txt>
        </el-form-item>
        <el-form-item label="建筑面积：" prop="area">
          <nm-input-number v-model="form.model.area" allow-decimal>
            <template slot="append">平方米</template>
          </nm-input-number>
        </el-form-item>
      </nm-form-page>
    </nm-box-col>
    <nm-box-col :span="12"></nm-box-col>
  </nm-box-row>
</template>
<script>
export default {
  data() {
    return {
      form: {
        title: '限制文本输入格式',
        icon: 'list',
        labelWidth: '200px',
        model: {
          code: '',
          phone: '',
          money: '',
          money1: '',
          money2: '',
          area: ''
        },
        action: this.submit
      },
      select: {
        method: this.queryOptions
      }
    }
  },
  methods: {
    submit() {
      return new Promise(resolve => {
        resolve()
      })
    }
  },
  watch: {
    'form.model.money2'(val) {
      console.log(val)
    }
  }
}
</script>
