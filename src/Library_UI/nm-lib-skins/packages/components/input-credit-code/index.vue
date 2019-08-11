<template>
  <nm-mask-input :value="value_" :mask="mask" :placeholder="placeholder" @click="onChange">
    <nm-icon v-if="icon" :name="icon" slot="prefix" />
  </nm-mask-input>
</template>
<script>
import mixins from '../../mixins/components/input'

export default {
  name: 'InputCreditCode',
  mixins: [mixins],
  props: {
    icon: {
      type: String,
      default: 'card'
    },
    placeholder: {
      type: String,
      default: '91320116580493736P'
    }
  },
  methods: {
    mask() {
      let maskArr = []
      for (let i = 0; i < 2; i++) {
        maskArr.push(/[0-9A-HJ-NPQRTUWXY]/)
      }
      for (let i = 0; i < 6; i++) {
        maskArr.push(/\d/)
      }
      for (let i = 0; i < 10; i++) {
        maskArr.push(/[0-9A-HJ-NPQRTUWXY]/)
      }
      return maskArr
    }
  }
}
</script>
