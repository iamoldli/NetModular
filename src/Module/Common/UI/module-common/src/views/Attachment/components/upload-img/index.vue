<template>
  <div class="nm-attachment-upload-img" :style="style" v-loading="loading">
    <el-upload v-bind="uploadOptions">
      <nm-attachment-img v-show="id" :id="id" class="preview" />
      <div v-show="!id" class="text-box">
        <i class="el-icon-upload" :style="{fontSize:iconSize}"></i>
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
    <span v-show="id" class="remove" @click="reset">
      <nm-icon name="close" />
    </span>
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
    },
    /** 图标大小 */
    iconSize: {
      type: String,
      default: '5em'
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
.nm-attachment-upload-img {
  position: relative;
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

  .el-upload {
    width: 100%;
    height: 100%;
  }

  .el-upload-dragger {
    position: relative;
    top: 50%;
    border: none;
    margin-top: -50%;
    width: 100%;
    height: auto;
    .text-box {
      font-size: 14px;

      .el-icon-upload {
        margin: auto;
        font-size: 5em;
        color: #c0c4cc !important;
      }

      em {
        color: #409eff;
        font-style: normal;
      }
    }
  }

  .el-loading-spinner {
    line-height: 100%;
  }

  .remove {
    position: absolute;
    display: none;
    top: 0;
    right: 0;
    width: 30px;
    height: 30px;
    border-radius: 15px;
    background-color: #f56c6c;
    color: #ccc;
    font-size: 16px;

    &:hover {
      color: #fff;
    }
  }

  &:hover {
    border-color: #409eff;

    .remove {
      display: inline-block;
    }
  }
}
</style>
