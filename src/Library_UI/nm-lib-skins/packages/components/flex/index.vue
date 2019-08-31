<template>
  <section :class="['nm-flex',direction,fixMode]" :style="{height}">
    <template v-if="direction==='column'">
      <section class="nm-flex_top" :style="{height:fixMode==='top'?fix:''}">
        <slot name="top" />
      </section>
      <section class="nm-flex_bottom" :style="{height:fixMode==='bottom'?fix:'',paddingTop:gutter?gutter+'px':0}">
        <slot name="bottom" />
      </section>
    </template>
    <template v-else>
      <section class="nm-flex_left" :style="{width:fixMode==='left'?fix:''}">
        <slot name="left" />
      </section>
      <section class="nm-flex_right" :style="{width:fixMode==='right'?fix:'',paddingLeft:gutter?gutter+'px':0}">
        <slot name="right" />
      </section>
    </template>
  </section>
</template>
<script>
export default {
  name: 'Flex',
  props: {
    // 布局方向，对应flex-direction属性
    direction: {
      type: String,
      validator(value) {
        return value === 'column' || value === 'row'
      },
      default: 'column'
    },
    // 固定宽度或高度
    fix: {
      type: String
    },
    // 固定模块，top、bottom、left、right
    fixMode: {
      type: String,
      validator(value) {
        return ['top', 'bottom', 'left', 'right'].includes(value)
      },
      default: 'top'
    },
    height: {
      type: String,
      default: '100%'
    },
    /** 间隔 */
    gutter: Number
  }
}
</script>
