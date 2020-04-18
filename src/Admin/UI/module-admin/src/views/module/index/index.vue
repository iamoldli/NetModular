<template>
  <nm-container>
    <nm-box v-bind="box">
      <template v-slot:title
        >模块中心<span class="nm-p-l-10 nm-size-12">
          当前模块：<label class="nm-text-primary">{{ list.length }}</label></span
        >
      </template>
      <template v-slot:toolbar>
        <el-tooltip effect="dark" content="同步模块信息" placement="top" v-nm-has="buttons.sync.code">
          <nm-button icon="refresh" size="mini" @click="sync" />
        </el-tooltip>
      </template>
      <div class="module-list">
        <div class="module-list-item" v-for="(item, i) in list" :key="i">
          <div class="module-list-item-bar" :style="{ backgroundColor: item.color }"></div>
          <div class="module-list-item-wrapper">
            <div class="module-list-item-icon" :style="{ backgroundColor: item.color }"><nm-icon :name="item.icon" /></div>
            <div class="module-list-item-info">
              <p>
                {{ item.name
                }}<label class="api-request-count"
                  >访问<label class="nm-text-warning nm-p-l-3 nm-p-r-3">{{ item.apiRequestCount }}</label
                  >次</label
                >
              </p>
              <p><span>编码：</span>{{ item.code }}</p>
              <p><span>版本：</span>{{ item.version }}</p>
              <p :title="item.remarks"><span>说明：</span>{{ item.remarks }}</p>
            </div>
            <div class="module-list-item-btn">
              <nm-button text="接口" size="mini" type="success" @click="openPermission(item)" />
              <nm-button text="页面" size="mini" type="primary" @click="openPage(item)" />
              <nm-button text="删除" size="mini" type="danger" @click="remove(item)" />
            </div>
          </div>
        </div>
      </div>
    </nm-box>
    <!--接口列表-->
    <permission-list :module="module" :visible.sync="dialog.permission" />
    <!--页面列表-->
    <page-list :module="module" :visible.sync="dialog.page" />
  </nm-container>
</template>
<script>
import PermissionList from '../components/permission-list'
import PageList from '../components/page-list'
import page from './page'
// 接口
const { query, sync } = $api.admin.module

export default {
  name: page.name,
  components: { PermissionList, PageList },
  data() {
    return {
      list: [],
      colors: ['#409EFF', '#67C23A', '#E6A23C', '#F56C6C', '#6d3cf7', '#0079de'],
      box: {
        page: true,
        header: true,
        icon: 'app',
        fullscreen: true,
        loading: false
      },
      dialog: {
        permission: false,
        page: false
      },
      buttons: page.buttons,
      module: {}
    }
  },
  methods: {
    refresh() {
      query()
        .then(data => {
          this.list = data.map(m => {
            m.color = this.colors[parseInt(Math.random() * this.colors.length)]
            return m
          })
          this.box.loading = false
        })
        .catch(() => {
          this.box.loading = false
        })
    },
    openPermission(m) {
      this.module = m
      this.dialog.permission = true
    },
    openPage(m) {
      this.module = m
      this.dialog.page = true
    },
    sync() {
      this.box.loading = true
      sync()
        .then(() => {
          this.refresh()
        })
        .catch(() => {
          this.box.loading = false
        })
    },
    remove() {
      this._warning('在线删除模块功能开发中，敬请期待~', '提示')
    }
  },
  created() {
    this.refresh()
  }
}
</script>
<style lang="scss" scoped>
.module-list {
  height: 100%;
  &-item {
    position: relative;
    display: inline-block;
    margin: 0 1% 10px 0;
    width: 24%;
    height: 100px;

    &:nth-child(2n + 1) {
      margin-left: 0.5%;
    }

    &:nth-child(2n) {
      margin-right: 0.5%;
    }

    &-bar {
      position: absolute;
      left: 50%;
      top: 0;
      height: 2px;
      width: 0;
      margin-left: 0;
      transition: all 0.3s ease-in-out;
      z-index: 9999;
    }

    &-wrapper {
      position: absolute;
      padding: 0 0 0 80px;
      width: 100%;
      border: 1px solid #ebeef5;
      box-sizing: border-box;
      cursor: pointer;
      background-color: #fff;
      height: 100px;
      transition: all 0.3s ease-in-out;
      overflow: hidden;
    }

    &-icon {
      position: absolute;
      left: 10px;
      top: 20px;
      width: 60px;
      height: 60px;
      line-height: 60px;
      text-align: center;
      font-size: 40px;
      transition: all 0.4s ease-in-out;
      color: #fff;
      border-radius: 30px;
    }

    &-info {
      padding: 5px 10px 5px 0;
      height: 80px;
      line-height: 1.7;
      font-size: 12px;

      p {
        position: relative;
        padding-left: 34px;
        white-space: nowrap;
        text-overflow: ellipsis;
        overflow: hidden;
        word-break: break-all;
        color: #606266;

        span {
          position: absolute;
          left: 0;
        }
      }

      p:first-child {
        padding-left: 0;
        font-size: 14px;
        font-weight: 600;
        color: #333;
      }

      .api-request-count {
        float: right;
        font-size: 11px;
        font-weight: 400;
      }
    }

    &-btn {
      padding: 10px;
      text-align: right;

      .el-button--mini {
        padding: 4px 6px;
      }
    }

    &:hover {
      .module-list-item-bar {
        width: 100% !important;
        margin-left: -50%;
      }

      .module-list-item-wrapper {
        height: 135px;
        z-index: 999;
        box-shadow: 0 2px 12px 0 rgba(64, 158, 255, 0.5);
      }

      .module-list-item-icon {
        transform: rotateZ(360deg);
      }
    }
  }
}
</style>
