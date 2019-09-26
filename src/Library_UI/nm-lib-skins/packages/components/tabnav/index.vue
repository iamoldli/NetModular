<template>
  <section :class="['nm-tabnav',fontSize]">
    <slot name="before" />
    <div class="nm-tabnav-tabs">
      <el-tabs :value="current.path" type="card" :closable="true" @tab-click="click" @edit="edit">
        <el-tab-pane label="首页" :name="defaultPage"></el-tab-pane>
        <el-tab-pane v-for="item in opened" :key="item.path" :label="item.tabName" :name="item.path" />
      </el-tabs>
    </div>
    <div class="nm-tabnav-control">
      <el-dropdown @command="cmd => handleCommand(cmd)">
        <span class="nm-tabnav-control-btn">
          <nm-icon name="angle-double-down" />
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item command="left">
            <nm-icon name="arrow-left" class="nm-tabnav-control-icon" />关闭左侧
          </el-dropdown-item>
          <el-dropdown-item command="right">
            <nm-icon name="arrow-right" class="nm-tabnav-control-icon" />关闭右侧
          </el-dropdown-item>
          <el-dropdown-item command="other">
            <nm-icon name="other" class="nm-tabnav-control-icon" />关闭其他
          </el-dropdown-item>
          <el-dropdown-item command="all">
            <nm-icon name="app" class="nm-tabnav-control-icon" />全部关闭
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </div>
  </section>
</template>
<script>
import { mapState, mapActions } from 'vuex'
export default {
  name: 'Tabnav',
  computed: {
    ...mapState('app/page', { opened: 'opened', current: 'current', defaultPage: 'default' })
  },
  methods: {
    ...mapActions('app/page', ['close', 'closeLeft', 'closeRight', 'closeOther', 'closeAll']),
    click(tab) {
      if (tab.name === this.defaultPage) {
        this.$router.push(this.defaultPage)
        return
      }
      const page = this.opened.find(page => page.path === tab.name)
      if (page) {
        const { name, params, query } = page
        this.$router.push({ name, params, query })
      }
    },
    edit(tabName) {
      this.close({ path: tabName, router: this.$router })
    },
    /**
     * @description 处理关闭标签下拉菜单命令
     * @param {String} cmd 命令
     * @param {String} tagName 选择的标签名称
     */
    handleCommand(cmd, tagName = null) {
      const params = { path: tagName, router: this.$router }
      switch (cmd) {
        case 'left': this.closeLeft(params); break
        case 'right': this.closeRight(params); break
        case 'other': this.closeOther(params); break
        case 'all': this.closeAll(params); break
      }
    }
  }
}
</script>
