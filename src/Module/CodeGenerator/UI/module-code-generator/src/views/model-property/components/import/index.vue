<template>
  <nm-dialog v-bind="dialog" :visible.sync="visible_" @open="onOpen">
    <el-transfer class="nm-code-generator-model-property-import" v-model="value" :data="list"></el-transfer>
    <template v-slot:footer>
      <nm-button type="success" text="保存" @click="submit"/>
    </template>
  </nm-dialog>
</template>
<script>
import { mixins } from 'nm-lib-skins'
import propertyApi from '../../../../api/property'
import api from '../../../../api/model-property'
export default {
  mixins: [mixins.dialog],
  data () {
    return {
      list: [],
      value: [],
      loading: false
    }
  },
  props: {
    total: Number,
    parent: {
      type: Object,
      required: true
    },
    modelType: {
      type: Number,
      required: true
    }
  },
  computed: {
    dialog () {
      return {
        title: `从实体中导入属性(${this.parent.name})`,
        icon: 'import',
        width: '718px',
        height: '600px',
        noScrollbar: true,
        loading: this.loading,
        footer:true
      }
    }
  },
  methods: {
    queryList () {
      this.list = []
      propertyApi.select(this.parent.id).then(data => {
        data.map(m => {
          if (m.value !== 'Id') {
            this.list.push({
              key: m.value,
              label: m.label,
              disabled: this.value.includes(m.value),
              data: m.data
            })
          }
        })
      })
    },
    querySelected () {
      this.value = []
      api.select({ classId: this.parent.id, modelType: this.modelType }).then(data => {
        data.map(m => {
          this.value.push(m.value)
        })
        this.queryList()
      })
    },
    submit () {
      let ids = []
      this.list.map(m => {
        if (this.value.includes(m.key) && !m.disabled) {
          ids.push(m.data)
        }
      })

      if (ids.length < 1) {
        this._warning('请选择属性')
        return
      }

      const params = { classId: this.parent.id, modelType: this.modelType, ids, sort: this.total }
      this.loading = true
      api.importFromEntity(params).then(data => {
        this._success('保存成功')
        this.$emit('success')
        this.loading = false
        this.visible_ = false
      }).catch(() => {
        this._error('保存失败')
        this.loading = false
      })
    },
    onOpen () {
      this.querySelected()
    }
  }
}
</script>
<style lang="scss">
.nm-code-generator-model-property-import {
  padding: 10px;
  height: 100%;
  box-sizing: border-box;
  .el-transfer-panel {
    width: 300px;
    height: 100%;
    .el-transfer-panel__body {
      height: 455px;
      .el-transfer-panel__list {
        height: 100%;
      }
    }
  }
}
</style>
