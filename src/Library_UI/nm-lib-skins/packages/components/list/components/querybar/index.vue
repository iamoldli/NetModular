<template>
  <!--工具栏-->
  <section class="nm-list-querybar">
    <nm-form ref="normalForm" class="nm-list-querybar-normal" :model="model_" :rules="rules" :inline="true">
      <slot/>
      <el-form-item v-if="!noSearch">
        <nm-button type="primary" @click="query" :icon="!noSearchButtonIcon?'search':''" text="查询"/>
      </el-form-item>
      <el-form-item v-if="!noSearch">
        <nm-button type="info" @click="reset" :icon="!noSearchButtonIcon?'refresh':''" text="重置"/>
      </el-form-item>
      <el-form-item v-if="advanced_.enabled">
        <nm-button ref="showAdvnacedBtn" type="warning" @click="onAdvancedClick">
          高级查询
          <i class="el-icon--right" :class="[showAdvanced?'el-icon-arrow-up':'el-icon-arrow-down']"></i>
        </nm-button>
      </el-form-item>
      <!--自定义按钮插槽-->
      <el-form-item>
        <slot name="buttons"/>
      </el-form-item>
    </nm-form>

    <!--高级查询框-->
    <transition name="el-zoom-in-top">
      <section ref="advancedBox" class="nm-list-querybar-advanced" v-if="advanced_.enabled" v-show="showAdvanced" :style="advancedStyle">
        <nm-box page header footer title="高级查询" icon="search" :toolbar="null">
          <template v-slot:toolbar>
            <nm-button icon="close" @click="showAdvanced=false"/>
          </template>
          <!--查询条件-->
          <nm-form ref="advancedForm" :model="model_" :rules="rules" :label-width="advanced_.labelWidth" :inline="advanced_.inline">
            <slot name="advanced"/>
          </nm-form>
          <template v-slot:footer>
            <!--查询按钮-->
            <nm-button type="primary" @click="query" text="查询" :icon="!noSearchButtonIcon?'search':''"/>
            <!--重置按钮-->
            <nm-button type="info" @click="reset" text="重置" :icon="!noSearchButtonIcon?'refresh':''"/>
          </template>
        </nm-box>
        <div ref="arrow" class="advanced-arrow"/>
      </section>
    </transition>
  </section>
</template>
<script>
const defaultAdvanced = {
  // 是否开启
  enabled: false,
  // 宽度
  width: '400px',
  // 高度
  height: '',
  // 表单标签宽度
  labelWidth: '100px',
  // 内联表单
  inline: false
}
export default {
  data() {
    return {
      // 是否显示高级查询
      showAdvanced: false
    }
  },
  props: {
    /** 输入框的宽度 */
    inputWidth: {
      type: String,
      default: '160px'
    },
    /** 查询模型 */
    model: Object,
    /** 验证规则 */
    rules: Object,
    /** 高级查询属性 */
    advanced: Object,
    /** 不显示按钮图标 */
    noSearchButtonIcon: Boolean,
    /** 不需要查询 */
    noSearch: Boolean
  },
  computed: {
    model_() {
      return this.model || {}
    },
    /** 高级查询设置 */
    advanced_() {
      const ad = this.$_.merge({}, defaultAdvanced, this.advanced)
      ad.enabled = ad.enabled && !this.noSearch
      return ad
    },
    /** 高级查询框样式 */
    advancedStyle() {
      return {
        width: this.advanced_.width,
        height: this.advanced_.height
      }
    }
  },
  methods: {
    query() {
      this.$parent.query()
    },
    /** 表单重置 */
    reset() {
      if (this.$refs.normalForm) {
        this.$refs.normalForm.reset()
      }
      if (this.$refs.advancedForm) {
        this.$refs.advancedForm.reset()
      }
      this.$parent.reset(true)
    },
    /** 高级查询按钮点击事件 */
    onAdvancedClick() {
      this.showAdvanced = !this.showAdvanced

      if (this.showAdvanced) {
        this.$nextTick(() => {
          let $box = this.$refs.advancedBox
          let $arrow = this.$refs.arrow
          const boxWidth = $box.offsetWidth
          const { x, y, width, height } = this.$refs.showAdvnacedBtn.$el.getBoundingClientRect()
          let left = x + width / 2 - $box.offsetWidth / 2
          // 判断右侧有没有超出页面
          if (left + boxWidth > document.body.offsetWidth) {
            left = document.body.offsetWidth - boxWidth - 20
            // 计算箭头的位置
            $arrow.style.left = x - left - 10 + width / 2 + 'px'
          } else {
            // 计算箭头的位置
            $arrow.style.left = $box.offsetWidth / 2 + 'px'
          }

          $box.style.left = left + 'px'
          $box.style.top = y + height + 14 + 'px'

          // 设置高度
          if (!this.advanced_.height) {
            const boxHeader = $box.querySelector('.nm-box-header').offsetHeight
            const boxFooter = $box.querySelector('.nm-box-footer').offsetHeight
            let boxHeight = $box.querySelector('.nm-form').offsetHeight + boxHeader + boxFooter + 20
            if (!this.advanced_.inline) {
              boxHeight += 20
            }
            $box.style.height = boxHeight + 'px'
          }
        })
      }
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.$refs.normalForm.$el.addEventListener('keydown', e => {
        if (e.keyCode === 13) {
          this.query()
        }
      })
      if (this.inputWidth) {
        let inputs = this.$refs.normalForm.$el.querySelectorAll('.el-input__inner')
        if (inputs) {
          for (let i = 0; i < inputs.length; i++) {
            inputs[i].style.width = this.inputWidth
          }
        }
      }
    })
  }
}
</script>
