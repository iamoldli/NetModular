<template>
  <div :style="{ width, height }" class="nm-file-img-preview">
    <img v-if="url__" :src="url__" />
    <nm-icon class="no-picture" v-else name="photo" />
  </div>
</template>
<script>
export default {
  data() {
    return {
      url_: this.url,
      url__: ''
    }
  },
  props: {
    url: {
      type: String
    },
    //是否私有
    private: Boolean,
    width: {
      type: String,
      default: 'auto'
    },
    height: {
      type: String,
      default: 'auto'
    }
  },
  watch: {
    url: {
      immediate: true,
      handler(val) {
        if (val !== this.url_) {
          if (this.private && this.url) {
            $api.admin.file.preview(this.url).then(url => {
              this.url__ = url
            })
          } else {
            this.url__ = val
          }
          this.url_ = val
        }
      }
    }
  }
}
</script>
<style lang="scss">
.nm-file-img-preview {
  display: block;
  img {
    width: 100%;
  }
}
</style>
