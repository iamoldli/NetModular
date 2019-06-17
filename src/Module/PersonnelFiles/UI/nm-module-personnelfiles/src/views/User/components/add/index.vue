<template>
  <nm-form-dialog ref="form" v-bind="form" v-on="on" :visible.sync="visible_">
    <el-row>
      <el-col :span="14" :offset="1">
        <el-form-item label="工号：" prop="number">
          <el-input v-model="form.model.number" clearable/>
        </el-form-item>
        <el-form-item label="姓名：" prop="name">
          <el-input v-model="form.model.name" clearable/>
        </el-form-item>
        <el-form-item label="性别：" prop="sex">
          <el-input v-model="form.model.sex" clearable/>
        </el-form-item>
      </el-col>
      <el-col :span="6">
        <el-form-item label="照片：" prop="picture">
          <el-upload class="nm-pf-user-picture-uploader" v-bind="pictureUpload">
            <img v-if="form.model.picture" :src="form.model.picture" class="picture-uploader-img">
            <i v-else class="el-icon-plus picture-uploader-icon"></i>
          </el-upload>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="20" :offset="1">
        <el-form-item label="学历：" prop="education">
          <el-input v-model="form.model.education" clearable/>
        </el-form-item>
        <el-form-item label="籍贯：" prop="native">
          <el-input v-model="form.model.native" clearable/>
        </el-form-item>
        <el-form-item label="所属部门：" prop="departmentId">
          <el-input v-model="form.model.departmentId" clearable/>
        </el-form-item>
        <el-form-item label="职位编号：" prop="positionId">
          <el-input v-model="form.model.positionId" clearable/>
        </el-form-item>
        <el-form-item label="民族：" prop="nation">
          <el-input v-model="form.model.nation" clearable/>
        </el-form-item>
        <el-form-item label="出生日期：" prop="birthday">
          <el-input v-model="form.model.birthday" clearable/>
        </el-form-item>
      </el-col>
    </el-row>
  </nm-form-dialog>
</template>
<script>
import { mapState } from 'vuex'
import api from '../../../../api/User'
import { mixins } from 'nm-lib-skins'
export default {
  mixins: [mixins.dialog],
  data() {
    return {
      form: {
        title: '添加用户信息',
        icon: 'add',
        action: api.add,
        model: {
          /** 学历 */
          education: '',
          /** 姓名 */
          name: '',
          /** 籍贯 */
          native: '',
          /** 所属部门 */
          departmentId: '',
          /** 职位编号 */
          positionId: '',
          /** 民族 */
          nation: '',
          /** 工号 */
          number: '',
          /** 出生日期 */
          birthday: '',
          /** 性别 */
          sex: '',
          /** 照片 */
          picture: ''
        },
        rules: {
        }
      },
      on: {
        success: this.onSuccess,
        open: this.onOpen
      }
    }
  },
  computed: {
    ...mapState('module/admin/token', ['accessToken']),
    pictureUpload() {
      return {
        action: api.getUploadPictureUrl(),
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        'show-file-list': false,
        'before-upload': this.beforeLogoUpload,
        'on-success': this.handleLogoSuccess
      }
    }
  },
  methods: {
    onSuccess() {
      this.$emit('success')
    },
    onOpen() {
      this.$nextTick(() => {
        this.$refs.form.reset()
      })
    },
    beforeLogoUpload(file) {
      if (file.type !== 'image/jpeg' && file.type !== 'image/png') {
        this._error('上传Logo图片只能是 JPG/PNG 格式!')
        return false
      }
      if (file.size / 1024 > 2048) {
        this._error('上传Logo图片大小不能超过 2MB!')
        return false
      }
      return true
    },
    handleLogoSuccess(res, file) {
      if (res.code === 1) {
        this.form.model.picture = res.data.fullPath
      }
    }
  }
}
</script>
<style lang="scss">
.nm-pf-user-picture-uploader {
  width: 100px;
  height: 125px;
  line-height: 125px;
  border: 1px dashed #ccc;
  text-align: center;
  font-size: 25px;
  color: #8c939d;
  cursor: pointer;
  border-radius: 5px;

  &:hover {
    border-color: #409eff;
  }

  .el-upload {
    width: 100%;
  }
}
</style>
