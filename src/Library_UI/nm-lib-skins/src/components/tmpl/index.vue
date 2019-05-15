<template>
  <nm-container>
    <nm-box page header :title="title" :icon="icon">
      <div>
        <slot/>
      </div>
      <nm-attributes v-if="attrs" :data="attrs"/>
      <div>
        <slot name="after-attrs"/>
      </div>
      <nm-slots v-if="slots" :data="slots"/>
      <div>
        <slot name="after-slots"/>
      </div>
      <nm-methods v-if="methods" :data="methods"/>
      <div>
        <slot name="after-methods"/>
      </div>
      <nm-events v-if="events" :data="events"/>
      <div>
        <slot name="after-events"/>
      </div>
    </nm-box>
  </nm-container>
</template>
<script>
import NmAttributes from './components/attributes'
import NmMethods from './components/methods'
import NmEvents from './components/events'
import NmSlots from './components/slots'
export default {
  name: 'Tmpl',
  components: { NmAttributes, NmMethods, NmEvents, NmSlots },
  props: {
    title: {
      type: String
    },
    icon: {
      type: String
    },
    attrs: {
      type: Array
    },
    slots: {
      type: Array
    },
    methods: {
      type: Array
    },
    events: {
      type: Array
    }
  }
}
</script>
<style lang="scss">
.nm-tmpl-box {
  margin-bottom: 30px;
  &-title {
    margin: 10px 0;
    font-size: 25px;
  }
}
</style>
