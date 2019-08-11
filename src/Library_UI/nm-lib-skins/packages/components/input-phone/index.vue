<template>
  <nm-mask-input :value="value_" :mask="mask" :placeholder="placeholder" @click="onChange">
    <nm-icon v-if="icon" :name="icon" slot="prefix" />
  </nm-mask-input>
</template>
<script>
import mixins from '../../mixins/components/input'
export default {
  name: 'InputPhone',
  mixins: [mixins],
  props: {
    icon: {
      type: String,
      default: 'phone'
    },
    placeholder: {
      type: String,
      default: '15000000000'
    }
  },
  methods: {
    mask() {
      let maskArr = ['1']
      for (let i = 0; i < 10; i++) {
        maskArr.push(/\d/)
      }
      return maskArr
    }
  }
}
</script>
