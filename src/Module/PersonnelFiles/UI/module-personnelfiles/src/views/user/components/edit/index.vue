<template>
  <nm-dialog v-bind="dialog" @opened="onOpened" :visible.sync="visible_">
    <el-tabs class="nm-dialog-tabs" v-model="tab" type="card" @tab-click="onTabClick">
      <el-tab-pane name="base">
        <span slot="label">
          <nm-icon name="node"/>基础信息
        </span>
        <base-page ref="base" v-on="on"/>
      </el-tab-pane>
      <el-tab-pane name="contact">
        <span slot="label">
          <nm-icon name="phone"/>联系方式
        </span>
        <contact-page ref="contact" v-on="on"/>
      </el-tab-pane>
    </el-tabs>
    <template v-slot:footer>
      <nm-button text="保存" type="success" @click="onSubmit"/>
      <nm-button text="重置" type="info" @click="onReset"/>
    </template>
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import BasePage from './components/base'
import ContactPage from './components/contact'

export default {
  mixins: [mixins.dialog],
  components: { BasePage, ContactPage },
  data() {
    return {
      userId: '',
      tab: 'base',
      dialog: {
        title: '编辑用户信息',
        icon: 'edit',
        footer: true,
        loading: false
      },
      on: {
        success: this.onSuccess,
        error: this.onError,
        'validate-error': this.onValidateError
      }
    }
  },
  props: {
    id: {
      type: String,
      required: true
    }
  },
  methods: {
    edit() {
      this.dialog.loading = true
      this.$refs[this.tab].edit(this.userId).then(() => {
        this.dialog.loading = false
      }).catch(() => {
        this.dialog.loading = false
      })
    },
    refresh() {
      this.$refs.base.refresh()
      this.$refs.contact.refresh()
      this.edit()
    },
    onOpened() {
      if (this.userId !== this.id) {
        this.userId = this.id
        this.tab = 'base'
        this.refresh()
      }
    },
    onReset() {
      this.$refs[this.tab].reset()
    },
    onSubmit() {
      this.dialog.loading = true
      this.$refs[this.tab].submit()
    },
    onSuccess() {
      this.dialog.loading = false
      this.$emit('success')
    },
    onError() {
      this.dialog.loading = false
    },
    onValidateError() {
      this.dialog.loading = false
    },
    onTabClick(tab) {
      this.edit()
    }
  }
}
</script>
