<template>
  <nm-container>
    <nm-box page header title="个人信息" icon="user">
      <el-form label-width="120px">
        <el-row>
          <el-col :span="10" :offset="1">
            <el-form-item label="编号：">
              <el-input :value="id" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="10">
            <el-form-item label="用户名：">
              <el-input :value="name" disabled />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </nm-box>
  </nm-container>
</template>
<script>
import { mapState } from 'vuex'
export default {
  computed: {
    ...mapState('app/account', ['id', 'name'])
  }
}
</script>
