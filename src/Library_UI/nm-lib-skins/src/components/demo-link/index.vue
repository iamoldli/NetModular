<template>
  <router-link :to="{name:'nm-'+name}">{{label}}</router-link>
</template>
<script>
export default {
  props: {
    name: String,
    label: String
  }
}
</script>
