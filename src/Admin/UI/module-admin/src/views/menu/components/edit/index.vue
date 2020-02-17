<template>
  <nm-dialog v-bind="dialog" @open="onOpen" :visible.sync="visible_">
    <node-menu ref="node" v-if="model.type === 0" :model="model" :parent="parent" v-on="on" />
    <route-menu ref="route" v-else-if="model.type === 1" :model="model" :parent="parent" v-on="on" />
    <link-menu ref="link" v-else :model="model" :parent="parent" v-on="on" />
    <template v-slot:footer>
      <nm-button text="保存" type="success" @click="onSubmit" />
      <nm-button text="重置" type="info" @click="onReset" />
    </template>
  </nm-dialog>
</template>
<script>
import { mixins } from 'netmodular-ui'
import NodeMenu from './components/node'
import RouteMenu from './components/route'
import LinkMenu from './components/link'

const api = $api.admin.menu

export default {
  mixins: [mixins.dialog],
  components: { NodeMenu, RouteMenu, LinkMenu },
  data() {
    return {
      model: {
        type: 0
      },
      dialog: {
        footer: true,
        title: '编辑菜单',
        icon: 'add',
        width: '60%',
        class: 'nm-admin-menu-edit',
        loading: false
      },
      on: {
        success: this.onSuccess,
        error: this.onError,
        'validate-error': this.onValidateError
      }
    }
  },
  props: ['parent', 'id'],
  methods: {
    async edit() {
      if (this.id === '') {
        this._warning('请选择要编辑的菜单~')
        return
      }

      this.dialog.loading = true
      this.model = await api.edit(this.id)
      this.setModel()
      this.dialog.loading = false
    },
    onOpen() {
      this.edit()
    },
    onSubmit() {
      this.dialog.loading = true
      this.getModel().submit()
    },
    onReset() {
      this.$nextTick(() => {
        this.getModel().reset()
      })
    },
    onSuccess(model) {
      this.dialog.loading = false
      this.$emit('success', model)
    },
    onError() {
      this.dialog.loading = false
    },
    onValidateError() {
      this.dialog.loading = false
    },
    setModel() {
      this.$nextTick(() => {
        this.getModel().setModel()
      })
    },
    getModel() {
      if (this.model.type === 0) {
        return this.$refs.node
      } else if (this.model.type === 1) {
        return this.$refs.route
      } else {
        return this.$refs.link
      }
    }
  }
}
</script>
