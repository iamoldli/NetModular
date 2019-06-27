<template>
  <img :src="url">
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
