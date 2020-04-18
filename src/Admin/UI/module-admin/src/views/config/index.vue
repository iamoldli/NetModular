<template>
  <nm-container>
    <nm-tabs>
      <el-tabs v-model="tab">
        <el-tab-pane name="module" lazy>
          <span slot="label"> <nm-icon name="app"></nm-icon>业务模块 </span>
          <module-pane ref="module" />
        </el-tab-pane>
        <el-tab-pane name="library" lazy>
          <span slot="label"> <nm-icon name="system"></nm-icon>基础类库 </span>
          <library-pane ref="library" :descriptors="libraries" />
        </el-tab-pane>
      </el-tabs>
    </nm-tabs>
  </nm-container>
</template>
<script>
import page from './page'
import ModulePane from './module'
import LibraryPane from './library'

const { getDescriptors } = $api.admin.config
export default {
  name: page.name,
  components: { ModulePane, LibraryPane },
  data() {
    return {
      tab: 'module',
      descriptors: []
    }
  },
  computed: {
    libraries() {
      return this.descriptors.filter(m => m.type === 0)
    }
  },
  created() {
    getDescriptors().then(data => {
      this.descriptors = data
    })
  }
}
</script>
