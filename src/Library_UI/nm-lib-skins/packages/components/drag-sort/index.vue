<template>
  <section class="nm-drag-sort">
    <draggable tag="ul" v-model="list" v-bind="dragOptions" v-on="dragOn">
      <li class="nm-drag-sort-item" v-for="(item,index) in list" :key="item.id">{{`${index+1}、${item.label}`}}</li>
    </draggable>
  </section>
</template>
<script>
import draggable from 'vuedraggable'
export default {
  name: 'DragSort',
  components: { draggable },
  data() {
    return {
      list: this.value,
      dragOptions: {
        animation: 200,
        ghostClass: 'nm-drag-sort-ghost'
      },
      dragOn: {
        start: this.onDragStart,
        end: this.onDragEnd
      }
    }
  },
  props: {
    value: {
      type: Array,
      required: true
    }
  },
  methods: {
    onDragStart() {
      this.$emit('start')
    },
    onDragEnd() {
      this.list.map((item, index) => {
        item.sort = index
      })
      this.$emit('input', this.list)
      this.$emit('end')
    }
  },
  watch: {
    value(val) {
      this.list = val
    }
  }
}
</script>
