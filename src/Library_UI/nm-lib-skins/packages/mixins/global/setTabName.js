import { mapActions } from 'vuex'
export default {
  methods: {
    ...mapActions('app/page', ['setTabName'])
  }
}
