<template>
  <div class="nm-attachment-img-preview">
    <img v-if="url" :src="url" />
    <nm-icon class="no-picture" v-else name="photo" />
  </div>
</template>
<script>
import api from '../../../../api/Attachment'
export default {
  name: '',
  data() {
    return {
      url: ''
    }
  },
  props: {
    id: {
      type: String
    }
  },
  methods: {
    get() {
      if (this.id) {
        api.download(this.id).then(url => {
          this.url = url
        })
      } else {
        this.url = ''
      }
    }
  },
  mounted() {
    this.get()
  },
  watch: {
    id() {
      this.get()
    }
  }
}
</script>
<style lang="scss">
.nm-attachment-img-preview {
  display: block;
}
</style>
