import { mapState } from 'vuex'
export default {
  computed: {
    ...mapState('app/loading', {
      loadingText: 'text',
      loadingBackground: 'background',
      loadingSpinner: 'spinner'
    })
  },
  methods: {
    _loading (text) {
      return this.$loading({
        lock: true,
        text: text || this.loadingText,
        spinner: this.loadingSpinner,
        background: this.loadingBackground
      })
    }
  }
}
