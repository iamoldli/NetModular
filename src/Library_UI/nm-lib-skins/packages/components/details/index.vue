<template>
  <section
    ref="details"
    :class="['nm-details',noBorder?'no-border':'']"
    v-loading="loading"
    :element-loading-text="loadingText"
    :element-loading-background="loadingBackground"
    :element-loading-spinner="loadingSpinner"
  >
    <template v-if="model_">
      <template v-for="(group,i) in options">
        <div v-if="group.length>0" :class="['nm-details-group',`nm-details-group-${group.length}`]" :key="i">
          <div class="nm-details-item" v-for="(item,t) in group" :key="t">
            <div class="nm-details-item-label">
              <slot :name="`label-${item.prop}`" :model="model_" :item="item">{{item.label}}</slot>
            </div>
            <div class="nm-details-item-content">
              <slot :name="`content-${item.prop}`" :model="model_" :item="item">{{model_[item.prop]}}</slot>
            </div>
          </div>
        </div>
      </template>
    </template>
  </section>
</template>
<script>
import { mapState } from 'vuex'
export default {
  name: 'Details',
  data() {
    return {
      model_: {},
      loading_: false
    }
  },
  props: {
    // 查询方法
    action: Function,
    // 模型数据
    model: Object,
    /** 配置项 */
    options: Array,
    /** 标签的宽度 */
    labelWidth: {
      type: String,
      default: '80px'
    },
    // 不显示loading
    noLoading: Boolean,
    // 不包含外边框
    noBorder: Boolean,
    // 是否创建时查询一次
    queryOnCreated: {
      type: Boolean,
      default: true
    }
  },
  computed: {
    ...mapState('app/loading', { loadingText: 'text', loadingBackground: 'background', loadingSpinner: 'spinner' }),
    loading() {
      return !this.noLoading && this.loading_
    }
  },
  methods: {
    query() {
      if (this.action && typeof this.action === 'function') {
        this.loading_ = true
        this.action().then(data => {
          this.model_ = data
          this.loading_ = false
        }).catch(() => {
          this.loading_ = false
        })
      }
    },
    refresh() {
      this.query()
    }
  },
  mounted() {
    if (this.queryOnCreated) {
      this.query()
    }
    if (this.model) {
      this.model_ = this.model
    }
    this.$nextTick(() => {
      // 设置标签宽度
      const items = this.$el.querySelectorAll('.nm-details-item-label')
      for (let i = 0; i < items.length; i++) {
        items[i].style.width = this.labelWidth
      }
    })
  },
  watch: {
    model(val) {
      if (val) {
        this.model_ = val
      }
    }
  }
}
</script>
