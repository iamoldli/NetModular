<template>
  <div class="nm-idb-block" :class="isActive?'is-active':''" @click="$emit('isActiveSkin',skinName)">
    <!-- <div class="nm-idb-block-title">
      <slot name="header"></slot>
      <span style="float: right;padding-right:20px">
        <el-button v-if="isCollapse" type="text" :icon="collapsed?'el-icon-arrow-down':'el-icon-arrow-up'" size="mini" @click="collapsed=!collapsed"></el-button>
      </span>
    </div>-->
    <div class="nm-idb-block-content" :style="collapsed?'display: none;':''">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  data () {
    return {
      collapsed: false
    }
  },
  props: {
    // 是否显示头部
    header: {
      type: Boolean,
      default () {
        return true
      }
    },
    skinName:
      {
        type: String,
        default () {
          return ''
        }

      },
    isActiveSkin: {
      type: Function
    },
    /** 是否选中 */
    isActive: {
      type: Boolean,
      default () {
        return false
      }

    },
    // 是否折叠头部
    isCollapse: {
      type: Boolean,
      default: true
    }
  }
}
</script>

<style lang="scss">
.nm-idb-block {
  background: #2f373a;
  font-size: 100%;
  color: #4e4e4e;
  background-color: #fff;
  border-radius: 4px;
  margin-bottom: 1.875rem;
  margin-left: 1.5rem;
  transition: all 0.4s ease-in-out 0s;
  position: relative;
  cursor: pointer;
  &:hover,
  &.is-active {
    box-shadow: 1px 3px 8px 3px rgba(#1e90ff, 0.5);
  }
}

.nm-idb-block {
  box-shadow: 1px 3px 8px 3px rgba(0, 0, 0, 0.1);
}
.nm-idb-block-title {
  border-bottom: 1px solid #eaeaea;
  padding: 1rem 1rem;
  position: relative;
}
.nm-idb-block-content {
  padding: 0.5rem 0.5rem;
}
</style>
