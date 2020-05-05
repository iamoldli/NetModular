<template>
  <div :class="['nm-file-upload-img', disabled ? 'disabled' : '']" :style="{ width, height }" v-loading="loading">
    <div v-if="disabled" class="disabled-bg"></div>
    <el-upload
      ref="upload"
      :action="action"
      :headers="headers"
      :data="uploadData"
      :disabled="disabled || loading"
      :before-upload="onBeforeUpload"
      :show-file-list="false"
      :auto-upload="autoUpload"
      :accept="accept"
      :drag="drag"
      :on-success="onSuccess"
      :on-error="onError"
    >
      <nm-file-img v-show="value_" :url="url" :private="accessMode !== 1" class="preview" />
      <div v-show="!value_" class="text-box">
        <i class="el-icon-upload" :style="{ fontSize: iconSize }"></i>
        <div v-if="!noText" class="text">
          <slot name="text">
            将文件拖到此处，或
            <em>点击上传</em>
          </slot>
        </div>
        <div class="tip">
          <slot name="tip">{{ tip }}</slot>
        </div>
      </div>
    </el-upload>
    <span v-show="value_" class="remove" @click="reset">
      <nm-icon name="close" />
    </span>
  </div>
</template>
<script>
import mixins from '../upload-mixins'

export default {
  mixins: [mixins],
  data() {
    return {
      value_: this.value,
      url: ''
    }
  },
  props: {
    value: {
      type: String
    },
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
    /** 文本说明 */
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
    },
    /**不显示文本 */
    noText: Boolean
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
      if (res.code === 1) {
        const data = res.data
        this.value_ = data.fullPath
        this.url = data.url
        this.$emit('input', this.value_)
        this.$emit('success', data, file)
      } else {
        this._error(res.msg)
        this.$emit('error')
      }
      this.loading = false
    },
    reset() {
      this.value_ = ''
      this.url = ''
      this.$emit('input', this.value_)
    },
    get() {
      if (this.value_) {
        $api.admin.file.getByFullPath(this.value_).then(data => {
          this.url = data.url
        })
      } else {
        this.url = ''
      }
    }
  },
  watch: {
    value: {
      immediate: true,
      handler(val) {
        if (val !== this.value_) {
          this.value_ = val
          this.get()
        }
      }
    }
  }
}
</script>

<style lang="scss">
.nm-file-upload-img {
  position: relative;
  display: inline-block;
  box-sizing: border-box;
  border: 1px dashed #ccc;
  background-color: #fff;
  text-align: center;
  color: #8c939d;
  cursor: pointer;
  border-radius: 5px;
  overflow: hidden;

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

      .text {
        font-size: 13px;
        line-height: 130%;
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
    line-height: 27px;
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

  &.disabled:hover {
    border: 1px dashed #ccc;

    .remove {
      display: none;
    }
  }

  .disabled-bg {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: #f5f7fa;
    z-index: 999;
    opacity: 0.5;
    cursor: not-allowed;
  }
}
</style>
