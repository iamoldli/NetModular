import { mapGetters } from 'vuex'
export default {
  computed: {
    ...mapGetters('app/skins', ['fontSize'])
  }
}
