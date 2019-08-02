<template>
  <div class="td-attachment-upload-img" :style="style" v-loading="loading">
    <el-upload v-bind="uploadOptions">
      <td-attachment-img v-if="id" :id="id" class="preview" />
      <div class="text-box">
        <i class="el-icon-upload"></i>
        <div class="text">
          <slot name="text">
            将文件拖到此处，或
            <em>点击上传</em>
          </slot>
        </div>
        <div class="tip">
          <slot name="tip">{{tip}}</slot>
        </div>
      </div>
    </el-upload>
  </div>
</template>
<script>
import mixins from '../upload-mixins'
import api from '../../../../api/Attachment'

export default {
  mixins: [mixins],
  props: {
    /** 宽度 */
    width: {
      type: String,
      default: '100px'
    },
    /** 高度 */
    height: {
      type: String,
      default: '150px'
    },
    /** 文章说明 */
    text: String,
    /** 底部提示语 */
    tip: String,
    /** 接受上传的文件类型 */
    accept: {
      type: String,
      default: 'image/jpeg,image/png'
    },
    /** 可接受文件类型 */
    fileType: {
      type: Array,
      default() {
        return ['jpg', 'jpeg', 'png']
      }
    }
  },
  computed: {
    uploadOptions() {
      return {
        action: api.getUploadUrl(),
        headers: {
          Authorization: 'Bearer ' + this.accessToken
        },
        data: {
          module: this.module,
          group: this.group,
          name: this.name,
          auth: this.auth
        },
        accept: this.accept,
        drag: this.drag,
        autoUpload: true,
        showFileList: false,
        beforeUpload: this.onBeforeUpload,
        onSuccess: this.onSuccess,
        onError: this.onError
      }
    },
    style() {
      return { width: this.width, height: this.height }
    }
  },
  methods: {
    onBeforeUpload(file) {
      // 文件格式验证
      if (!this.verifyFileType(file)) {
        return false
      }
      // 验证是否超出最大上传数
      if (this.maxSizeBytes && file.size > this.maxSizeBytes) {
        this._error(`文件大小${this.getSizeUnit(file.size)}超过了最大允许值${this.maxSize}`)
        return false
      }
      if (this.beforeUpload && typeof this.beforeUpload === 'function' && !this.beforeUpload(file)) {
        return false
      }

      this.loading = true
      return true
    },
    onSuccess(res, file) {
      const data = res.data
      if (data.code === 1) {
        this.id = data.data.id
        this.$emit('input', this.id)
        this.$emit('success', res, file)
      } else {
        this.$emit('error')
      }
      this.loading = false
    },
    reset() {
      this.id = ''
      this.$emit('input', this.id)
    }
  }
}
</script>

<style lang="scss">
.td-attachment-upload-img {
  display: inline-block;
  box-sizing: border-box;
  border: 1px dashed #ccc;
  background-color: #fff;
  text-align: center;
  color: #8c939d;
  cursor: pointer;
  border-radius: 5px;

  > div {
    width: 100%;
    height: 100%;
  }
  &:hover {
    border-color: #409eff;
  }

  .preview {
    width: 100%;
    height: 100%;
    line-height: 100%;
    text-align: center;
    img {
      max-width: 100%;
      max-height: 100%;
    }
    .no-picture {
      font-size: 2em;
    }
  }

  .text-box {
    font-size: 14px;

    .el-icon-upload {
      font-size: 5em;
      color: #c0c4cc !important;
    }

    em {
      color: #409eff;
      font-style: normal;
    }
  }

  .el-upload {
    width: 100%;
    height: 100%;
  }

  .el-upload-dragger {
    position: relative;
    top: 50%;
    border: none;
    margin-top: -100px;
    width: 100%;
    height: 200px;
  }

  .el-loading-spinner {
    line-height: 100%;
  }
}
</style>
