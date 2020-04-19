<template>
  <el-tabs class="nm-admin-config-library page" v-model="tab" tab-position="left" type="border-card">
    <!--系统信息-->
    <el-tab-pane name="system">
      <span slot="label"> <nm-icon name="system"></nm-icon>系统信息 </span>
      <system-config />
    </el-tab-pane>
    <!--前端组件-->
    <el-tab-pane name="component" lazy>
      <span slot="label"> <nm-icon name="table"></nm-icon>前端组件 </span>
      <component-config />
    </el-tab-pane>
    <!--通用路径-->
    <el-tab-pane name="path" lazy>
      <span slot="label"> <nm-icon name="attachment"></nm-icon>通用路径 </span>
      <path-config />
    </el-tab-pane>
    <!--认证授权-->
    <el-tab-pane name="auth" lazy>
      <span slot="label"> <nm-icon name="verifycode"></nm-icon>认证授权 </span>
      <auth-config />
    </el-tab-pane>
    <!--Excel-->
    <el-tab-pane name="excel" lazy>
      <span slot="label"> <nm-icon name="xlsx"></nm-icon>Excel </span>
      <excel-config />
    </el-tab-pane>
    <!--Redis-->
    <el-tab-pane v-if="hasRedis" name="redis" lazy>
      <span slot="label"> <nm-icon name="redis"></nm-icon>Redis </span>
      <redis-config />
    </el-tab-pane>
    <!--RabbitMQ-->
    <el-tab-pane v-if="hasRabbitMQ" name="rabbitmq" lazy>
      <span slot="label"> <nm-icon name="rabbit-mq"></nm-icon>RabbitMQ </span>
      <rabbitmq-config />
    </el-tab-pane>
    <!--Quartz-->
    <el-tab-pane v-if="hasQuartz" name="quartz" lazy>
      <span slot="label"> <nm-icon name="timer"></nm-icon>Quartz </span>
      <quartz-config />
    </el-tab-pane>
  </el-tabs>
</template>
<script>
import SystemConfig from './components/system'
import ComponentConfig from './components/component'
import PathConfig from './components/path'
import AuthConfig from './components/auth'
import RedisConfig from './components/redis'
import RabbitmqConfig from './components/rabbitmq'
import ExcelConfig from './components/excel'
import QuartzConfig from './components/quartz'
export default {
  components: { SystemConfig, ComponentConfig, PathConfig, AuthConfig, RedisConfig, RabbitmqConfig, ExcelConfig, QuartzConfig },
  data() {
    return {
      tab: 'system'
    }
  },
  props: {
    descriptors: Array
  },
  computed: {
    hasRedis() {
      return this.has('redis')
    },
    hasRabbitMQ() {
      return this.has('RabbitMQ')
    },
    hasQuartz() {
      return this.has('Quartz')
    }
  },
  methods: {
    has(code) {
      return this.descriptors.findIndex(m => m.code.toLowerCase() === code.toLowerCase()) > -1
    }
  }
}
</script>
<style lang="scss">
.nm-admin-config-library {
  .el-tabs__header {
    margin-right: 0 !important;
  }
  .el-tabs__item {
    text-align: left !important;

    &.is-active {
      color: #67c23a !important;
    }
  }
  .el-tabs__content {
    .nm-box {
      border-left: none !important;
    }
  }
}
</style>
