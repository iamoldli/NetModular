<template>
  <img :src="$withBase(`/images/${id}.png`)" />
</template>
<script>
export default {
  props: {
    id: String
  }
}
</script>