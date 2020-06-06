<template>
  <nm-box page :header="false" footer :loading="loading" no-scrollbar>
    <nm-flex style="position: absolute;height:100%" fix-mode="top" fix="60px">
      <template v-slot:top>
        <el-form label-width="100px">
          <el-form-item style="width:500px;margin:15px 0;" label="选择平台：">
            <nm-platform-select v-model="platform" no-web @change="queryBindPlatformPermissions" />
          </el-form-item>
        </el-form>
      </template>
      <template v-slot:bottom>
        <nm-scrollbar>
          <div class="nm-p-l-15 nm-p-r-15 nm-p-b-15">
            <el-tree ref="tree" v-bind="tree">
              <span slot-scope="{ data }">
                <nm-icon name="attachment" />
                <span class="nm-m-l-5"
                  >{{ data.label
                  }}<label v-if="data.item.isPermission" class="nm-p-l-5 nm-text-info nm-size-14" style="font-style: italic;"
                    >(<label>{{ data.item.code }}</label
                    >)</label
                  ></span
                >
              </span>
            </el-tree>
          </div>
        </nm-scrollbar>
      </template>
    </nm-flex>
    <template v-slot:footer>
      <nm-button type="success" text="保存" @click="save" />
      <nm-button type="info" text="重置" @click="reset" />
    </template>
  </nm-box>
</template>
<script>
import { mixins } from 'netmodular-ui'
const api = $api.admin.permission
const { queryBindPlatformPermissions, bindPlatformPermissions } = $api.admin.role
export default {
  mixins: [mixins.visible],
  data() {
    return {
      tree: {
        data: [],
        nodeKey: 'id',
        defaultExpandAll: true,
        showCheckbox: true
      },
      platform: 1,
      loading: false,
      permissions: []
    }
  },
  props: {
    id: String
  },
  methods: {
    queryPermissionTree() {
      this.loading = true
      api
        .tree()
        .then(data => {
          this.tree.data = [data]
          this.loading = false
        })
        .catch(() => {
          this.loading = false
        })
    },
    queryBindPlatformPermissions() {
      this.loading = true
      queryBindPlatformPermissions({ roleId: this.id, platform: this.platform }).then(data => {
        this.permissions = data
        this.reset()
        this.loading = false
      })
    },
    save() {
      let permissions = this.$refs.tree
        .getCheckedNodes()
        .filter(m => m.item.isPermission)
        .map(m => {
          return m.item.code
        })
      this.loading = true
      bindPlatformPermissions({ roleId: this.id, platform: this.platform, permissions })
        .then(() => {
          this._success('保存成功')
          this.permissions = permissions
          this.loading = false
        })
        .catch(() => {
          this.loading = false
        })
    },
    reset() {
      this.$nextTick(() => {
        this.$refs.tree.setCheckedKeys(this.permissions)
      })
    }
  },
  created() {
    this.queryPermissionTree()
    this.queryBindPlatformPermissions()
  },
  watch: {
    id() {
      this.platform = 1
      this.queryBindPlatformPermissions()
    }
  }
}
</script>
