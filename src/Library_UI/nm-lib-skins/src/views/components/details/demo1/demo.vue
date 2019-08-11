<template>
  <nm-box page header no-padding title="产品详情" icon="detail" fullscreen>
    <nm-details v-bind="details"></nm-details>
  </nm-box>
</template>
<script>
import api from '../../../../api/product'
export default {
  data() {
    return {
      details: {
        action: api.get,
        options: [
          [
            {
              label: '编号',
              prop: 'id'
            },
            {
              label: '产品名称',
              prop: 'title'
            }
          ],
          [
            {
              label: '价格',
              prop: 'price'
            },
            {
              label: '库存',
              prop: 'store'
            }
          ],
          [
            {
              label: '介绍',
              prop: 'des'
            }
          ]
        ]
      }
    }
  }
}
</script>
