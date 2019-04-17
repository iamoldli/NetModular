<template>
  <section class="nm-main">
    <nm-header/>
    <nm-tabnav/>
    <section class="nm-content">
      <td-container>
        <transition name="fade-transverse">
          <keep-alive :include="keepAlive">
            <router-view :key="routePath"/>
          </keep-alive>
        </transition>
      </td-container>
    </section>
  </section>
</template>
<script>
import NmHeader from '../header'
import { mapState } from 'vuex'
export default {
  components: { NmHeader },
  computed: {
    ...mapState('app/page', ['keepAlive']),
    routePath () {
      let $route = this.$route
      let path = $route.path
      if ($route.meta.parent) {
        path = ''
        let len = $route.matched[0].path.split('/').length
        let arr = $route.path.split('/')
        for (let i = 0; i < len; i++) {
          path += arr[i] + '/'
        }

        if (path) {
          path = path.substring(0, path.length - 1)
        }
      }
      return path
    }
  }
}
</script>
