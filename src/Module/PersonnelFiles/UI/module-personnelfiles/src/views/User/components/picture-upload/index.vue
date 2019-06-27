<template>
  <el-upload class="nm-pf-user-picture-uploader" v-bind="pictureUpload">
    <nm-attachment-img v-if="value" :id="value" class="picture-uploader-img"/>
    <i v-else class="el-icon-plus picture-uploader-icon"></i>
  </el-upload>
</template>
<script>
import { mapState } from 'vuex'
export default {
  props: ['value'],
  computed: {
    ...mapState('module/admin/token', ['accessToken']),
    pictureUpload() {
      return {
        action: this.$attachment.uploadUrl,
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        data: {
          module: 'PersonnelFiles',
          group: 'UserPicture'
        },
        'show-file-list': false,
        'before-upload': this.beforeUpload,
        'on-success': this.onSuccess
      }
    }
  },
  methods: {
    beforeUpload(file) {
      if (file.type !== 'image/jpeg' && file.type !== 'image/png') {
        this._error('用户照片只能是 JPG/PNG 格式!')
        return false
      }
      if (file.size / 1024 > 500) {
        this._error('用户照片大小不能超过 500KB!')
        return false
      }
      return true
    },
    onSuccess(res, file) {
      if (res.code === 1) {
        this.$emit('input', res.data.id)
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

  .picture-uploader-img {
    width: 100%;
    height: 100%;
  }
}
</style>
